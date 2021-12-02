using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    public Slider slider;

    public GameObject foodPickUp;

    private float _minX = -4.25f, _maxX = 4.25f, _minZ = -4.25f, _maxZ = 4.25f;
    private float _yPos = 0f;

    private int _scoreCount;
    private Text _scoreText;


    void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        _scoreText = GameObject.Find("Score").GetComponent<Text>();
        Invoke("StartSpawning", 0.5f);

        slider.value = PlayerPrefs.GetFloat("SoundVolume");
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnPickUps());
    }

    public void CancelSpawning()
    {
        CancelInvoke("SpawnPickUps");
    }

    IEnumerator SpawnPickUps()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));

        if(Random.Range(0,10) >= 2)
        {
            Instantiate(foodPickUp,
                new Vector3(Random.Range(_minX, _maxX), _yPos, Random.Range(_minZ, _maxZ)),
                Quaternion.identity);
        }

        Invoke("StartSpawning", 0f);
    }


    public void IncreaseScore()
    {
        _scoreCount++;
        _scoreText.text = "X" + _scoreCount;
    }
}
