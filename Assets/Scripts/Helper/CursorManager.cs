using UnityEngine;

namespace Project.Helper
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance;

        [SerializeField] private Texture2D normalCursor;
        [SerializeField] private Texture2D grabCursor;
        [SerializeField] private Texture2D doorCursor;
        [SerializeField] private Texture2D dialogueCursor;
        [SerializeField] private Texture2D putCursor;
        [SerializeField] private Texture2D ventCursor;
        [SerializeField] private Vector2 hotspot = Vector2.zero;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            SetNormalCursor(); // Set default cursor on start
        }

        public void SetNormalCursor()
        {
            Cursor.SetCursor(normalCursor, hotspot, CursorMode.Auto);
        }

        public void SetDoorCursor()
        {
            Cursor.SetCursor(doorCursor, hotspot, CursorMode.Auto);
        }

        public void SetDialogueCursor()
        {
            Cursor.SetCursor(dialogueCursor, hotspot, CursorMode.Auto);
        }

        public void SetGrabCursor()
        {
            Cursor.SetCursor(grabCursor, hotspot, CursorMode.Auto);
        }

        public void SetPutCursor()
        {
            Cursor.SetCursor(putCursor, hotspot, CursorMode.Auto);
        }

        public void SetVentCursor()
        {
            Cursor.SetCursor(ventCursor, hotspot, CursorMode.Auto);
        }
    }
}