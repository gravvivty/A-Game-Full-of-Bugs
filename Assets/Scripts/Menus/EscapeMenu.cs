using UnityEngine.SceneManagement;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel; // das Panel mit Resume/Menu etc.
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject confirmPanel;

    private bool isVisible = false;

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        confirmPanel.SetActive(false); // Sicherstellen, dass das Dialogfenster unsichtbar ist
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isVisible = !isVisible;

        canvasGroup.alpha = isVisible ? 1 : 0;
        canvasGroup.interactable = isVisible;
        canvasGroup.blocksRaycasts = isVisible;

        Time.timeScale = isVisible ? 0 : 1;
    }

    public void ResumeGame()
    {
        isVisible = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        Time.timeScale = 1;
    }

    public void OnQuitButtonClicked()
    {
        mainPanel.SetActive(false);         // Escape-Menü ausblenden
        confirmPanel.SetActive(true);       // Bestätigungsfenster einblenden
    }


    public void OnConfirmYes()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu"); // oder "MainMenu", je nach Szenenname
    }

    public void OnConfirmNo()
    {
        confirmPanel.SetActive(false);      // Bestätigungsfenster ausblenden
        mainPanel.SetActive(true);          // Escape-Menü wieder anzeigen
    }
}
