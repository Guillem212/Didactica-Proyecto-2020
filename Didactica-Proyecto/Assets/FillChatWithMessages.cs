using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillChatWithMessages : MonoBehaviour
{
    const int WIDTH_PER_CHAR = 26;
    const int HEIGHT_PER_LINE = 75;

    [SerializeField] GameObject received_msg;
    [SerializeField] GameObject your_msg;
    [SerializeField] XMLReader xmlReader;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        
    }

    public void FillChatWithMsg(string name)
    {
        foreach (var item in xmlReader.Application)
        {
            if (item.Value.person_name.Equals(name))
            {
                foreach(var msginChat in item.Value.messages)
                {
                    int cont_sl = 0;//msginChat.Value.text.Length < 29? 0  : (int)(((float)msginChat.Value.text.Length / 29) - (((float)msginChat.Value.text.Length / 29) % 1));
                    int cont_ch = 0;
                    int cont_p = 0;
                    foreach(char c in msginChat.Value.text)
                    {
                        cont_ch++;
                        if (!c.Equals(" ")) cont_p++;
                        else if (c.Equals(" ")) cont_p = 0;
                        if (cont_ch > 29)
                        {
                            cont_ch = cont_p < 29?cont_p : 0;
                            cont_sl++;
                        }
                    }
                    received_msg.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = msginChat.Value.text;
                    GameObject msg = Instantiate(received_msg, transform);

                    RectTransform rt = msg.transform.Find("bg_image").GetComponent<RectTransform>();
                    msg.transform.Find("bg_image").GetComponent<RectTransform>().sizeDelta = new Vector2(-Mathf.Clamp(800 - msginChat.Value.text.Length * WIDTH_PER_CHAR, 0, 800), rt.sizeDelta.y);
                    msg.GetComponent<RectTransform>().sizeDelta = new Vector2(msg.GetComponent<RectTransform>().sizeDelta.x, 160 + HEIGHT_PER_LINE * cont_sl);

                    //Answers containers
                    foreach (var ans in msginChat.Value.answers){
                        int cont_l = ans.Value.text.Length < 29 ? 0 : ans.Value.text.Length / 29 + 1;
                        your_msg.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ans.Value.text;

                        GameObject msg_ans = Instantiate(your_msg, transform);
                        RectTransform rt_ans = msg_ans.transform.Find("bg_image").GetComponent<RectTransform>();
                        msg_ans.transform.Find("bg_image").GetComponent<RectTransform>().offsetMin = new Vector2(Mathf.Clamp(1200 - msginChat.Value.text.Length * 40, 453, 1200), rt_ans.offsetMin.y);
                        print(Mathf.Clamp(1200 - msginChat.Value.text.Length * 40, 453, 1200));
                        msg_ans.GetComponent<RectTransform>().sizeDelta = new Vector2(msg_ans.GetComponent<RectTransform>().sizeDelta.x, 160 + HEIGHT_PER_LINE * cont_l);
                    }
                }               
            }
        }
    }
}
