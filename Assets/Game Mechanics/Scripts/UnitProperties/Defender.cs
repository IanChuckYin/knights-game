using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour {

    [SerializeField] int goldCost;
    [SerializeField] int damage;
    [SerializeField] int health;
    [SerializeField] string unitName;
    [SerializeField] string abilityDescription;
    [SerializeField] Defender upgradeUnit;

    SpriteRenderer spriteRenderer;

    Vector2 coordinates;

    UnitInfoPanel unitInfoPanel;
    UpgradePanelButton upgradePanelButton;

    float deathTimerDelay = 1f;

    bool selected = false;

    // Use this for initialization
    void Awake () {
        unitInfoPanel = FindObjectOfType<UnitInfoPanel>();
        upgradePanelButton = FindObjectOfType<UpgradePanelButton>();
    }

    // Update is called once per frame
    void Update () {
        CheckIfSelected();
	}

    // Check if this unit is currently being selected
    private void CheckIfSelected()
    {
        if (selected)
        {
            DisplayDefenderHealthOntoPanel();
            upgradePanelButton.UpdateUpgradeButtonUI(upgradeUnit);
        }
    }

    private void OnMouseDown()
    {
        InitializeSelectingUnit(unitInfoPanel);
    }

    public void InitializeSelectingUnit(UnitInfoPanel unitInfoPanel)
    {
        unitInfoPanel.UnselectAllUnits();
        DisplayDefenderInfoOntoPanel(unitInfoPanel);
        ApplyIndicator();
        SetSelected(true);
    }

    // Pass in a UnitInfoPanel object to help initialize it from other classes
    public void DisplayDefenderInfoOntoPanel(UnitInfoPanel unitInfoPanel)
    {
        unitInfoPanel.UpdateDisplay(GetComponentInChildren<SpriteRenderer>(),
                                    unitName,
                                    goldCost.ToString(),
                                    "",
                                    damage.ToString(),
                                    abilityDescription);
    }

    public void DisplayDefenderHealthOntoPanel()
    {
        unitInfoPanel.UpdateInfoPanelHealth(GetComponent<Health>().GetHealth().ToString());
    }

    public void ApplyIndicator()
    {
        GetComponentInChildren<SelectedUnitIndicator>().AddSelectedIndicator();
    }

    public void KillDefender()
    {
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());

        GetComponent<Animator>().SetBool("IsAttacking", false);
        GetComponent<Animator>().SetBool("IsDead", true);

        Destroy(gameObject, deathTimerDelay);
    }


    // GETTERS AND SETTERS
    public void SetUnitPrice(int price)
    {
        goldCost = price;
    }

    public int GetUnitPrice()
    {
        return goldCost;
    }

    public void SetUnitDamage(int damage) { this.damage = damage; }

    public int GetUnitDamage() { return damage; }


    public void SetUnitHealth(int health) { this.health = health; }

    public int GetUnitHealth() { return health; }


    public void SetUnitName(string name) { unitName = name; }

    public string GetUnitName() { return unitName; }


    public void SetAbilityDescription(string description) { abilityDescription = description; }

    public string GetAbilityDescription() { return abilityDescription; }


    public void SetSelected(bool selected) { this.selected = selected; }

    public bool GetSelectedValue() { return selected; }

    public Defender GetUpgradeUnit()
    {
        return upgradeUnit;
    }

    public Vector2 GetDefenderCoordinates() { return coordinates; }

    public void SetDefenderCoordinates(Vector2 coordinates) { this.coordinates = coordinates; }
}
