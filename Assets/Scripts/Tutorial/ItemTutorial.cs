using Project.Dialogue;
using Project.Inventory;
using UnityEngine;

namespace Project.Tutorial
{
    public class ItemTutorial : MonoBehaviour
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] private TutorialUI tutorialBoxUI;
        [SerializeField][TextArea] private string dialogueToShow;
        [SerializeField] private string PrefabName;
        [SerializeField] private float fallbackAutoDismissDelay = 5f;

        private bool tutorialTriggered = false;
        private float timer = 0f;

        void Start()
        {
            int wasShown = PlayerPrefs.GetInt(PrefabName, 0);
            Debug.Log($"[ItemTutorial] PlayerPrefs for '{PrefabName}': {wasShown}");

            if (wasShown == 1)
            {
                if (tutorialBoxUI != null)
                    tutorialBoxUI.gameObject.SetActive(false);

                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (tutorialTriggered)
            {
                timer += Time.deltaTime;

                if (Input.GetMouseButtonDown(0) || timer >= fallbackAutoDismissDelay)
                {
                    Debug.Log("[ItemTutorial] Tutorial dismissed.");
                    tutorialBoxUI.gameObject.SetActive(false);

                    if (string.IsNullOrEmpty(PrefabName))
                    {
                        Debug.LogError("[ItemTutorial] PrefabName is empty — cannot save tutorial state.");
                    }
                    else
                    {
                        PlayerPrefs.SetInt(PrefabName, 1);
                        PlayerPrefs.Save();
                    }

                    // Use coroutine instead of immediate destroy
                    StartCoroutine(DestroyAfterDelay());
                }
                return;
            }

            if (InventoryManager.Instance.HasItemWithID(itemData.itemID))
            {
                tutorialTriggered = true;
                timer = 0f;

                Debug.Log("[ItemTutorial] Showing tutorial...");

                if (tutorialBoxUI != null && !string.IsNullOrEmpty(dialogueToShow))
                {
                    tutorialBoxUI.SetTutorialText(dialogueToShow);
                    tutorialBoxUI.gameObject.SetActive(true);
                }
            }
        }

        private System.Collections.IEnumerator DestroyAfterDelay()
        {
            yield return new WaitForSeconds(0.1f); // Let PlayerPrefs flush
            Debug.Log("[ItemTutorial] Now destroying object after delay.");
            Destroy(gameObject);
        }
        
        void OnDestroy()
        {
            Debug.Log($"[ItemTutorial] OnDestroy: PlayerPrefs '{PrefabName}' = {PlayerPrefs.GetInt(PrefabName, 0)}");
        }
    }
}