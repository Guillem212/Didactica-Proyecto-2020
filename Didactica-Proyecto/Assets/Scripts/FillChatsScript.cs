using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FillChatsScript : MonoBehaviour
{
    public Sprite[] faces;

    public GameObject chatContainer;
    private List<GameObject> chats;
    bool filled = false;
    GameObject gm;

    public void FillChats()
    {
        chats.Clear();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in XMLReader.xmlReader.Application)
        {
            chatContainer.transform.Find("Person_name").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.person_name;
            foreach (var m in item.Value.messages)
            {
                if(m.Value.isActive)
                    chatContainer.transform.Find("Last_Message").GetComponent<TMPro.TextMeshProUGUI>().text = m.Value.text;
            }
            chatContainer.transform.Find("Has_Unread_Message").gameObject.SetActive(item.Value.unreadMessages);
            chatContainer.transform.Find("Last_Time").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.lastMessage.messageTime;

            foreach (var f in faces)
            {
                if(f.name == item.Value.person_name)
                    chatContainer.transform.Find("Image").GetComponent<Image>().sprite = f;
            }

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
