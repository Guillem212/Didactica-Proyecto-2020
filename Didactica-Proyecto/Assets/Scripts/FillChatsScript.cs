using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FillChatsScript : MonoBehaviour
{
    public GameObject chatContainer;
    private List<GameObject> chats;
    bool filled = false;
    GameObject gm;
    // Start is called before the first frame update

    public void FillChats()
    {
        chats.Clear();
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var item in XMLReader.xmlReader.Application)
        {
            chatContainer.transform.Find("Person_name").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.person_name;
            chatContainer.transform.Find("Last_Message").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.lastMessage.text;
            chatContainer.transform.Find("Has_Unread_Message").gameObject.SetActive(item.Value.unreadMessages);
            chatContainer.transform.Find("Last_Time").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.lastMessage.messageTime;

            GameObject chat = Instantiate(chatContainer, transform);

            //Creates listener for each chat passing the name of hte person to hte function
            chat.GetComponent<Button>().onClick.AddListener(delegate { gm.GetComponent<CanvasManager>().ChangeToNextCanvas(item.Value.person_name); });
            chats.Add(chat);
        }
    }
    void Start()
    {
        gm = GameObject.Find("GameManager");
        chats = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!filled && XMLReader.xmlReader != null)
        {
            FillChats();
            filled = true;
        }
    }
}
