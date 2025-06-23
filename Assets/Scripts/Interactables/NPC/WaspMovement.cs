using UnityEngine;
using System.Collections.Generic;

namespace Project.Interactable.NPCs
{
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
    }
}