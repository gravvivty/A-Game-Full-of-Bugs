using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public float timeToNextScene = 10f;
    public string sceneToLoad;
    private float timer = 0f;
    private bool videoFinished = false;

    void Update()
    {
        if (videoFinished) return;

        timer += Time.deltaTime;

        if (timer >= timeToNextScene)
        {
            videoFinished = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}