using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderGrid : MonoBehaviour {

    Defender defender;

    [Header("Controllers")]
    [SerializeField] UnitInfoPanel unitInfoPanel;
    [SerializeField] ShopController shopController;
    [SerializeField] UpgradeController upgradeController;
    [SerializeField] AudioController audioController;
    [SerializeField] PlayerController playerController;

    GameObject defenderParent;

    bool deployUnit = false;

    // Use this for initialization
    void Awake () {
        defenderParent = GameObject.FindWithTag("DEFENDER_PARENT");
	}

    // Sets the current selected defender
    public void SetSelectedDefender(Defender selectedDefender)
    {
        defender = selectedDefender;
    }

    // Tells the game if we have a unit currently selected
    public void SetDeployUnitStatus(bool status)
    {
        deployUnit = status;
    }

    /*  If we are currently selecting a unit, get the coordinates of where to place it.
        Tell the game that we are no longer selecing a unit for deployment
        Initialize telling the game to reset the visual effects of selecting a unit
    */
    private void OnMouseDown()
    {
        if (deployUnit && shopController.GetShopStatus() && !upgradeController.GetUpgradePanelStatus())
        {
            Vector2 coordinates = GetSquareClicked();

            AttemptToPlaceDefenderAt(coordinates);
            SetDeployUnitStatus(false);
            ResetSelectUnitForDeployment();
        }
    }

    // If we have enough gold, spawn the defender and spent the gold
    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        int defenderCost = defender.GetUnitPrice();

        if (playerController.HaveEnoughGold(defenderCost))
        {
            SpawnDefender(gridPos);
            playerController.RemovePlayerGold(defenderCost);
        }
        else
        {
            Debug.Log("NOT ENOUGH GOLD");
        }
    }

    // Get the coordinates of the square that we are deploying to in world units
    private Vector2 GetSquareClicked()
    {
        Vector2 clickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);

        return SnapToGrid(worldPosition);
    }

    // Rounds the coordinates to an integer value
    private Vector2 SnapToGrid(Vector2 rawWorldPosition)
    {
        float newX = Mathf.RoundToInt(rawWorldPosition.x);
        float newY = Mathf.RoundToInt(rawWorldPosition.y);

        Vector2 snappedWorldPosition = new Vector2(newX, newY);
        return snappedWorldPosition;
    }

    // Creates a defender object of the currently selected defender at the selected coordinates
    private void SpawnDefender(Vector2 coordinates)
    {
        Defender newDefender = Instantiate(defender, coordinates, transform.rotation) as Defender;
        newDefender.transform.parent = defenderParent.transform;
        newDefender.SetDefenderCoordinates(coordinates);
        newDefender.InitializeSelectingUnit(unitInfoPanel);

        audioController.PlayUnitDeployed();
    }

    // Iterate through all available units and set their color back to greyed out
    private void ResetSelectUnitForDeployment()
    {
        var availableUnits = FindObjectsOfType<SelectUnitForDeployment>();

        foreach (SelectUnitForDeployment unit in availableUnits)
        {
            unit.SetDisabledColor(unit);
        }
    }

    public void DestroyAllDefenders()
    {
        Defender[] defenders = FindObjectsOfType<Defender>();

        foreach(Defender defender in defenders)
        {
            Destroy(defender.gameObject);
        }
    }
}
