using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public Button startGame, exitGame;


    private void Start()
    {
        startGame.onClick.AddListener(StartGame);
        exitGame.onClick.AddListener(ExitGame);
    }




    void ExitGame()
    {
        Application.Quit();
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
