using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager canvasManager;

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

    public void ChangeToNextCanvas()
    {
        //Change to the canvas that belong to the correspondig chat clicked.
    }
}
