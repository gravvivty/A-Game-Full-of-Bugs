using System.Collections;
using Project.Dialogue.Data;
using Project.Interactable.NPCs;
using Project.Inventory;
using UnityEngine;

namespace Project.Scene.SceneControllerLvl1
{
    public class SceneControllerLvl1 : MonoBehaviour
    {
        [SerializeField] private GameObject fireGO;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject tiedRope;
        [SerializeField] private GameObject cutRope;
        [SerializeField] private GameObject omi;
        [SerializeField] private GameObject omi_noCurtain;
        [SerializeField] private DialogueData omi_panic_dialogueData;
        [SerializeField] private GameObject guard;
        [SerializeField] private DialogueData guard_panic_dialogueData;
        [SerializeField] private GameObject villageDoor;
        [SerializeField] private RuntimeAnimatorController StaffAnim;

        void Awake()
        {
            StartCoroutine(InitializeScene());
        }

        private IEnumerator  InitializeScene()
        {
            yield return null; // Wait one frame to let all Start() run

            if (PlayerPrefs.GetInt("isLit", 0) == 1)
            {
                if (villageDoor != null) villageDoor.SetActive(true);

                fireGO.SetActive(true);
                var fireAnimator = fireGO.GetComponent<Animator>();
                fireAnimator.Play("FireIdle", 0, 0f);
                Object.FindFirstObjectByType<AudioManager>().Play("fire_loop");

                if (omi.activeSelf)
                {
                    StartCoroutine(PlayOmiPanicNextFrame());
                    omi.GetComponent<NPC>().SetDialogueData(omi_panic_dialogueData);
                }

                if (omi_noCurtain.activeSelf)
                {
                    StartCoroutine(PlayNoCurtainOmiPanicNextFrame());
                    omi_noCurtain.GetComponent<NPC>().SetDialogueData(omi_panic_dialogueData);
                }

                guard.GetComponent<NPCMovement>().enabled = false;
                Animator guardAnimator = guard.GetComponent<Animator>();
                guardAnimator.Play("FireGuard", 0, 0f);
                guard.transform.localPosition = new Vector3(-0.6f, 1.6f, 0f);
                guard.GetComponent<SpriteRenderer>().sortingLayerName = "NPC";
                guard.GetComponent<NPC>().SetDialogueData(guard_panic_dialogueData);
            }

            if (PlayerPrefs.GetInt("isRopeCut", 0) == 1 &&
                !HasRequiredItem(52) &&
                !HasRequiredItem(54) &&
                !HasRequiredItem(55) &&
                !HasRequiredItem(56))
            {
               tiedRope?.SetActive(false);
               cutRope?.SetActive(true);
            }else if (PlayerPrefs.GetInt("isRopeCut", 0) == 1 &&
                HasRequiredItem(52) ||
                HasRequiredItem(54) ||
                HasRequiredItem(55) ||
                HasRequiredItem(56))
            {
                tiedRope?.SetActive(false);
                cutRope?.SetActive(false);
            }
        }
        void Update()
        {
            if (InventoryManager.Instance.HasItemWithID(56))
            {
                player.GetComponent<Animator>().runtimeAnimatorController = StaffAnim;
            }
        }

        IEnumerator wait()
        {
            yield return null;
            yield return null;
        }

        private bool HasRequiredItem(int id)
        {
            return InventoryManager.Instance.HasItemWithID(id);
        }
        
        
        IEnumerator PlayOmiPanicNextFrame()
        {
            yield return null;
            if (omi.activeSelf)
            {
                var anim = omi.GetComponent<Animator>();
                anim.enabled = true;
                anim.Play("PanicOmi", 0, 0f);
                anim.Update(0f);
            }
        }
        
        IEnumerator PlayNoCurtainOmiPanicNextFrame()
        {
            yield return null;
            if (omi_noCurtain.activeSelf)
            {
                var anim = omi_noCurtain.GetComponent<Animator>();
                anim.enabled = true;
                anim.Play("PanicOmi", 0, 0f);
                anim.Update(0f);
            }
        }
    }
}