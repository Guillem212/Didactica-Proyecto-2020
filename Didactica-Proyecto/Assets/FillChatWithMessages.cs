using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillChatWithMessages : MonoBehaviour
{
    const int WIDTH_PER_CHAR = 26;
    const int HEIGHT_PER_LINE = 75;

    [SerializeField] GameObject received_msg;
    [SerializeField] GameObject your_msg;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FillChatWithMsg(string name)
    {
        print(name);
        foreach (Transform child in transform) //Limpiar chat anterior
        {
            Destroy(child.gameObject);
        }

        foreach (var item in XMLReader.xmlReader.Application)
        { 
            if (item.Value.person_name.Equals(name))
            {
                foreach (var msginChat in item.Value.messages)
                {
                    if (msginChat.Value.isActive && msginChat.Value.text.Length > 0)
                    {
                        #region Adjust text to fit the container 
                        int m_cont_sl = 0;
                        int m_cont_ch = 0;
                        int m_cont = 0;
                        string modified_msg = "";
                        string m_aux = "";
                        foreach (char c in msginChat.Value.text)
                        {
                            m_cont++;
                            m_cont_ch++;
                            m_aux += (m_cont_ch == 29 && c.Equals(' ')) ? "" : c.ToString();
                            if (c.Equals(' ') || msginChat.Value.text.Length == m_cont) { modified_msg += m_aux; m_aux = ""; }
                            if (m_cont_ch > 28)
                            {
                                modified_msg += m_aux.Length < 29 ? "\n" : "";
                                m_aux += m_aux.Length < 29 ? "" : "\n";
                                m_cont_ch = m_aux.Length < 29 ? m_aux.Equals("") ? 1 : m_aux.Length : 0;
                                m_cont_sl += msginChat.Value.text.Length == m_cont  && m_aux.Equals("") ? 0 : 1;
                            }
                        }
                        #endregion

                        received_msg.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = modified_msg;
                        GameObject msg = Instantiate(received_msg, transform);

                        RectTransform rt = msg.transform.Find("bg_image").GetComponent<RectTransform>();
                        msg.transform.Find("bg_image").GetComponent<RectTransform>().sizeDelta = new Vector2(-Mathf.Clamp(800 - msginChat.Value.text.Length * WIDTH_PER_CHAR, 0, 800), rt.sizeDelta.y);
                        msg.GetComponent<RectTransform>().sizeDelta = new Vector2(msg.GetComponent<RectTransform>().sizeDelta.x, 160 + HEIGHT_PER_LINE * m_cont_sl);

                    }

                    //Answers containers
                    foreach (var ans in msginChat.Value.answers){
                        if (ans.Value.isSelected && ans.Value.text.Length > 0)
                        {
                            #region Adjust text to fit the container
                            int a_cont_sl = 0;
                            int a_cont_ch = 0;
                            int a_cont = 0;
                            string modified_ans = "";
                            string a_aux = "";
                            foreach (char c in ans.Value.text)
                            {
                                a_cont++;
                                a_cont_ch++;
                                a_aux += (a_cont_ch == 29 && c.Equals(' ')) ? "" : c.ToString();
                                if (c.Equals(' ') || ans.Value.text.Length == a_cont) { modified_ans += a_aux; a_aux = ""; }
                                if (a_cont_ch > 28)
                                {
                                    modified_ans += a_aux.Length < 29 ? "\n" : "";
                                    a_aux += a_aux.Length < 29 ? "" : "\n";
                                    a_cont_ch = a_aux.Length < 29 ? a_aux.Equals("")? 1 : a_aux.Length : 0;
                                    a_cont_sl += ans.Value.text.Length == a_cont && a_aux.Equals("") ? 0 : 1;
                                }
                            }
                            #endregion

                            your_msg.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = modified_ans;
                            GameObject msg_ans = Instantiate(your_msg, transform);
                            RectTransform rt_ans = msg_ans.transform.Find("bg_image").GetComponent<RectTransform>();
                            msg_ans.transform.Find("bg_image").GetComponent<RectTransform>().offsetMin = new Vector2(Mathf.Clamp(1200 - ans.Value.text.Length * WIDTH_PER_CHAR, 453, 1200), rt_ans.offsetMin.y);
                            msg_ans.GetComponent<RectTransform>().sizeDelta = new Vector2(msg_ans.GetComponent<RectTransform>().sizeDelta.x, 160 + HEIGHT_PER_LINE * a_cont_sl);
                        }
                       
                    }
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
