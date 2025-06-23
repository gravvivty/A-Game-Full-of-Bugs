using UnityEngine;

namespace Project.Helper
{
    /// <summary>
    /// This class is used to create an outline effect for a sprite.
    /// </summary>
    public class SpriteOutline : MonoBehaviour
    {
        [SerializeField] private Color outlineColor = new Color(0 ,1, 0, 1);
        [SerializeField] private float outlineSize = 1.05f;

        private SpriteRenderer outlineRenderer;
        private SpriteRenderer mainRenderer;

        /// <summary>
        /// Initializes the outline by creating a child object and setting up the sprite renderer.
        /// Copy the sprite and sorting properties from the main sprite renderer.
        /// </summary>
        private void Start()
        {
            // Create child object for outline
            GameObject outlineObject = new GameObject("Outline");
            outlineObject.transform.parent = transform;
            outlineObject.transform.localPosition = Vector3.zero;
            outlineObject.transform.localRotation = Quaternion.identity;
            //outlineObject.transform.localScale = Vector3.one;

            // Add and setup outline sprite renderer
            outlineRenderer = outlineObject.AddComponent<SpriteRenderer>();
            mainRenderer = GetComponent<SpriteRenderer>();
            outlineRenderer.material = new Material(Shader.Find("Unlit/SolidColorShader"));

            // Copy sprite and sorting properties
            outlineRenderer.sprite = mainRenderer.sprite;
            outlineRenderer.sortingLayerID = mainRenderer.sortingLayerID;
            outlineRenderer.sortingOrder = mainRenderer.sortingOrder - 1;

            // Set initial state
            outlineRenderer.gameObject.SetActive(false);
        }

        /// <summary>
        /// Shows or hides the outline based on the provided boolean value.
        /// </summary>
        public void ShowOutline(bool show)
        {
            if (outlineRenderer != null)
            {
                outlineRenderer.gameObject.SetActive(show);
                if (show)
                {
                    outlineRenderer.transform.localScale = Vector3.one * outlineSize;
                    outlineRenderer.color = outlineColor;
                }
            }
        }
        
        // Updates outline with animations + layer
        private void LateUpdate()
        {
            if (outlineRenderer != null && mainRenderer != null && outlineRenderer.gameObject.activeSelf)
            {
                // Sync sprite
                outlineRenderer.sprite = mainRenderer.sprite;

                // Sync sorting layer and order
                outlineRenderer.sortingLayerID = mainRenderer.sortingLayerID;
                outlineRenderer.sortingOrder = mainRenderer.sortingOrder - 1;
            }
        }
    }
}