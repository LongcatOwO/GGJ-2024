using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreenAnimator : MonoBehaviour
{
    [SerializeField] Texture2D[] Textures;
    [SerializeField] float AnimationSpeed = 20f;
    [SerializeField] int NextScreenTriggerFrame = 25;

    [SerializeField] RawImage RawImage;
    [SerializeField] Slider StartButtonSlider;
    [SerializeField] TitleStartButton StartButton;

    private float _heldTime;
    private int _currentFrameIndex;

    private void Update()
    {
        if (StartButton.IsButtonHeld)
        {
            _heldTime += Time.deltaTime;
            UpdateLoadValue(_heldTime);
        } 
        else
        {
            NextSceneIfCharged();
        }
    }


    private void NextSceneIfCharged()
    {
        if (_currentFrameIndex >= NextScreenTriggerFrame - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 
        else
        {
            if (_currentFrameIndex > 0)
            {
                Debug.Log("Stopped at frame " + _currentFrameIndex);
            }
            _heldTime = 0;
            UpdateLoadValue(_heldTime);
        }
    }

    private void UpdateLoadValue(float heldTime)
    {
        _currentFrameIndex = Mathf.Min(Mathf.FloorToInt(heldTime * AnimationSpeed), Textures.Length - 1);
        RawImage.texture = Textures[_currentFrameIndex];
        StartButtonSlider.value = heldTime * AnimationSpeed / (NextScreenTriggerFrame - 1);
    }
}
