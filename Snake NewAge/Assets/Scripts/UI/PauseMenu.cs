using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button resume;
    public Button exit,exitGame;
    public Button pause;
    public Button reload;

    public Text curScore,endScore;

    public GameObject pausePanel;

    private void Start()
    {
        resume.onClick.AddListener(ResumeGame);
        pause.onClick.AddListener(PauseGame);
        exit.onClick.AddListener(ExitGame);
        exitGame.onClick.AddListener(ExitGame);
        reload.onClick.AddListener(ReloadLevel);
    }

    void ExitGame()
    {
        Application.Quit();
        print("Exit Game");
    }

    void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        endScore.text = curScore.text;
    }
}
