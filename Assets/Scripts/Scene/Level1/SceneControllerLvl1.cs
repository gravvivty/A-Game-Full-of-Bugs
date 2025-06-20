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
            InitializeScene();
        }

        private void InitializeScene()
        {

            if (PlayerPrefs.GetInt("isLit", 0) == 1)
            {
                if (villageDoor != null) villageDoor.SetActive(true);

                fireGO.SetActive(true);
                var fireAnimator = fireGO.GetComponent<Animator>();
                fireAnimator.Play("FireIdle", 0, 0f);

                if (omi.activeSelf)
                {
                    Animator omiAnimator = omi.GetComponent<Animator>();
                    Debug.Log("PanicOmi? " + omiAnimator.HasState(0, Animator.StringToHash("PanicOmi")));
                    omiAnimator.Play("PanicOmi", 0, 0f);
                    omi.GetComponent<NPC>().SetDialogueData(omi_panic_dialogueData);
                }

                if (omi_noCurtain.activeSelf)
                {
                    Animator omiNoCurtainAnimator = omi_noCurtain.GetComponent<Animator>();
                    omiNoCurtainAnimator.Play("PanicOmi", 0, 0f);
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
    }
}