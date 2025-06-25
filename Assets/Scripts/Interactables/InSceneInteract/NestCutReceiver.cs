using System.Collections;
using Project.Inventory;
using Project.Player;
using UnityEngine;

namespace Project.Interactable.InSceneInteract
{
    public class NestCutReceiver : ItemReceiver
    {
        [SerializeField] private GameObject player;

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
                    // Start cutting animation
                    StartCoroutine(TriggerAnimation());
                }


                // CUSTOM LOGIC ----
            }

            Debug.Log("Can't use this item on the table.");
            return false;
        }

        public IEnumerator TriggerAnimation()
        {
            player.GetComponent<PlayerMovement>().MovePlayerTo(new Vector2(-2f, -2.5f));
            yield return new WaitForSeconds(1.5f);
            player.GetComponent<Animator>().SetBool("Cutting", true);
            player.GetComponent<SpriteRenderer>().flipX = false;
            yield return new WaitForSeconds(1f);
            player.GetComponent<Animator>().SetBool("Cutting", false);
        }
    }
}
