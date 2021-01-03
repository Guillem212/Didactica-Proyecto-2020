using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class FillChatsScript : MonoBehaviour
{
    public GameObject chatContainer;
    public XMLReader xmlReader;
    private List<GameObject> chats;
    bool filled = false;
    // Start is called before the first frame update

    public void FillChats()
    {
        chats = new List<GameObject>();

        foreach (var item in xmlReader.Application)
        {
            Debug.Log(item.Value.person_name);
            chatContainer.transform.Find("Person_name").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.person_name;
            chatContainer.transform.Find("Last_Message").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.lastMessage.text;
            chatContainer.transform.Find("Has_Unread_Message").gameObject.SetActive(item.Value.unreadMessages);
            chatContainer.transform.Find("Last_Time").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.lastMessage.messageTime;

            Instantiate(chatContainer, transform);
            //chats.Add(chatContainer);
        }

       /* foreach (var item in chats)
        {
            Instantiate(item, transform);
        }*/
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!filled && xmlReader!= null)
        {
            FillChats();
            filled = true;
        }
    }
}
