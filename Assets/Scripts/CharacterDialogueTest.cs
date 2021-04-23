using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDialogueTest : MonoBehaviour
{
    private Text messageText;
    public GameObject uiTextMessage;
    private TextWriter.TextWriterSingle textWriterSingle;

    private void Awake()
    {
        messageText = uiTextMessage.GetComponent<Text>();
    }

    private void Start()
    {
        //messageText.text = "Hello World!";
        textWriterSingle = TextWriter.AddWriterStatic(messageText, "Elijah is a feg", 0.1f, true, true);
    }
}
