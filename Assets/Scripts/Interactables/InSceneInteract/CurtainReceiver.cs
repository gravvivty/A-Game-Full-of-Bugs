using System.Collections;
using Project.Dialogue.Data;
using Project.Interactable.NPCs;
using Project.Inventory;
using Project.Player;
using UnityEngine;

namespace Project.Interactable.InSceneInteract
{
    public class CurtainReceiver : ItemReceiver
    {
        public GameObject fire;
        public GameObject ray;
        public GameObject player;
        public GameObject guard;
        public GameObject omi_curtain;
        public DialogueData omi_panic_dialogueData;
        public DialogueData guard_panic_dialogueData;
        public GameObject omi;
        public GameObject villageDoor;
        public GameObject playerHomeDoor;
        public override bool TryUseItem(ItemData draggedItem)
        {
            if (PlayerPrefs.GetInt("isLit", 0) == 1)
            {
                Debug.Log("IT IS ALREADY BURNING!!!!");
            }
            else
            {
                // Check for a valid combination
                if (draggedItem.CanCombine(itemRepresentation.itemID))
                {
                    ItemData result = draggedItem.GetCombinationResult(itemRepresentation.itemID);
                    Debug.Log($"Combined {draggedItem.itemName} with {itemRepresentation.itemName} to get {result.itemName}");

                    // CUSTOM LOGIC -----
                    if (spriteRenderer != null && draggedItem.itemID == 56)
                    {
                        itemRepresentation = result;
                        player.GetComponent<PlayerMovement>().allowInput = false;

                        StartCoroutine(activateRay());

                        StartCoroutine(activateMoving());

                        PlayerPrefs.SetInt("isLit", 1);
                        PlayerPrefs.Save();

                        Debug.Log("IT BUUUURNS!!!");
                        return true;
                    }
                    // CUSTOM LOGIC ----
                    Debug.Log("Can't use this item on the Curtain.");
                }
            }
            return false;
        }

        private IEnumerator wait()
        {
            yield return new WaitForSeconds(2f);
        }
        private IEnumerator activateMoving()
        {
            yield return new WaitForSeconds(6f);
            player.GetComponent<SpriteRenderer>().flipX = false;
            player.GetComponent<Animator>().SetBool("Burning", true);
            yield return new WaitForSeconds(2f);
            omi_curtain.GetComponent<Animator>().SetBool("Panic", true);
            omi_curtain.GetComponent<NPC>().SetDialogueData(omi_panic_dialogueData);
            yield return new WaitForSeconds(1f);
            guard.GetComponent<NPCMovement>().WalkTo(new Vector2(-10, -3.5f));
            guard.GetComponent<NPC>().SetDialogueData(guard_panic_dialogueData);
            villageDoor.SetActive(true);
            playerHomeDoor.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<Animator>().SetBool("Burning", false);
            player.GetComponent<PlayerMovement>().allowInput = true;
        }

        private IEnumerator activateRay()
        {
            Vector3 burnPosition = new Vector3(2.3f, -6f);
            yield return StartCoroutine(MovePlayerToBurnPosition(burnPosition, 1f));
            yield return new WaitForSeconds(2f);

            ray.SetActive(true);

            Animator rayAnimator = ray.GetComponent<Animator>();
            rayAnimator.SetTrigger("Start");

            // Wait for the start animation to finish (adjust time based on your animation length)
            yield return new WaitForSeconds(1f); // adjust if Start anim length differs

            rayAnimator.SetBool("Idle", true); // Start idle loop
            yield return new WaitForSeconds(2f); // Idle for 3 seconds

            fire.SetActive(true);
            fire.GetComponent<Animator>().SetBool("isLit", true);

            rayAnimator.SetBool("Idle", false);
            rayAnimator.SetTrigger("Stop"); // Play stop animation

            // Wait for the stop animation to finish (adjust based on your animation)
            yield return new WaitForSeconds(1f); // adjust if Stop anim length differs

            ray.SetActive(false); // Hide ray after animation sequence
        }

        private IEnumerator MovePlayerToBurnPosition(Vector3 targetPosition, float waitAfterArrive = 0.5f)
        {
            player.GetComponent<PlayerMovement>().MovePlayerTo(targetPosition);

            // Position check loop - not needed iirc
            while (Vector2.Distance(player.transform.position, targetPosition) > 0.1f)
            {
                yield return null;
            }

            // Player has arrived
            player.GetComponent<SpriteRenderer>().flipX = false;
            player.GetComponent<Animator>().SetBool("Burning", true);

            yield return new WaitForSeconds(waitAfterArrive);
        }
    }
}