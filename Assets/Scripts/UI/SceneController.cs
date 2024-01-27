using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private string openAdditiveSceneName;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.UnloadSceneAsync(openAdditiveSceneName);
    }

    public void GoToHowToPlay()
    {
        openAdditiveSceneName = "HowToPlayScene";

        SceneManager.LoadScene(openAdditiveSceneName, LoadSceneMode.Additive);
    }

    public void GoToChangeControl()
    {
        openAdditiveSceneName = "ChangeControlScene";

        SceneManager.LoadScene(openAdditiveSceneName, LoadSceneMode.Additive);
    }
}
