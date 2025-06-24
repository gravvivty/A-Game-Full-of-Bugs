using Project.Interactable;
using UnityEngine;

namespace Project.Interactable.VentSystem
{
    public class Vent : Interactables
    {
        protected override void Interact()
        {
            VentTeleportSystem.Instance.EnterVent(gameObject);
        }
    }
}

