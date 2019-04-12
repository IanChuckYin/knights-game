using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {

    [SerializeField] float movespeed;
    [SerializeField] int bounty;

    [SerializeField] int damage;
    [SerializeField] int health;
    [SerializeField] string unitName;
    [SerializeField] string abilityDescription;

    float stoppedMovespeed = 0;
    float deathTimerDelay = 1f;

    bool selected = false;
    bool isDead = false;

    GameObject currentTarget;
    UnitInfoPanel unitInfoPanel;
    UpgradePanelButton upgradePanelButton;

    LevelController levelController;
    AudioController audioController;

	// Use this for initialization
	void Awake () {
        levelController = FindObjectOfType<LevelController>();
        levelController.AttackerSpawned();

        unitInfoPanel = FindObjectOfType<UnitInfoPanel>();
        upgradePanelButton = FindObjectOfType<UpgradePanelButton>();
        audioController = FindObjectOfType<AudioController>();
    }

    private void GivePlayerKillBounty()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();

        if (playerController)
        {
            playerController.AddPlayerGold(bounty);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isDead)
        {
            UpdateAnimationState();
            CheckIfSelected();
        }
    }

    private void CheckIfSelected()
    {
        if (selected)
        {
            unitInfoPanel.UpdateInfoPanelHealth(GetComponent<Health>().GetHealth().ToString());
            upgradePanelButton.UpdateUpgradeButtonUI(null);
        }
    }

    private void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            StopAnimatingAttack();
            Move(movespeed);
        }
        else {
            Move(stoppedMovespeed);
        }
    }

    // Moves the unit left
    public void Move(float movespeed)
    {
        transform.Translate(Vector2.left * movespeed * Time.deltaTime);
    }

    private void AnimateAttack()
    {
        GetComponent<Animator>().SetBool("IsAttacking", true);
    }

    private void StopAnimatingAttack()
    {
        GetComponent<Animator>().SetBool("IsAttacking", false);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;

        if (otherObject.GetComponent<Defender>())
        {
            Attack(otherObject);
        }
    }

    private void Attack(GameObject target)
    {
        AnimateAttack();

        currentTarget = target;
    }

    public void HitCurrentTarget()
    {
        if (!currentTarget) { return;  }

        Health health = currentTarget.GetComponent<Health>();

        if (health)
        {
            health.DealDamage(damage);

            audioController.PlayRandomSlashSound();

            if (health.GetHealth() <= 0)
            {
                currentTarget = null;
                CheckIfDevil();
            }
        }
    }

    private void OnMouseDown()
    {
        InitializeSelectingUnit(unitInfoPanel);
    }

    public void InitializeSelectingUnit(UnitInfoPanel unitInfoPanel)
    {
        unitInfoPanel.UnselectAllUnits();
        DisplayAttackerInfoOntoPanel(unitInfoPanel);
        ApplyIndicator();
        SetSelected(true);
    }

    // Pass in a UnitInfoPanel object to help initialize it from other classes
    public void DisplayAttackerInfoOntoPanel(UnitInfoPanel unitInfoPanel)
    {
        unitInfoPanel.UpdateDisplay(GetComponentInChildren<SpriteRenderer>(),
                                    unitName,
                                    "",
                                    bounty.ToString(),
                                    damage.ToString(),
                                    abilityDescription);
    }

    private void ApplyIndicator()
    {
        GetComponentInChildren<SelectedUnitIndicator>().AddSelectedIndicator();
    }

    public void KillAttacker()
    {
        isDead = true;

        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());

        Move(stoppedMovespeed);

        StopAnimatingAttack();
        GetComponent<Animator>().SetBool("IsDead", true);

        Destroy(gameObject, deathTimerDelay);
    }

    private void OnDestroy()
    {
        if (levelController != null)
        {
            levelController.AttackerKilled();
        }

        GivePlayerKillBounty();
    }

    private void CheckIfDevil()
    {
        if (GetComponent<Devil>())
        {
            GetComponent<Devil>().SummonUnit();
        }
    }


    // GETTERS AND SETTERS
    public void SetAttackerBounty(int bounty)
    {
        this.bounty = bounty;
    }

    public int GetAttackBounty()
    {
        return bounty;
    }

    public void SetMovementSpeed(float speed)
    {
        movespeed = speed;
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
}
