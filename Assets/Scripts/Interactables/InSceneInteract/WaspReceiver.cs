using Project.Helper;
using Project.Interactable.NPCs;
using Project.Inventory;
using UnityEngine;

namespace Project.Interactable.InSceneInteract
{
    public class WaspReceiver : ItemReceiver
    {
        [SerializeField] private GameObject wasp;
        [SerializeField] private GameObject key;
        public override bool TryUseItem(ItemData draggedItem)
        {
            // Check for a valid combination
            if (draggedItem.CanCombine(itemRepresentation.itemID))
            {
                ItemData result = draggedItem.GetCombinationResult(itemRepresentation.itemID);
                Debug.Log($"Combined {draggedItem.itemName} with {itemRepresentation.itemName} to get {result.itemName}");

                // CUSTOM LOGIC -----
                if (spriteRenderer != null && draggedItem.itemID == 56)
                {
                    wasp.GetComponent<Animator>().SetBool("Blinded", true);
                    wasp.GetComponent<BoxCollider2D>().enabled = false;
                    key.GetComponent<BoxCollider2D>().enabled = true;
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
