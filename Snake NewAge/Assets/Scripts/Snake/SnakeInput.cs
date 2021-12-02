using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    [HideInInspector] public int isPressed;

    private PlayerController _playerController;

    private int _horizontal = 0, _vertical = 0;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }


    private void Update()
    {
        _horizontal = 0;
        _vertical = 0;

        TurnSnake();
    }

    void TurnSnake()
    {
        GetInput();
        SetMovement();
    }


    void GetInput()
    {
        //Prevent double movement
        if (_horizontal != 0)
            _vertical = 0;

        //Call functions to go left,right,up, down
        TurnUp();
        TurnDown();
        TurnRight();
        TurnLeft();
    }


    void SetMovement()
    {

        if (_vertical != 0)
        {
            _playerController.SetInputDirection((_vertical==1)?PlayerDirection.UP : PlayerDirection.DOWN);
        }
        if (_horizontal != 0)
            _playerController.SetInputDirection((_horizontal==1)?PlayerDirection.RIGHT:PlayerDirection.LEFT);
    }



    public void TurnLeft()
    {
        if (Input.GetKeyDown(KeyCode.A) || isPressed == 1)
        {
            _horizontal = -1;
        }
    }

    public void TurnRight()
    {
        if (Input.GetKeyDown(KeyCode.D) || isPressed == 2)
            _horizontal = 1;
    }

    public void TurnUp()
    {
        if (Input.GetKeyDown(KeyCode.W) || isPressed == 3)
            _vertical = 1;
    }

    public void TurnDown()
    {
        if (Input.GetKeyDown(KeyCode.S) || isPressed == 4)
            _vertical = -1;
    }

    //End Class
}
