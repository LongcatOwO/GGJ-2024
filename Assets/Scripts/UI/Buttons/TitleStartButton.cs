using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleStartButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public bool IsButtonHeld => _buttonHeld && _buttonSelected;

    private bool _buttonHeld;
    private bool _buttonSelected;

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.Select();

        PlayerInputHandler.Instance.OnAttackInputDown += () => _buttonHeld = true;
        PlayerInputHandler.Instance.OnAttackInputUp += () => _buttonHeld = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        _buttonSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _buttonSelected = false;
    }
}
