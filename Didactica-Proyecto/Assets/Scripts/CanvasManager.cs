using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager canvasManager;
    public GameObject app_Base;
    public GameObject chat_Base;

    private GameObject sendContainer;
    private GameObject sendContainerOpened;
    private float scrollClosed;

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
    private void Start()
    {
        sendContainer = chat_Base.transform.Find("SendContainer").gameObject;
        sendContainerOpened = chat_Base.transform.Find("SendContainerOpened").gameObject;
    }

    public void ChangeToNextCanvas(string name)
    {
        //Change to the canvas that belong to the correspondig chat clicked.
        app_Base.SetActive(!app_Base.activeInHierarchy);
        chat_Base.SetActive(!chat_Base.activeInHierarchy);

        //Si estoy en un chat pues relleno con las movidas de su chat
        if (chat_Base.activeInHierarchy) FillChatWithPerson(name);
        else app_Base.GetComponentInChildren<FillChatsScript>().FillChats();
    }

    private void FillChatWithPerson(string name)
    {
        chat_Base.transform.Find("Header").transform.Find("Header_Name").GetComponent<TMPro.TextMeshProUGUI>().text = name;
        chat_Base.GetComponentInChildren<FillChatWithMessages>().FillChatWithMsg(name);
    }

    public void OpenCloseSendContainer()
    {

        sendContainer.SetActive(!sendContainer.activeInHierarchy);
        sendContainerOpened.SetActive(!sendContainerOpened.activeInHierarchy);

        if (sendContainerOpened.activeInHierarchy)
        {
            sendContainerOpened.GetComponentInChildren<FillAnswers>().FillAnswersContent();
            RectTransform scrollViewRect = chat_Base.transform.Find("Scroll_View").GetComponent<RectTransform>();
            scrollClosed = scrollViewRect.offsetMin.y;
            scrollViewRect.offsetMin = new Vector2(scrollViewRect.offsetMin.x, -(sendContainerOpened.GetComponent<RectTransform>().position.y + 100 - 2440));
        }
        else
        {
            RectTransform scrollViewRect = chat_Base.transform.Find("Scroll_View").GetComponent<RectTransform>();
            scrollViewRect.offsetMin = new Vector2(scrollViewRect.offsetMin.x, scrollClosed);
        }
    }
}
