using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimator : MonoBehaviour
{
    [SerializeField] Texture2D[] Textures;
    [SerializeField] float AnimationSpeed = 20f;
    [SerializeField] int NextScreenTriggerFrame = 25;

    private RawImage _rawImage;

    private bool _holdingInput;
    private float _heldTime;
    private int _currentFrameIndex;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInputHandler.Instance.OnAttackInputDown += () => _holdingInput = true;
        PlayerInputHandler.Instance.OnAttackInputUp += () => _holdingInput = false;

        _rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        if (_holdingInput)
        {
            _heldTime += Time.deltaTime;

            _currentFrameIndex = Mathf.Min(Mathf.FloorToInt(_heldTime * AnimationSpeed), Textures.Length - 1);
            _rawImage.texture = Textures[_currentFrameIndex];
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
            Debug.Log("Go to next scene!");
        } 
        else
        {
            if (_currentFrameIndex > 0)
            {
                Debug.Log("Stopped at frame " + _currentFrameIndex);
            }
            _currentFrameIndex = 0;
            _heldTime = 0;
            _rawImage.texture = Textures[0];
        }
    }
}
