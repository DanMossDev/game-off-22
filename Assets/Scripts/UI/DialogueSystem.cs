using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogue;

    public static DialogueSystem Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void ShowDialogue(string incomingText)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(TypeText(incomingText));
    }

    IEnumerator TypeText(string incomingText)
    {
        int i = 1;

        while (i < incomingText.Length)
        {
            dialogue.text = incomingText.Substring(0, i);
            i++;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(4);
        dialogue.text = "";
        dialogueBox.SetActive(false);
    }
}
