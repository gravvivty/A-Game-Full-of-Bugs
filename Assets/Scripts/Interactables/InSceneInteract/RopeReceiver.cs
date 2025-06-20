using System.Collections;
using Project.Dialogue.Data;
using Project.Interactable.NPCs;
using Project.Inventory;
using UnityEngine;

namespace Project.Interactable.InSceneInteract
{
    public class RopeReceiver : ItemReceiver
    {
        public GameObject tiedRope;
        public GameObject cutRope;
        public override bool TryUseItem(ItemData draggedItem)
        {
            // Check for a valid combination
            if (draggedItem.CanCombine(itemRepresentation.itemID))
            {
                ItemData result = draggedItem.GetCombinationResult(itemRepresentation.itemID);
                Debug.Log($"Combined {draggedItem.itemName} with {itemRepresentation.itemName} to get {result.itemName}");
                
                // CUSTOM LOGIC -----
                if (spriteRenderer != null && draggedItem.itemID == 59)
                {
                    itemRepresentation = null;
                    cutRope.SetActive(true);
                    PlayerPrefs.SetInt("isRopeCut", 1);
                    PlayerPrefs.Save();
                    
                    gameObject.SetActive(false);
                    return true;
                }
                // CUSTOM LOGIC ----
                Debug.Log("Can't use this item on the Rope.");
            }
            return false;
        }
    }
}