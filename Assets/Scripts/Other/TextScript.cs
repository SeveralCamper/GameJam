using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public bool typingNow = false;
    public float textSpeed= 0.05f;
    public float blickCursorSpeed = 1f;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = GetComponentsInParent<RectTransform>()[1];
        GetComponent<RectTransform>().sizeDelta = new Vector2(rt.sizeDelta.x * 0.7f, rt.sizeDelta.y * 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        // if (!typingNow) say("delay the inevitable");
    }

    public void say(string message)
    {
        if (!typingNow) StartCoroutine(changeText(message));
    }

    private IEnumerator changeText(string message)
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
    }

    // ¯\_(ツ)_/¯
    // ＼(≧▽≦)／
    // (；⌣̀_⌣́)
    // (×﹏×)
    // (▼益▼)
}
