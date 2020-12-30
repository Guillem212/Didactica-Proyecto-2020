using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Chat : ScriptableObject
{
    public string person_name;
    public bool unreadMessages;
    public S_Messages lastMessage;

    public List<S_Messages> messages;
}
