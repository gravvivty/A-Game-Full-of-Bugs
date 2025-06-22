using Project.Dialogue.Data;
using Project.Interactable.NPCs;
using Project.Inventory;
using UnityEngine;

namespace Project.Interactable.InSceneInteract
{
    public class SnailReceiver : ItemReceiver
    {
        [SerializeField] private NPC snailNPC;
        [SerializeField] private DialogueData newSnailDialogue;
        [SerializeField] private Transform walkTarget;

        public override bool TryUseItem(ItemData draggedItem)
        {
            ItemData result = null; // hier deklariert

            if (draggedItem.CanCombine(itemRepresentation.itemID))
            {
                result = draggedItem.GetCombinationResult(itemRepresentation.itemID);
                Debug.Log($"Combined {draggedItem.itemName} with {itemRepresentation.itemName} to get {result.itemName}");

                if (spriteRenderer != null && draggedItem.itemID == 61)
                {
                    InventoryManager.Instance.RemoveItem(draggedItem);
                    snailNPC.SetDialogueData(newSnailDialogue);
                    spriteRenderer.sprite = result.icon; // funktioniert jetzt

                    var animator = snailNPC.GetComponent<Animator>();
                    if (animator != null)
                    {
                        animator.SetBool("IsCracked", true);
                    }

                    var movement = snailNPC.GetComponent<NPCMovement>();
                    if (movement != null)
                    {
                        movement.WalkTo(walkTarget.position);
                    }

                    return true;
                }
            }

            Debug.Log("Can't use this item on the Snail.");
            return false;
        }
    }
}
