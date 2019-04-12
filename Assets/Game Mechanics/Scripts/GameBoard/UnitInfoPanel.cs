using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoPanel : MonoBehaviour {

    const string INFO_DEFENDER_PREVIEW_TAG = "INFO_PREVIEW_UNIT";
    const string INFO_NAME_TAG = "INFO_TEXT_NAME";
    const string INFO_COST_TAG = "INFO_TEXT_COST";
    const string INFO_BOUNTY_TAG = "INFO_TEXT_BOUNTY";
    const string INFO_HEALTH_TAG = "INFO_TEXT_HEALTH";
    const string INFO_DAMAGE_TAG = "INFO_TEXT_DAMAGE";
    const string INFO_ABILITY_TAG = "INFO_TEXT_ABILITY";

    SpriteRenderer defenderPreview;

    Text unitName;
    Text unitCost;
    Text unitBounty;
    Text unitHealth;
    Text unitDamage;
    Text unitAbility;

    // Use this for initialization
    void Start () {
        defenderPreview = GameObject.FindWithTag(INFO_DEFENDER_PREVIEW_TAG).GetComponent<SpriteRenderer>();

        unitName = GameObject.FindWithTag(INFO_NAME_TAG).GetComponent<Text>();
        unitCost = GameObject.FindWithTag(INFO_COST_TAG).GetComponent<Text>();
        unitBounty = GameObject.FindWithTag(INFO_BOUNTY_TAG).GetComponent<Text>();
        unitHealth = GameObject.FindWithTag(INFO_HEALTH_TAG).GetComponent<Text>();
        unitDamage = GameObject.FindWithTag(INFO_DAMAGE_TAG).GetComponent<Text>();
        unitAbility = GameObject.FindWithTag(INFO_ABILITY_TAG).GetComponent<Text>();
    }

    public void UpdateDisplay(SpriteRenderer unitSprite,
                              string unitName, 
                              string unitCost, 
                              string unitBounty, 
                              string unitDamage, 
                              string unitAbility)
    {
        defenderPreview.sprite = unitSprite.sprite;

        this.unitName.text = "Name: " + unitName;
        this.unitCost.text = unitCost != "" ? "Cost: " + unitCost + "g": "";
        this.unitBounty.text = unitBounty != "" ? "Bounty: " + unitBounty + "g": "";
        this.unitDamage.text = "Damage: " + unitDamage;
        this.unitAbility.text = "Ability: "+ unitAbility;
    }

    public void UpdateInfoPanelHealth(string unitHealth)
    {
        this.unitHealth.text = "Health: " + unitHealth;
    }

    public void UnselectAllUnits()
    {
        UnselectAllDefenders();
        UnselectAllAttackers();
    }

    public void UnselectAllDefenders()
    {
        Defender[] defenders = FindObjectsOfType<Defender>();

        foreach (Defender defender in defenders)
        {
            defender.SetSelected(false);
            defender.GetComponentInChildren<SelectedUnitIndicator>().RemoveSelectedIndicator();
        }
    }

    public void UnselectAllAttackers()
    {
        Attacker[] attackers = FindObjectsOfType<Attacker>();

        foreach (Attacker attacker in attackers)
        {
            attacker.SetSelected(false);
            attacker.GetComponentInChildren<SelectedUnitIndicator>().RemoveSelectedIndicator();
        }
    }
}
