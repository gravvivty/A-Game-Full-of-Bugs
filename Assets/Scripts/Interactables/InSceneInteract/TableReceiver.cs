using Project.Dialogue.Data;
using Project.Interactable.NPCs;
using Project.Inventory;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Interactable.InSceneInteract
{
    public class TableReceiver : ItemReceiver
    {
        [SerializeField] private GameObject[] honeyObjects;
        private int honeyCount;

        // Dialogue related
        [SerializeField] private NPC ant;
        [SerializeField] private DialogueData antAllHoneyDialogue;
        [SerializeField] private GameObject player;
        [SerializeField] private DialogueData antMirrorHoneyDialogue;

        public override bool TryUseItem(ItemData draggedItem)
        {
            // Check for a valid combination
            if (draggedItem.CanCombine(itemRepresentation.itemID))
            {
                ItemData result = draggedItem.GetCombinationResult(itemRepresentation.itemID);
                Debug.Log($"Combined {draggedItem.itemName} with {itemRepresentation.itemName} to get {result.itemName}");

                // CUSTOM LOGIC -----
                if (spriteRenderer != null && (draggedItem.itemID == 60 || draggedItem.itemID == 63 || draggedItem.itemID == 68))
                {
                    honeyCount++;
                    honeyObjects[honeyCount - 1].SetActive(true);
                    InventoryManager.Instance.RemoveItem(draggedItem);

                    if (honeyCount >= 3)
                    {
                        ant.SetDialogueData(antAllHoneyDialogue);
                    }
                    return true;
                }

                if (spriteRenderer != null && draggedItem.itemID == 56 && honeyCount >= 3)
                {
                    // TODO: Make player walk to right position
                    player.GetComponent<Animator>().SetBool("Mirroring", true);
                    ant.SetDialogueData(antMirrorHoneyDialogue);
                    // TODO: stop animation when dialogue ends
                }
                // CUSTOM LOGIC ----
            }

            Debug.Log("Can't use this item on the table.");
            return false;
        }
    }
}
