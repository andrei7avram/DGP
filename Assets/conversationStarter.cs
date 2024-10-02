using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class conversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConv;

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            ConversationManager.Instance.StartConversation(myConv); 
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag("Player")) {
            ConversationManager.Instance.EndConversation();
        }
    }
}
