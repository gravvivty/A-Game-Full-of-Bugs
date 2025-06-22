using Project.Helper;
using Project.Inventory;
using UnityEngine;

namespace Project.Interactable.InSceneInteract
{
    public class NestHatchReceiver : ItemReceiver
    {
        [SerializeField] private GameObject nestHatchClosed;
        [SerializeField] private GameObject nestHatchOpen;

        public override bool TryUseItem(ItemData draggedItem)
        {
            // Check for a valid combination
            if (draggedItem.CanCombine(itemRepresentation.itemID))
            {
                ItemData result = draggedItem.GetCombinationResult(itemRepresentation.itemID);
                Debug.Log($"Combined {draggedItem.itemName} with {itemRepresentation.itemName} to get {result.itemName}");

                // CUSTOM LOGIC -----
                if (spriteRenderer != null && draggedItem.itemID == 83)
                {
                    InventoryManager.Instance.RemoveItem(draggedItem);
                    nestHatchOpen.SetActive(true);
                    nestHatchClosed.SetActive(false);
                    return true;
                }

                // CUSTOM LOGIC ----
            }

            Debug.Log("Can't use this item on the Nest Door.");
            CursorManager.Instance.SetPutCursor();
            return false;
        }
    }
}
