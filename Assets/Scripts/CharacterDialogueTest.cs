using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDialogueTest : MonoBehaviour
{
    private Text messageText;
    public GameObject uiTextMessage;
    public string[] dialogueText;
    private int count = 0;
    private TextWriter.TextWriterSingle textWriterSingle;

    private void Awake()
    {
        messageText = uiTextMessage.GetComponent<Text>();
    }

    private void Start()
    {
        //messageText.text = "Hello World!";
        textWriterSingle = TextWriter.AddWriterStatic(messageText, dialogueText[0], 0.1f, true, true);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (textWriterSingle != null && textWriterSingle.Isactive())
            {
                textWriterSingle.WriteAllAndDestroy();
            }
            else
            {
                if (count < (dialogueText.Length - 1))
                {
                    count++;
                    textWriterSingle = TextWriter.AddWriterStatic(messageText, dialogueText[count], 0.1f, true, true);
                }
                else
                {
                    //reset back to start of the array
                    count = 0;
                }
            }
        }
    }
}
