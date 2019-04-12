using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelButton : MonoBehaviour {

    [SerializeField] Sprite upgradeImage;
    [SerializeField] Sprite noUpgradeImage;

    Image image;

    // Use this for initialization
    void Start () {
        image = gameObject.GetComponent<Image>();
    }
	
    public void UpdateUpgradeButtonUI(Defender upgradeUnit)
    {
        if (upgradeUnit)
        {
            image.sprite = upgradeImage;
        }
        else
        {
            image.sprite = noUpgradeImage;
        }
    }
}
