using UnityEngine;

namespace Project.Interactable.VentSystem
{
    public class VentDestination : MonoBehaviour
    {
        [SerializeField] private Transform teleportPoint;

        public Transform GetTeleportPoint()
        {
            return teleportPoint;
        }
    }
}