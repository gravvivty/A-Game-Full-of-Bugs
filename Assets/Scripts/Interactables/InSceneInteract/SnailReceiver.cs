using Project.Dialogue.Data;
using Project.Interactable.NPCs;
using Project.Inventory;
using UnityEngine;
using Project.Audio;

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
                    spriteRenderer.sprite = result.icon;
                    snailNPC.GetComponent<Animator>().SetBool("Cracked", true);

                    Object.FindFirstObjectByType<CustomAudioManager>().Play("motor_snail");

                    return true;
                }
            }

            Debug.Log("Can't use this item on the Snail.");
            Object.FindFirstObjectByType<CustomAudioManager>().Play("wrong");
            return false;
        }
    }
}
