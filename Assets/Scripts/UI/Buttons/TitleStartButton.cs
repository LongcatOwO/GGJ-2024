using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleStartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsButtonHeld => _buttonHeld;

    private bool _buttonHeld;

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.Select();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _buttonHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _buttonHeld = false;
    }
}
