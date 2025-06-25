using Project.Helper;
using Project.Inventory;
using UnityEngine;

namespace Project.Interactable.InSceneInteract
{
    public class OvenReceiver : ItemReceiver
    {
        public override bool TryUseItem(ItemData draggedItem)
        {
            // Check for a valid combination
            if (draggedItem.CanCombine(itemRepresentation.itemID))
            {
                ItemData result = draggedItem.GetCombinationResult(itemRepresentation.itemID);
                Debug.Log($"Combined {draggedItem.itemName} with {itemRepresentation.itemName} to get {result.itemName}");

                // CUSTOM LOGIC -----
                if (spriteRenderer != null && draggedItem.itemID == 75)
                {
                    InventoryManager.Instance.RemoveItem(draggedItem);
                    InventoryManager.Instance.AddItem(result);
                    return true;
                }

                // CUSTOM LOGIC ----
            }

            Debug.Log("Can't use this item on the Nest Door.");
            FindFirstObjectByType<AudioManager>().Play("wrong");
            CursorManager.Instance.SetPutCursor();
            return false;
        }
    }
}
