using Project.Interactable;
using UnityEngine;
using Project.Audio;
using Project.Dialogue;

namespace Project.Interactable.VentSystem
{
    public class Vent : Interactables
    {
        protected override void Interact()
        {
            DialogueManager.Instance.EndDialogue(); // End any ongoing dialogue when picking up an item
            VentTeleportSystem.Instance.EnterVent(gameObject);
            FindFirstObjectByType<CustomAudioManager>().Play("vent");
        }
    }
}

