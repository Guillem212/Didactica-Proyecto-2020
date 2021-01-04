using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct S_Chat
{
    //public string id;
    public string person_name;
    public bool unreadMessages;
    public S_Messages lastMessage;

    public Dictionary<int, S_Messages> messages;
}
