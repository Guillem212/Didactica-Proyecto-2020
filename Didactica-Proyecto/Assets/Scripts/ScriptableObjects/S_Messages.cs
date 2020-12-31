using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct S_Messages
{
    public string text;
    public string messageTime;
    public bool isActive;
    public bool isSendByPerson;

    public Dictionary<int, S_Answers> answers;
}
