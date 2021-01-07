using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAnswers : MonoBehaviour
{
    // Start is called before the first frame update
    const int WIDTH_PER_CHAR = 26;
    const int HEIGHT_PER_LINE = 75;

    [SerializeField] GameObject answer;
    [SerializeField] GameObject content;
    [SerializeField] TMPro.TextMeshProUGUI person;

    public void SendAnswer(int msg_k, int ans_k)
    {
        foreach (var item in XMLReader.xmlReader.Application)
        {
            if (item.Value.person_name.Equals(person.text))
            {
                S_Answers aux_ans = item.Value.messages[msg_k].answers[ans_k];
                aux_ans.isSelected = true;
                item.Value.messages[msg_k].answers[ans_k] = aux_ans;

                //for(int i = msg_k + 1; item.Value.messages[i].answers[0].text.Length > 0 && i < item.Value.messages.Count - 1; i++)
                //{
                    S_Messages aux_msg = item.Value.messages[msg_k + 1];
                    aux_msg.isActive = true;
                    item.Value.messages[msg_k + 1] = aux_msg;
                //}

                break;
            }     
        }
        content.GetComponent<FillChatWithMessages>().FillChatWithMsg(person.text);
    }
    public void FillAnswersContent()
    {
        foreach (Transform child in transform) //Limpiar chat anterior
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (var item in XMLReader.xmlReader.Application)
        {
            if (item.Value.person_name.Equals(person.text))
            {
                foreach(var msg in item.Value.messages)
                {
                    if (!msg.Value.isActive) //Que mensaje quiero responder
                    {
                        //Answers containers
                        foreach (var ans in item.Value.messages[msg.Key - 1].answers)
                        {
                            print(ans.Value.isSelected);
                            #region Adjust text to fit the container 
                            int m_cont_sl = 0;
                            int m_cont_ch = 0;
                            int m_cont = 0;
                            string modified_msg = "";
                            string m_aux = "";
                            foreach (char c in ans.Value.text)
                            {
                                m_cont++;
                                m_cont_ch++;
                                m_aux += (m_cont_ch == 44 && c.Equals(' ')) ? "" : c.ToString();
                                if (c.Equals(' ') || ans.Value.text.Length == m_cont) { modified_msg += m_aux; m_aux = ""; }
                                if (m_cont_ch > 43)
                                {
                                    modified_msg += m_aux.Length < 44 ? "\n" : "";
                                    m_aux += m_aux.Length < 44 ? "" : "\n";
                                    m_cont_ch = m_aux.Length < 44 ? m_aux.Equals("") ? 1 : m_aux.Length : 0;
                                    m_cont_sl += ans.Value.text.Length == m_cont && m_aux.Equals("") ? 0 : 1;
                                }
                            }
                            #endregion

                            answer.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = modified_msg;
                            GameObject m = Instantiate(answer, transform);
                            m.GetComponent<Button>().onClick.AddListener(delegate { SendAnswer(msg.Key-1, ans.Key); });
                            
                            m.GetComponent<RectTransform>().sizeDelta = new Vector2(m.GetComponent<RectTransform>().sizeDelta.x, 215 + HEIGHT_PER_LINE * m_cont_sl);

                        }

                        //Chat readed
                        S_Chat aux_chat = item.Value;
                        aux_chat.unreadMessages = false;
                        XMLReader.xmlReader.Application[item.Key] = aux_chat;
                        return;
                    }
                }
            }

        }
    }
}
