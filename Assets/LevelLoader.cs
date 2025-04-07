using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadSceneByIndex(int sceneIndex) => SceneManager.LoadScene(sceneIndex);

    public void Quit()
    {
        Application.Quit();
    }
}

