﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager canvasManager;
    public GameObject app_Base;
    public GameObject chat_Base;

    private void Awake()
    {
        if (canvasManager == null)
        {
            canvasManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void ChangeToNextCanvas(string name)
    {
        //Change to the canvas that belong to the correspondig chat clicked.
        app_Base.SetActive(!app_Base.activeInHierarchy);
        chat_Base.SetActive(!chat_Base.activeInHierarchy);

        if (chat_Base.activeInHierarchy)
        {
            chat_Base.transform.Find("Header").transform.Find("Header_Name").GetComponent<TMPro.TextMeshProUGUI>().text = name;
        }
    }
}
