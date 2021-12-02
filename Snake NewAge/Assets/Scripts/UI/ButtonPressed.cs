using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private SnakeInput _snakeInput;
    public int setNumber;

    private void Start()
    {
        _snakeInput = FindObjectOfType<SnakeInput>().GetComponent<SnakeInput>();
    }




    public void OnPointerDown(PointerEventData eventData)
    {
        _snakeInput.isPressed = setNumber;
        Debug.Log("Dowm");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _snakeInput.isPressed = 0;
        Debug.Log("Up");
    }


}
