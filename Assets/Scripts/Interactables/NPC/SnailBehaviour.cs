using UnityEngine;
using System.Collections;

public class SnailBehaviour : MonoBehaviour
{
    public Transform walkTarget;
    public float moveSpeed = 3f;
    public Animator animator;

    private Rigidbody2D rb;
    private bool isMoving = false;

    private bool hasReceivedMotorPart = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        isMoving = false;

        if (animator != null)
        {
            animator.SetBool("IsTalking", false);
            animator.SetInteger("SnailState", 0); // idle
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector2 direction = ((Vector2)walkTarget.position - rb.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

            if (Vector2.Distance(rb.position, walkTarget.position) < 0.05f)
            {
                rb.MovePosition(walkTarget.position);
                isMoving = false;

                // Schnecke angekommen — zurück zu idle oder tree je nach Logik
                animator.SetInteger("SnailState", 0); // idle
            }
        }
    }

    public void OnMotorPartReceived()
    {
        if (hasReceivedMotorPart) return;
        hasReceivedMotorPart = true;

        Debug.Log("Motor läuft! Die Schnecke ist jetzt cracked.");
        animator.SetInteger("SnailState", 1); // cracked

        // Optional: Warte z.B. kurz bevor Schnecke losläuft
        StartCoroutine(StartMovingAfterDelay());
    }

    private IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        Debug.Log("Schnecke beginnt zu laufen (tree).");
        animator.SetInteger("SnailState", 2); // tree

        isMoving = true;
    }

    // Diese Methode kannst du vom NPC-Script aufrufen, wenn man nochmal redet
    public void OnStartMoving()
    {
        if (hasReceivedMotorPart)
        {
            animator.SetInteger("SnailState", 2); // tree
            isMoving = true;
        }
    }

    // Diese Methoden für Talking-Animation (vom NPC)
    public void StartTalking()
    {
        animator.SetBool("IsTalking", true);
    }

    public void StopTalking()
    {
        animator.SetBool("IsTalking", false);
    }
}
