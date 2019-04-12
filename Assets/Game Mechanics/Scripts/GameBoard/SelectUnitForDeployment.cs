using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUnitForDeployment : MonoBehaviour
{

    [SerializeField] Defender defenderPrefab;

    [SerializeField] UnitInfoPanel unitInfoPanel;
    [SerializeField] DefenderGrid defenderGrid;

    /* When we select a unit, iterate through all units to grey them out and then make the selected unit's color white
     * Set the current selected defender
     * Tell the game that we are currently selecting a defender to deploy
     */
    private void OnMouseDown()
    {
        var availableUnits = FindObjectsOfType<SelectUnitForDeployment>();

        foreach (SelectUnitForDeployment unit in availableUnits)
        {
            SetDisabledColor(unit);
        }

        GetComponent<SpriteRenderer>().color = Color.white;
        defenderGrid.SetSelectedDefender(defenderPrefab);
        defenderGrid.SetDeployUnitStatus(true);
        
        SetSelectedUnitAttributesToPanel();
    }

    // Sets the color of a unit to grey
    public void SetDisabledColor(SelectUnitForDeployment unit)
    {
        unit.GetComponent<SpriteRenderer>().color = new Color32(145, 145, 145, 255);
    }

    private void SetSelectedUnitAttributesToPanel()
    {
        unitInfoPanel.UnselectAllUnits();
        defenderPrefab.DisplayDefenderInfoOntoPanel(unitInfoPanel);
        unitInfoPanel.UpdateInfoPanelHealth(defenderPrefab.GetUnitHealth().ToString());
    }
}
