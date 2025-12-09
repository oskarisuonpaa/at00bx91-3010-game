using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // added Customize player button for MainScene
    public void CustomizePlayer()
    {
        SceneManager.LoadScene("SkinSelection");
    }
    // added Back Button to SkinSelection scene
    public void BackToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
