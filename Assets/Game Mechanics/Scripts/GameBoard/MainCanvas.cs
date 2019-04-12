using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour {

    private void Awake()
    {
        EnsureOneInstanceOfThisObject();
    }

    private void EnsureOneInstanceOfThisObject()
    {
        MainCanvas[] mainCanvases = FindObjectsOfType<MainCanvas>();

        if (mainCanvases.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
