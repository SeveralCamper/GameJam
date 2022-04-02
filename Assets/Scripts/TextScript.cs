using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public bool typingNow = false;
    public float textSpeed= 0.2f;
    public float blickCursorSpeed = 0.7f;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = GetComponentsInParent<RectTransform>()[1];
        GetComponent<RectTransform>().sizeDelta = new Vector2(rt.sizeDelta.x * 0.7f, rt.sizeDelta.y * 0.7f);
        StartCoroutine(blickCursor());
    }

    // Update is called once per frame
    void Update()
    {
        if (!typingNow) say("delay the inevitable");
    }

    public void say(string message)
    {
        if (!typingNow) StartCoroutine(changeText(message));
    }

    public IEnumerator changeText(string message)
    {
        typingNow = true;
        GetComponent<Text>().text = "> ";
        for (int i = 0; i < message.Length; i++)
        {
            GetComponent<Text>().text += message[i].ToString();
            yield return new WaitForSeconds(textSpeed);
        }
        GetComponent<Text>().text += "_";
        typingNow = false;
        StartCoroutine(blickCursor());
    }

    public IEnumerator blickCursor()
    {
        bool isCursor = true;
        while(!typingNow)
        {
            yield return new WaitForSeconds(blickCursorSpeed);
            if (!typingNow)
            {
                if (isCursor)
                {
                    string message = GetComponent<Text>().text;
                    message = message.Remove(message.Length - 1);
                    GetComponent<Text>().text = message;
                }
                else
                {
                    GetComponent<Text>().text += "_";
                }
                isCursor = !isCursor;
            }
        }
    }

    // ¯\_(ツ)_/¯
    // ＼(≧▽≦)／
    // (；⌣̀_⌣́)
    // (×﹏×)
    // (▼益▼)
}
