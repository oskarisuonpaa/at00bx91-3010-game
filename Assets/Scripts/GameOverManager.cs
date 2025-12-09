using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    public Camera playerCamera;
    public Camera gameOverCamera;

    private void Awake()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);

            if (playerCamera != null) playerCamera.enabled = true;
            if (gameOverCamera != null) gameOverCamera.enabled = false;
        }
    }

    public void GameOver(bool playerWon)
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        if (gameOverText != null)
        {
            gameOverText.text = playerWon ? "GAME OVER - YOU WIN!" : "GAME OVER - YOU LOSE!";
        }

        if (playerWon)
        {
            if (playerCamera != null) playerCamera.enabled = true;
            if (gameOverCamera != null) gameOverCamera.enabled = false;
        } else
        {
            if (playerCamera != null) playerCamera.enabled = false;
            if (gameOverCamera != null) gameOverCamera.enabled = true;
        }

        Time.timeScale = 0f;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}
