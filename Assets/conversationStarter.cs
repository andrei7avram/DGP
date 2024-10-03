using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class conversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConv;
    [SerializeField] private GameObject obj;
    Animator animator;
    private ConversationManager convManagerInst;

    void Awake() {
        animator = obj.GetComponent<Animator>();
        convManagerInst = ConversationManager.Instance;
        // Initializes stuff.
        convManagerInst.FakeStartConversation(myConv);
        convManagerInst.EndConversation();
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            convManagerInst.StartConversation(myConv);
            animator.SetBool("talking", true);
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag("Player")) {
            convManagerInst.EndConversation();
            animator.SetBool("talking", false);
        }
    }
}
