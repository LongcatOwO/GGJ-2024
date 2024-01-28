using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteSceneController : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneController.Instance.GoToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        SceneController.Instance.ReturnToMainMenu();
    }

    public void GoToHowToPlay()
    {
        SceneController.Instance.GoToHowToPlay();
    }

    public void GoToChangeControl()
    {
        SceneController.Instance.GoToChangeControl();
    }
}
