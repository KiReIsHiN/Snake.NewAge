using UnityEngine;

public class GameOver : MonoBehaviour
{
    [HideInInspector] public static bool isGameOver = false;
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private AudioSource _audioManager;
    [SerializeField] private AudioClip _audioClipGameOver;
    AudioSource _audioSource;
    

    private void Start()
    {
        isGameOver = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        GameEnd();
    }

    void GameEnd()
    {
        if (isGameOver == true)
        {
            _gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            _audioManager.PlayOneShot(_audioClipGameOver);
        }
        else
        {
            return;
        }
    }
}
