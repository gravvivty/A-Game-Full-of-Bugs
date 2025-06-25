using System.Collections;
using Project.Dialogue.Data;
using Project.Interactable.NPCs;
using Project.Inventory;
using UnityEngine;

namespace Project.Interactable.InSceneInteract
{
    public class AntReceiver : ItemReceiver
    {
        [SerializeField] private GameObject useTablePrompt;

        public override bool TryUseItem(ItemData draggedItem)
        {
            // CUSTOM LOGIC -----
            if (spriteRenderer != null && (draggedItem.itemID == 60 || draggedItem.itemID == 63 || draggedItem.itemID == 68))
            {
                StartCoroutine(enablePrompt());
                InventoryManager.Instance.AddItem(draggedItem);
                return true;
            }

            Debug.Log("Can't use this item on the ANT.");
            return false;
        }

        IEnumerator enablePrompt()
        {
            useTablePrompt.SetActive(true);
            yield return new WaitForSeconds(2f);
            useTablePrompt.SetActive(false);
        }
    }
}
