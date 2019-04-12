using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour {

    const string UPGRADE_BUTTON_TAG = "UPGRADE_BUTTON";
    const string UPGRADE_DEFENDER_PREVIEW_TAG = "UPGRADE_PREVIEW_UNIT";
    const string UPGRADE_NAME_TAG = "UPGRADE_TEXT_NAME";
    const string UPGRADE_COST_TAG = "UPGRADE_TEXT_COST";
    const string UPGRADE_HEALTH_TAG = "UPGRADE_TEXT_HEALTH";
    const string UPGRADE_DAMAGE_TAG = "UPGRADE_TEXT_DAMAGE";
    const string UPGRADE_ABILITY_TAG = "UPGRADE_TEXT_ABILITY";

    Color32 affordableColor;
    Color32 unaffordableColor;

    bool upgradePanelOpen = false;

    Defender currentSelectedDefender;
    Defender upgradeUnit;

    SpriteRenderer upgradeDefenderPreview;

    Text unitName;
    Text unitCost;
    Text unitHealth;
    Text unitDamage;
    Text unitAbility;

    Image upgradeButtonColor;

    [Header("Controllers")]
    [SerializeField] PlayerController playerController;
    [SerializeField] AudioController audioController;
    [SerializeField] UnitInfoPanel unitInfoPanel;

    // Use this for initialization
    void Start ()
    {
        affordableColor = new Color32(0, 133, 42, 255);
        unaffordableColor = new Color32(113, 7, 0, 255);

        UpdatePanelActiveStatus();
    }

    private void Update()
    {
        if (upgradePanelOpen)
        {
            CheckUpgradeButtonColor();
        }
    }

    // Initialize all the editable variables in the Upgrade Panel
    private void InitializeUpgradePanel()
    {
        upgradeButtonColor = GameObject.FindWithTag(UPGRADE_BUTTON_TAG).GetComponent<Image>();

        upgradeDefenderPreview = GameObject.FindWithTag(UPGRADE_DEFENDER_PREVIEW_TAG).GetComponent<SpriteRenderer>();

        unitName = GameObject.FindWithTag(UPGRADE_NAME_TAG).GetComponent<Text>();
        unitCost = GameObject.FindWithTag(UPGRADE_COST_TAG).GetComponent<Text>();
        unitHealth = GameObject.FindWithTag(UPGRADE_HEALTH_TAG).GetComponent<Text>();
        unitDamage = GameObject.FindWithTag(UPGRADE_DAMAGE_TAG).GetComponent<Text>();
        unitAbility = GameObject.FindWithTag(UPGRADE_ABILITY_TAG).GetComponent<Text>();
    }

    // If an upgradable defender is selected, opens the Upgrade Panel. Otherwise it remains closed
    public void OpenUpgradePanel()
    {
        FindSelectedDefender();
        if (currentSelectedDefender)
        {
            GetUpgradedUnitFromSelectedDefender();
        }

        if (upgradeUnit)
        {
            upgradePanelOpen = true;
            UpdatePanelActiveStatus();

            InitializeUpgradePanel();
            PopulateWithUpgradeUnitStats();
        }
    }

    // Closes the Upgrade Panel
    public void CloseUpgradePanel()
    {
        upgradePanelOpen = false;
        UpdatePanelActiveStatus();
    }

    // Sets the Upgrade Panel active status
    private void UpdatePanelActiveStatus()
    {
        audioController.PlayOpenPanelSound();
        gameObject.SetActive(upgradePanelOpen);
    }

    public bool GetUpgradePanelStatus()
    {
        return upgradePanelOpen;
    }

    // Searches the game field for the currently selected defender
    private void FindSelectedDefender()
    {
        Defender[] defenders = FindObjectsOfType<Defender>();

        foreach (Defender defender in defenders)
        {
            if (defender.GetSelectedValue())
            {
                currentSelectedDefender = defender;
            }
        }
    }

    // If there is a currently selected defender, get its upgraded unit
    private void GetUpgradedUnitFromSelectedDefender()
    {
        if (currentSelectedDefender)
        {
            upgradeUnit = currentSelectedDefender.GetUpgradeUnit();
        }
    }

    // Fill the Upgrade Panel UI with the Upgrade Unit stats
    private void PopulateWithUpgradeUnitStats()
    {
        upgradeDefenderPreview.sprite = upgradeUnit.GetComponentInChildren<SpriteRenderer>().sprite;
        unitName.text = upgradeUnit.GetUnitName();
        unitCost.text = upgradeUnit.GetUnitPrice().ToString();
        unitHealth.text = "Health: " + upgradeUnit.GetUnitHealth().ToString();
        unitDamage.text = "Damage: " + upgradeUnit.GetUnitDamage().ToString();
        unitAbility.text = "Ability: " + upgradeUnit.GetAbilityDescription();
    }

    // If the player has enough gold, spend the gold to upgrade the unit and then close the panel
    public void PurchaseUnitUpgrade()
    {
        if (CheckIfPlayerHasEnoughGold())
        {
            audioController.PlayUnitUpgraded();

            SpendPlayerGold();
            RemoveCurrentSelectedDefender();
            InstantiateUpgradeUnit();

            currentSelectedDefender = null;
            upgradeUnit = null;

            CloseUpgradePanel();
        }
        else
        {
            Debug.Log("Not enough gold to upgrade");
        }
    }

    // Checks if the player can afford the upgrade
    private bool CheckIfPlayerHasEnoughGold()
    {
        return playerController.HaveEnoughGold(upgradeUnit.GetUnitPrice());
    }

    // Spends the player's gold on the upgrade
    private void SpendPlayerGold()
    {
        playerController.RemovePlayerGold(upgradeUnit.GetUnitPrice());
    }

    // Destroy the current selected unit
    private void RemoveCurrentSelectedDefender()
    {
        currentSelectedDefender.SetSelected(false);
        currentSelectedDefender.GetComponentInChildren<SelectedUnitIndicator>().RemoveSelectedIndicator();
        Destroy(currentSelectedDefender.gameObject);
    }

    // Create the new upgraded unit in the old unit's position and select it
    private void InstantiateUpgradeUnit()
    {
        Defender newUpgradeUnit = Instantiate(upgradeUnit, currentSelectedDefender.transform.position, currentSelectedDefender.transform.rotation) as Defender;

        newUpgradeUnit.InitializeSelectingUnit(unitInfoPanel);
    }

    // Changes the upgrade button's color based on whether the player can afford the upgrade or not
    private void CheckUpgradeButtonColor()
    {
        upgradeButtonColor.color = CheckIfPlayerHasEnoughGold() ? affordableColor : unaffordableColor;
    }
}
