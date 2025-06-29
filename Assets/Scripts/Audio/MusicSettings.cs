using UnityEngine;

namespace Project.Audio
{
    public class MusicSettings : MonoBehaviour
    {
        public static MusicSettings Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public bool IsMuted()
        {
            return PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        }

        public void ToggleMute()
        {
            // Delegate to the scene's music manager which has cooldown protection
            if (CustomMusicManager.Instance != null)
            {
                CustomMusicManager.Instance.ToggleMute();
            }
        }
    }
}