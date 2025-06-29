using UnityEngine;

namespace Project.Audio
{
    /// <summary>
    /// Manages background music for individual scenes.
    /// </summary>
    public class CustomMusicManager : MonoBehaviour
    {
        public static CustomMusicManager Instance;

        [SerializeField] private AudioSource audioSource;
        private float lastToggleTime = 0f;
        private const float toggleCooldown = 0.2f;

        private void Awake()
        {
            // Simple singleton for the current scene only (no DontDestroyOnLoad)
            if (Instance == null)
            {
                Instance = this;

                // Get or add AudioSource component
                if (audioSource == null)
                    audioSource = GetComponent<AudioSource>();

                // Load saved mute state
                LoadMuteState();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            // Start playing music if not muted
            if (!IsMuted() && audioSource.clip != null)
            {
                PlayMusic();
            }
        }

        public void ToggleMute()
        {
            // Prevent double calls
            if (Time.time - lastToggleTime < toggleCooldown)
            {
                Debug.Log("ToggleMute called too quickly, ignoring...");
                return;
            }

            lastToggleTime = Time.time;

            bool currentMuteState = IsMuted();
            Debug.Log($"Current mute state: {currentMuteState}, switching to: {!currentMuteState}");
            SetMuted(!currentMuteState);
        }

        public void SetMuted(bool muted)
        {
            Debug.Log($"Setting music muted state to: {muted}");
            audioSource.mute = muted;
            PlayerPrefs.SetInt("MusicMuted", muted ? 1 : 0);
            PlayerPrefs.Save();

            // If unmuting and we have music, start playing
            if (!muted && audioSource.clip != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        public bool IsMuted()
        {
            return audioSource.mute;
        }

        public void PlayMusic()
        {
            if (!audioSource.isPlaying && !IsMuted() && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }

        public void StopMusic()
        {
            audioSource.Stop();
        }

        private void LoadMuteState()
        {
            bool savedMuteState = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
            audioSource.mute = savedMuteState;
        }
    }
}