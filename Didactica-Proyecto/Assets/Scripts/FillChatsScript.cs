using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillChatsScript : MonoBehaviour
{
    public GameObject chatContainer;
    private List<GameObject> chats;
    // Start is called before the first frame update
    void Start()
    {
        chats = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            chats.Add(chatContainer);
        }

        foreach (var item in chats)
        {
            Instantiate(item, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
