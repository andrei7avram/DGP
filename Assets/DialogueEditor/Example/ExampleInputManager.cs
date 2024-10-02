using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueEditor
{
    public class ExampleInputManager : MonoBehaviour
    {
        public KeyCode m_UpKey;
        public KeyCode m_DownKey;
        public KeyCode m_SelectKey;

        private void Update()
        {
            if (ConversationManager.Instance != null)
            {
                UpdateConversationInput();
            }
        }

        private void UpdateConversationInput()
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                if (Input.GetKeyDown(m_UpKey)) {
                    ConversationManager.Instance.SelectPreviousOption();
                    Debug.Log("up");
                }
                else if (Input.GetKeyDown(m_DownKey)) {
                    ConversationManager.Instance.SelectNextOption();
                    Debug.Log("down");
                }
                else if (Input.GetKeyDown(m_SelectKey))
                    ConversationManager.Instance.PressSelectedOption();
            }
        }
    }
}
