using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class XMLReader : MonoBehaviour
{

    public Dictionary<int, S_Chat> Application;

    private void Start()
    {
        Application = new Dictionary<int, S_Chat>();
        GetChats(ref Application);

       /* foreach (var item in Application)
        {
            Debug.Log(item.Value.person_name);
            foreach (var m in item.Value.messages)
            {
                Debug.Log(m.Value.text + " " + m.Value.messageTime);
                foreach (var a in m.Value.answers)
                {
                    Debug.Log(a.Value.text);
                }
            }
        }*/

    }

    public void GetChats(ref Dictionary<int, S_Chat> application)
    {
        //Load xml
        XDocument xdoc = XDocument.Load("Assets/APP_XML/Chats.xml");

        //Run query
        var persons = from person in xdoc.Descendants("Person")
                      select new
                      {
                          person = person
                      };

        int id = 0;
        foreach (var person in persons)
        {
            id++;
            S_Chat chat;
            chat.person_name = person.person.Attribute("name").Value;
            //chat.id = person.person.Attribute("id").Value; 
            chat.messages = GetPersonMessages(id);
            chat.unreadMessages = true;
            chat.lastMessage = chat.messages.Last().Value;
            application.Add(id, chat);
        }
    }

    /// <summary>
    /// Gets all the messages that you have recieved from a person.
    /// </summary>
    /// <param name="personID">ID of the person on the XML</param>
    /// <returns>List of messages</returns>
    private Dictionary<int, S_Messages> GetPersonMessages(int personID)
    {
        Dictionary<int, S_Messages> messagesList = new Dictionary<int, S_Messages>();

        //Load xml
        XDocument xdoc = XDocument.Load("Assets/APP_XML/Chats.xml");

        //Run query
        var persons = from person in xdoc.Descendants("Person")
                       select new
                       {
                           PersonID = person.Attribute("id").Value,
                           PersonMessages = person.Descendants("Message")
                   };
        int i = 0;
        foreach (var person in persons)
        {
            if(person.PersonID == personID.ToString())
            {
                foreach (var message in person.PersonMessages)
                {
                    i++;
                    S_Messages mess;
                    mess.text = message.Element("Text").Value;
                    mess.messageTime = message.Element("Date").Value;

                    if (message.Attribute("isActive").Value == "true")
                        mess.isActive = true;
                    else
                        mess.isActive = false;

                    if (message.Attribute("isSendByPerson").Value == "true")
                        mess.isSendByPerson = true;
                    else
                        mess.isSendByPerson = false;

                    mess.answers = null;

                    mess.answers = GetAnswersToMessage(mess, i, personID);

                    messagesList.Add(i, mess);
                }
            }
        }
        return messagesList;
    }

    /// <summary>
    /// Gets all the answers that the last message have disposable.
    /// </summary>
    /// <param name="personID">Id of the person</param>
    /// <param name="messageValue">The last message</param>
    /// <returns>List of posible answers.</returns>
    private Dictionary<int, S_Answers> GetAnswersToMessage(S_Messages messageValue, int messageID, int personID)
    {
        Dictionary<int, S_Answers> answersList = new Dictionary<int, S_Answers>();
        string mess_id = messageID.ToString();
        string person_id = personID.ToString();

        //Load xml
        XDocument xdoc = XDocument.Load("Assets/APP_XML/Chats.xml");

        //Run query
        var persons = from person in xdoc.Descendants("Person")
                      select new
                      {
                          PersonID = person.Attribute("id").Value,
                          PersonMessages = person.Descendants("Message")
                      };
        int id = 0;
        foreach (var person in persons)
        {
            if (person.PersonID == person_id)
            {
                foreach (var message in person.PersonMessages)
                {
                    if(message.Attribute("id").Value == mess_id)
                    {
                        foreach (var a in message.Elements("Answer"))
                        {
                            id++;
                            S_Answers ans;
                            ans.text = a.Value;

                            if (a.Attribute("isActive").Value == "true")
                                ans.isSelected = true;
                            else
                                ans.isSelected = false;

                            answersList.Add(id, ans);
                        }
                    }
                }
            }
        }
        return answersList;
    }
}
