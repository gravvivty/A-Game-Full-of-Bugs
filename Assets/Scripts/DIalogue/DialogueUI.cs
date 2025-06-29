using UnityEngine;
using UnityEngine.UI;
using Project.Dialogue.Data;
using System.Collections.Generic;
using TMPro;
using Project.Inventory;
using Unity.VisualScripting;

namespace Project.Dialogue
{
    /// <summary>
    /// Class to manage the dialogue UI and display dialogue lines and choices.
    /// </summary>
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Transform choicesContainer;
        [SerializeField] private Button choiceButtonPrefab;

        public void DisplayDialogue(DialogueLine dialogue)
        {
            dialogueText.text = $"{dialogue.SpeakerName}: {dialogue.Text}";
            if (CheckConditions(dialogue.Conditions) == false)
            {
                Debug.Log("Dialogue conditions not met, skipping dialogue.");
                dialogueText.text = "...";
                return;
            }
            DisplayChoices(dialogue.Choices);
        }

        private void DisplayChoices(List<DialogueChoice> choices)
        {
            // Clear existing choices
            foreach (Transform child in choicesContainer)
            {
                Destroy(child.gameObject);
            }

            // Create new choice buttons
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i].Conditions != null && choices[i].Conditions.Count > 0)
                {
                    if (!CheckChoiceConditions(choices[i])) continue; // Skip this choice if conditions are not met
                }

                if (choices[i].isRemoved)
                {
                    continue;
                }

                var choice = choices[i];
                Debug.Log($"Choice {i}: {choice.Text}");
                int choiceIndex = i;

                // Instantiate the choice button from the prefab
                Button choiceButton = Instantiate(choiceButtonPrefab, choicesContainer);
                choiceButton.gameObject.transform.SetParent(choicesContainer, false);

                // Configure ContentSizeFitter on the button
                ContentSizeFitter buttonContentSizeFitter = choiceButton.GetComponent<ContentSizeFitter>();
                if (buttonContentSizeFitter == null)
                {
                    buttonContentSizeFitter = choiceButton.gameObject.AddComponent<ContentSizeFitter>();
                }
                buttonContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
                buttonContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

                // Reset all components to enabled state
                foreach (var component in choiceButton.GetComponentsInChildren<MonoBehaviour>())
                {
                    component.enabled = true;
                }

                // Configure the text component
                TextMeshProUGUI textComponent = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                textComponent.text = choice.Text;

                // Configure LayoutElement on the text for minimum height calculation
                LayoutElement textLayoutElement = textComponent.GetComponent<LayoutElement>();
                if (textLayoutElement == null)
                {
                    textLayoutElement = textComponent.gameObject.AddComponent<LayoutElement>();
                }
                textLayoutElement.preferredHeight = -1;
                textLayoutElement.minHeight = textComponent.fontSize + 10;

                choiceButton.onClick.AddListener(() => DialogueManager.Instance.MakeChoice(choiceIndex));
            }
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(choicesContainer.GetComponent<RectTransform>());

        }

        public bool CheckChoiceConditions(DialogueChoice dialogueChoice)
        {
            // Check conditions here and skip if not met
            bool conditionsMet = true;
            foreach (var condition in dialogueChoice.Conditions)
            {
                switch (condition.Type)
                {
                    case ConditionType.HasItem:
                        // Check if the player has the item
                        if (!InventoryManager.Instance.HasItemWithID(condition.ItemID))
                        {
                            conditionsMet = false;
                        }
                        break;
                }
            }

            return conditionsMet;
        }

        public bool CheckConditions(List<DialogueCondition> conditions)
        {
            // Check conditions here and skip if not met
            bool conditionsMet = true;
            foreach (var condition in conditions)
            {
                switch (condition.Type)
                {
                    case ConditionType.HasItem:
                        // Check if the player has the item
                        if (!InventoryManager.Instance.HasItemWithID(condition.ItemID))
                        {
                            conditionsMet = false;
                        }
                        break;
                }
            }

            return conditionsMet;
        }
    }
}