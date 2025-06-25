using Project.Helper;
using Project.Inventory;
using UnityEngine;

namespace Project.Interactable.InSceneInteract
{
    public class NestDoorReceiver : ItemReceiver
    {
        [SerializeField] private GameObject NestDoorClosed;
        [SerializeField] private GameObject NestDoorOpen;
        [SerializeField] private GameObject GroundNestDoorClosed;
        [SerializeField] private GameObject GroundNestDoorOpen;
        public override bool TryUseItem(ItemData draggedItem)
        {
            // Check for a valid combination
            if (draggedItem.CanCombine(itemRepresentation.itemID))
            {
                ItemData result = draggedItem.GetCombinationResult(itemRepresentation.itemID);
                Debug.Log($"Combined {draggedItem.itemName} with {itemRepresentation.itemName} to get {result.itemName}");

                // CUSTOM LOGIC -----
                if (spriteRenderer != null && draggedItem.itemID == 74)
                {
                    InventoryManager.Instance.RemoveItem(draggedItem);
                    InventoryManager.Instance.AddItem(result);
                    return true;
                }

                if (spriteRenderer != null && draggedItem.itemID == 80)
                {
                    InventoryManager.Instance.RemoveItem(draggedItem);
                    NestDoorOpen.SetActive(true);
                    GroundNestDoorClosed.SetActive(false);
                    GroundNestDoorOpen.SetActive(true);
                    NestDoorClosed.SetActive(false);
                    return true;
                }

                // CUSTOM LOGIC ----
            }

            Debug.Log("Can't use this item on the Nest Door. Dragged item: " + draggedItem.itemID);
            FindFirstObjectByType<AudioManager>().Play("wrong");
            CursorManager.Instance.SetPutCursor();
            return false;
        }
    }
}
