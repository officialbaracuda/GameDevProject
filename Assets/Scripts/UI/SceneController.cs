using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private string MAIN_MENU = "MainMenu";

    public void LoadScene(int level)
    {
        // MainMenu: Scene 0
        SceneManager.LoadScene(level); 
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
    }

    public void RestartScene()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public bool IsLastLevel() {
        if (SceneManager.GetActiveScene().buildIndex == 4) {
            return true;
        }
        return false;
    }
}
