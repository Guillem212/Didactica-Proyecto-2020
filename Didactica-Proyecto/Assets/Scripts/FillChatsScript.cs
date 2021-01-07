﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FillChatsScript : MonoBehaviour
{
    public Sprite[] faces;

    public GameObject chatContainer;
    public XMLReader xmlReader;
    private List<GameObject> chats;
    bool filled = false;
    GameObject gm;
    // Start is called before the first frame update

    public void FillChats()
    {
        chats = new List<GameObject>();

        foreach (var item in xmlReader.Application)
        {
            chatContainer.transform.Find("Person_name").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.person_name;
            chatContainer.transform.Find("Last_Message").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.lastMessage.text;
            chatContainer.transform.Find("Has_Unread_Message").gameObject.SetActive(item.Value.unreadMessages);
            chatContainer.transform.Find("Last_Time").GetComponent<TMPro.TextMeshProUGUI>().text = item.Value.lastMessage.messageTime;
            foreach (var face in faces)
            {
                if(item.Value.person_name == face.name)
                {
                    chatContainer.transform.Find("Image").GetComponent<Image>().sprite = face;
                }
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
