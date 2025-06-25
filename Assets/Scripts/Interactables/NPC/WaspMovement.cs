using UnityEngine;
using System.Collections.Generic;
using Project.Inventory;
using Project.Dialogue;

namespace Project.Interactable.NPCs
{
    /// <summary>
    /// Custom movement script for the Wasp NPC.
    /// </summary>
    public class WaspMovement : MonoBehaviour
    {
        public float moveSpeed;
        public Animator animator;
        [SerializeField] private List<Vector2> pathTargets = new List<Vector2>(); // Path of targets
        private int currentTargetIndex = 0;
        private Vector2 target;
        private bool isMoving = false;
        private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float valueAboveGround = 0f;
        [SerializeField] private BoxCollider2D playerDetectionCollider;
        private bool playerDetected = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            target = rb.position;
            if (moveSpeed == 0f)
            {
                moveSpeed = 25f;
            }
            SetPath(pathTargets); // Initialize path with current targets
            StartPathing(); // Start moving if there are targets
        }

        void Update()
        {

            if (!playerDetected)
            {
                DetectPlayer(); // Check for player detection
            }
            else
            {
                foreach (var item in InventoryManager.Instance.GetAllItems())
                {
                    if (item.itemID is 56 or 59)
                    {
                        continue; // Skip items that should not be removed
                    }
                    Debug.Log($"Removing item {item.itemName} from inventory because player is detected.");
                    InventoryManager.Instance.RemoveItem(item);
                }
            }

            if (isMoving)
            {
                Vector2 direction = (target - rb.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

                // Check if we are close enough to stop
                if (Vector2.Distance(rb.position, target) < 0.05f)
                {
                    rb.MovePosition(target); // Snap to target

                    // Move to next target in path
                    currentTargetIndex++;
                    if (currentTargetIndex < pathTargets.Count)
                    {
                        target = pathTargets[currentTargetIndex];
                    }
                    else
                    {
                        // Reached end of path
                        isMoving = false;
                        animator.SetBool("Running", false);
                    }
                }

                CheckSpriteLayer();
            }
        }

        public void StartPathing()
        {
            if (pathTargets.Count > 0)
            {
                currentTargetIndex = 0;
                target = pathTargets[currentTargetIndex];
                isMoving = true;
                animator.SetBool("Running", true);
            }
        }

        public void SetPath(List<Vector2> newPath)
        {
            pathTargets = new List<Vector2>(newPath);
        }

        public void AddTargetToPath(Vector2 newTarget)
        {
            pathTargets.Add(newTarget);
        }

        public void ClearPath()
        {
            pathTargets.Clear();
            isMoving = false;
            animator.SetBool("Running", false);
        }

        // Keep the old WalkTo method for backward compatibility
        public void WalkTo(Vector2 newTarget)
        {
            pathTargets.Clear();
            pathTargets.Add(newTarget);
            StartPathing();
        }

        private void CheckSpriteLayer()
        {
            if (spriteRenderer != null)
            {
                // Set the sorting layer based on the Y position of the NPC
                if (transform.position.y < valueAboveGround)
                {
                    spriteRenderer.sortingLayerID = SortingLayer.NameToID("PlayerAbove");
                }
                else
                {
                    spriteRenderer.sortingLayerID = SortingLayer.NameToID("PlayerBelow");
                }
            }
        }

        private void DetectPlayer()
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(playerDetectionCollider.bounds.center, playerDetectionCollider.bounds.size, 0f);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    // Player detected, you can add logic here
                    Debug.Log("Player detected by Wasp NPC.");
                    playerDetected = true;
                    NPC npcComponent = gameObject.GetComponent<NPC>();
                    if (npcComponent != null && npcComponent.dialogueData != null)
                    {
                        // Start dialogue immediately through DialogueManager
                        DialogueManager.Instance.StartDialogue(npcComponent.dialogueData, npcComponent.initialDialogueID, npcComponent);
                    }
                }
            }
        }
    }
}