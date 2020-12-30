using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class XMLReader : MonoBehaviour
{
    public List<S_Messages> GetPersonMessages(int personID)
    {
        List<S_Messages> messagesList = new List<S_Messages>();
        string id = personID.ToString();

        //Load xml
        XDocument xdoc = XDocument.Load("Assets/APP_XML/Chats.xml");

        //Run query
        var persons = from person in xdoc.Descendants("Person")
                       select new
                       {
                           PersonID = person.Attribute("id").Value,
                           PersonMessages = person.Descendants("Message")
                   };

        foreach (var person in persons)
        {
            if(person.PersonID == id)
            {
                foreach (var message in person.PersonMessages)
                {
                    S_Messages mess;
                    mess.text = message.Element("Text").Value;
                    mess.messageTime = message.Element("Date").Value;

                    if(message.Attribute("isSendByPerson").Value == "true")
                        mess.isSendByPerson = true;
                    else
                        mess.isSendByPerson = false;

                    if (message.Attribute("isActive").Value == "true")
                        mess.isActive = true;
                    else
                        mess.isActive = false;

                    messagesList.Add(mess);
                }
            }
        }

        return messagesList;

    }
}
