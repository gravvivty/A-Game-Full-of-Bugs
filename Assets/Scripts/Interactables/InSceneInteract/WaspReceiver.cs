using System.Collections;
using Project.Helper;
using Project.Interactable.NPCs;
using Project.Inventory;
using Project.Player;
using UnityEngine;
using Project.Audio;

namespace Project.Interactable.InSceneInteract
{
    public class WaspReceiver : ItemReceiver
    {
        [SerializeField] private GameObject wasp;
        [SerializeField] private GameObject key;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject positionDialogue;
        [SerializeField] private GameObject window;

        public override bool TryUseItem(ItemData draggedItem)
        {
            // Check for a valid combination
            if (draggedItem.CanCombine(itemRepresentation.itemID))
            {
                ItemData result = draggedItem.GetCombinationResult(itemRepresentation.itemID);
                Debug.Log($"Combined {draggedItem.itemName} with {itemRepresentation.itemName} to get {result.itemName}");

                if (player.transform.position.x < .7f)
                {
                    positionDialogue.SetActive(true);
                    StartCoroutine(wait());
                    return false;
                }

                // CUSTOM LOGIC -----
                if (spriteRenderer != null && draggedItem.itemID == 56)
                {
                    StartCoroutine(StartBlindingSequence());
                    return true;
                }

                // CUSTOM LOGIC ----
            }

            Debug.Log("Can't use this item on the Nest Door.");
            FindFirstObjectByType<CustomAudioManager>().Play("wrong");
            CursorManager.Instance.SetPutCursor();
            return false;
        }

        private IEnumerator wait()
        {
            yield return new WaitForSeconds(2f);
            positionDialogue.SetActive(false);
        }

        private IEnumerator StartBlindingSequence()
        {
            player.GetComponent<PlayerMovement>().MovePlayerTo(new Vector2(6f, 1.4f));
            yield return new WaitForSeconds(1f);

            player.GetComponent<Animator>().SetBool("Burning", true);
            window.GetComponent<Animator>().SetBool("Blinding", true);
            player.GetComponent<SpriteRenderer>().flipX = true;

            yield return new WaitForSeconds(1.5f);
            player.GetComponent<Animator>().SetBool("Burning", false);
            window.GetComponent<Animator>().SetBool("Blinding", false);

            wasp.GetComponent<Animator>().SetBool("Blinded", true);
            wasp.GetComponent<BoxCollider2D>().enabled = false;
            key.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
