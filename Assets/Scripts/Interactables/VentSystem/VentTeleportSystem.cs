using UnityEngine;
using Project.Player;
using System.Collections;
using UnityEngine.EventSystems;

namespace Project.Interactable.VentSystem
{
    public class VentTeleportSystem : MonoBehaviour
    {
        public static VentTeleportSystem Instance;

        private PlayerMovement player;
        private bool awaitingDestination = false;
        private SpriteRenderer playerSprite;
        private Collider2D playerCollider;

        private float teleportCooldown = 0f;
        private const float teleportCooldownTime = 0.2f;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            playerSprite = player.GetComponent<SpriteRenderer>();
            playerCollider = player.GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (teleportCooldown > 0f)
                teleportCooldown -= Time.deltaTime;

            // Only allow vent selection if we're awaiting a destination
            if (!awaitingDestination) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider != null)
                {
                    VentDestination destination = hit.collider.GetComponent<VentDestination>();

                    if (destination == null)
                    {
                        var vent = hit.collider.GetComponent<Vent>();
                        if (vent != null)
                            destination = vent.GetComponentInChildren<VentDestination>();
                    }

                    if (destination != null)
                    {
                        player.transform.position = destination.GetTeleportPoint().position;
                        playerSprite.enabled = true;
                        playerCollider.enabled = true;
                        awaitingDestination = false;

                        // Start cooldown *after* teleport
                        teleportCooldown = teleportCooldownTime;
                    }
                    else
                    {
                        Debug.Log("Clicked object is not a valid Vent or VentDestination.");
                    }
                }
            }
        }

        public void EnterVent(GameObject vent)
        {
            if (awaitingDestination)
            {
                Debug.Log("Already awaiting a vent destination.");
                return;
            }

            if (teleportCooldown > 0f)
            {
                Debug.Log("Teleport cooldown active.");
                return;
            }

            float allowedDistance = 5f;
            float distance = Vector2.Distance(player.transform.position, vent.transform.position);

            if (distance > allowedDistance)
            {
                Debug.Log("Too far to enter vent.");
                return;
            }

            // Begin venting
            playerSprite.enabled = false;
            playerCollider.enabled = false;
            awaitingDestination = true;
        }
    }
}