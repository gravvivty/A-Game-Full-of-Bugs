using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Project.Audio;

public class SettingsMenuManager : MonoBehaviour
{
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;

    void Start()
    {
        // Set up the sound button
        if (soundButton != null)
        {
            soundButton.onClick.AddListener(ToggleSound);
            UpdateSoundButtonDisplay();
        }
    }

    public void ToggleSound()
    {
        Debug.Log("ToggleSound called");
        if (MusicSettings.Instance != null)
        {
            Debug.Log("Toggling sound");
            MusicSettings.Instance.ToggleMute();
            UpdateSoundButtonDisplay();
        }
    }

    private void UpdateSoundButtonDisplay()
    {
        if (MusicSettings.Instance != null && soundButtonText != null)
        {
            bool isMuted = MusicSettings.Instance.IsMuted();
            soundButtonText.text = isMuted ? "Sound: Off" : "Sound: On";
        }
    }
}