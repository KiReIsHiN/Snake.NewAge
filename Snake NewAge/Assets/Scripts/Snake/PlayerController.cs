using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public PlayerDirection direction; // direction snake is going to go
    [HideInInspector] public float stepLength = 0.2f; // How much unit we move every node
    [HideInInspector] public float movementFriquency = 0.1f; // How many time we move per second
    private float _counter; // uses with movementFrequency to allow player to move
    private bool _move;

    float currentVolume; //Volume of AudioListener

    [SerializeField] private GameObject _tailPrefab; // our tails

    private List<Vector3> _deltaPosition; // store the previous move of the player 
    private List<Rigidbody> _nodes;// store our nodes

    private Rigidbody _mainBody;
    private Rigidbody _headBody;

    private Transform _tr; // our transform

    private bool _createNodeAtTail;


    void Awake()
    {
        _tr = transform;
        _mainBody = GetComponent<Rigidbody>(); // The top enmpty component of snake (heat,node,tails are inside)

        InitSnakeNodes();
        InitPlayer();

        _deltaPosition = new List<Vector3>()
        {
            new Vector3(-stepLength,0f,0f), //-direction x .... LEFT
            new Vector3(0f,0f,stepLength),  // direction z .... UP
            new Vector3(stepLength,0f,0f),  // direction x .... RIGHT
            new Vector3(0f,0f,-stepLength)  //-direction z .... DOWN
        };
    }
         
    void Update()
    {
        CheckMovementFrequency();
    }

    private void FixedUpdate()
    {
        if (_move)
        {
            _move = false;
            Move();
        }

        SetAudioLevel();
    }


    void SetAudioLevel()
    {
        currentVolume = PlayerPrefs.GetFloat("SoundVolume");
        AudioListener.volume = currentVolume;
    }



    void InitSnakeNodes()
    {
        _nodes = new List<Rigidbody>();

        _nodes.Add(_tr.GetChild(0).GetComponent<Rigidbody>());//Add head
        _nodes.Add(_tr.GetChild(1).GetComponent<Rigidbody>());//Add node
        _nodes.Add(_tr.GetChild(2).GetComponent<Rigidbody>());//Add tail

        _headBody = _nodes[0];
    }

    //Set random direction in the begining
    void SetDirectionRandom()
    {
        int dirRandom = Random.Range(0,(int)PlayerDirection.COUNT);
        direction = (PlayerDirection)dirRandom;
    }

    //Put nodes in correct order
    void InitPlayer()
    {
        SetDirectionRandom();

        switch (direction)
        {
            case PlayerDirection.LEFT:
                _nodes[1].position = _nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                _nodes[2].position = _nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.UP:
                _nodes[1].position = _nodes[0].position - new Vector3(0f, 0f, Metrics.NODE);
                _nodes[2].position = _nodes[0].position - new Vector3(0f, 0f, Metrics.NODE * 2f);
                break;
            case PlayerDirection.RIGHT:
                _nodes[1].position = _nodes[0].position - new Vector3(Metrics.NODE,0f,0f);
                _nodes[2].position = _nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.DOWN:
                _nodes[1].position = _nodes[0].position + new Vector3(0f, 0f, Metrics.NODE);
                _nodes[2].position = _nodes[0].position + new Vector3(0f, 0f, Metrics.NODE * 2f);
                break;
        }
    }

    //Here we move snake
    void Move()
    {
        Vector3 dPosition = _deltaPosition[(int)direction];

        Vector3 parentPosition = _headBody.position;
        Vector3 previousPosition;

        _mainBody.position = _mainBody.position + dPosition;
        _headBody.position = _headBody.position + dPosition;

        for (int i = 1; i < _nodes.Count; i++)
        {
            previousPosition = _nodes[i].position;
            _nodes[i].position = parentPosition;
            parentPosition = previousPosition;
        }

        //check if we need to create a new node
        //because we ate a food
        if (_createNodeAtTail)
        {
            _createNodeAtTail = false;

            GameObject newTail = Instantiate(_tailPrefab, _nodes[_nodes.Count-1].position, Quaternion.identity);
            newTail.transform.SetParent(transform, true);
            _nodes.Add(newTail.GetComponent<Rigidbody>());
        }
    }

    void CheckMovementFrequency()
    {
        _counter += Time.deltaTime;

        if(_counter >= movementFriquency)
        {
            _counter = 0;
            _move = true;
        }
    }

    //Prevent snake from moving back direction to avoid hitting itself (it cannot change direction viceversa)
    public void SetInputDirection(PlayerDirection dir)
    {
        if(dir == PlayerDirection.UP   && direction==PlayerDirection.DOWN || 
           dir == PlayerDirection.DOWN && direction==PlayerDirection.UP   ||
           dir==PlayerDirection.RIGHT  && direction==PlayerDirection.LEFT ||
           dir==PlayerDirection.LEFT   && direction==PlayerDirection.RIGHT)
        {
            return;
        }
        direction = dir;
        ForceMove();
    }

    void ForceMove()
    {
        _counter = 0;
        _move = false;
        Move();
    }


    //Check collision to end game or to grow snake
    void OnTriggerEnter(Collider target)
    {
        if(target.tag == Tags.WALL || target.tag == Tags.TAIL)
        {
            GameOver.isGameOver = true;
        }

        if(target.tag == Tags.FOOD)
        {
            target.gameObject.SetActive(false);
            _createNodeAtTail = true;
            GamePlayController.instance.IncreaseScore();
        }
    }
}
