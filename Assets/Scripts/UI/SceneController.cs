using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GoToHowToPlay()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

    public void GoToChangeControl()
    {
        SceneManager.LoadScene("ChangeControlScene");
    }
}
