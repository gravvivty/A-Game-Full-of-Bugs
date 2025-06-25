using Project.Interactable;
using UnityEngine;
using Project.Audio;

namespace Project.Interactable.VentSystem
{
    public class Vent : Interactables
    {
        protected override void Interact()
        {
            VentTeleportSystem.Instance.EnterVent(gameObject);
            FindFirstObjectByType<CustomAudioManager>().Play("vent");
        }
    }
}

