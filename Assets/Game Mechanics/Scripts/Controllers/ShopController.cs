using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

    private bool shopOpened;

    GameObject gameShop;

    [SerializeField] AudioController audioController;

	// Use this for initialization
	void Start () {
        gameShop = gameObject;
        shopOpened = false;
        UpdateShopActiveStatus(shopOpened);
	}

    public void ToggleShop()
    {
        shopOpened = !shopOpened;
        UpdateShopActiveStatus(shopOpened);
        audioController.PlayOpenPanelSound();
    }

    public void UpdateShopActiveStatus(bool status)
    {
        gameShop.SetActive(status);
    }

    public bool GetShopStatus()
    {
        return shopOpened;
    }
}
