using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;
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
            moveSpeed = 5f;
        }
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
                isMoving = false;
                animator.SetBool("Running", false); // STOP walk animation
            }
            
            CheckSpriteLayer();
        }
    }

    public void WalkTo(Vector2 newTarget)
    {
        target = newTarget;
        isMoving = true;

        if (animator != null)
        {
            animator.SetBool("Running", true);
        }
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