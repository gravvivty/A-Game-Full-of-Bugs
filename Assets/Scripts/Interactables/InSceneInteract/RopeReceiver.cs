using System.Collections;
using Project.Dialogue.Data;
using Project.Interactable.NPCs;
using Project.Inventory;
using UnityEngine;
using Project.Audio;

namespace Project.Interactable.InSceneInteract
{
    public class RopeReceiver : ItemReceiver
    {
        public GameObject tiedRope;
        public GameObject cutRope;
        
        public override bool TryUseItem(ItemData draggedItem)
        {
            // Prevent re-cutting if already cut
            if (PlayerPrefs.GetInt("isRopeCut", 0) == 1)
            {
                Debug.Log("Rope already cut. Ignoring interaction.");
                return false;
            }

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
                    Destroy(tiedRope);
                    this.enabled = false;
                    PlayerPrefs.SetInt("isRopeCut", 1);
                    PlayerPrefs.Save();

                    return true;
                }

                Debug.Log("Can't use this item on the Rope.");
                FindFirstObjectByType<CustomAudioManager>().Play("wrong");
            }

            return false;
        }
    }
}