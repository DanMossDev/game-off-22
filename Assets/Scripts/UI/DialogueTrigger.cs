using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] string Dialogue;
    [SerializeField] AudioClip DialogueAudio;
    
    void OnTriggerEnter(Collider other)
    {
        DialogueSystem.Instance.ShowDialogue(Dialogue);
    }
}
