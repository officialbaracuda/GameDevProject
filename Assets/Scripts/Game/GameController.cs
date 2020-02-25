using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private SceneController sceneController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private GUIController guiController;
    [SerializeField] private GameObject pauseMenu, gameOverMenu, gameFinishedMenu;

    [SerializeField] private int points, coins, stars, lives;
    [SerializeField] private bool isGameOver, isLevelOver, isGamePaused, isGameFinished;

    private Animator pauseMenuAnimator, gameOverMenuAnimator, gameFinishedMenuAnimator;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        pauseMenuAnimator = pauseMenu.GetComponent<Animator>();
        gameOverMenuAnimator = gameOverMenu.GetComponent<Animator>();
        if (sceneController.IsLastLevel())
        {
            gameFinishedMenuAnimator = gameFinishedMenu.GetComponent<Animator>();
        }
        SetGameData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused && !isGameOver)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }


        if (isGameOver && gameOverMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pause"))
        {
            Time.timeScale = 0;
        }

        if (isGamePaused && pauseMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pause"))
        {
            Time.timeScale = 0;
        }

        if (isGameFinished && gameFinishedMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pause"))
        {
            Time.timeScale = 0;
        }


        if (!isGameOver && !isGamePaused && !isGameFinished)
        {
            Time.timeScale = 1;
        }

    }

    public void Collect(ItemType type, int value)
    {
        Debug.Log(type + " has collected with value " + value);
        switch (type)
        {
            case ItemType.Coin:
                {
                    guiController.UpdateCoinCountText(coins, value);
                    coins += value;
                    audioController.PlaySFX(audioController.coin);
                }
                break;
            case ItemType.Star:
                {
                    guiController.AddStar(stars);
                    stars += value;
                    audioController.PlaySFX(audioController.star);
                }
                break;
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        isGamePaused = true;

    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    private void Restart()
    {
        Time.timeScale = 1;
        SetGameData();
    }

    public void PlayerDie()
    {
        lives--;
        guiController.RemoveLive(lives);
        audioController.PlaySFX(audioController.die);
        if (lives <= 0)
        {
            GameOVer();
        }
    }

    private void GameOVer()
    {
        Debug.Log("GAME OVER");
        string nickname = StorageController.GetString(Constants.Nickname + Constants.Current);
        int currentCoin = StorageController.GetInteger(nickname + Constants.Coin);
        int currentStar = StorageController.GetInteger(nickname + Constants.Star);
        StorageController.SaveInteger(nickname + Constants.Coin, currentCoin + coins);
        StorageController.SaveInteger(nickname + Constants.Star, currentStar + stars);
        isGameOver = true;
        gameOverMenu.SetActive(true);
    }

    public void NextLevel()
    {
        string nickname = StorageController.GetString(Constants.Nickname + Constants.Current);
        int currentCoin = StorageController.GetInteger(nickname + Constants.Coin);
        int currentStar = StorageController.GetInteger(nickname + Constants.Star);
        StorageController.SaveInteger(nickname + Constants.Coin, currentCoin + coins);
        StorageController.SaveInteger(nickname + Constants.Star, currentStar + stars);
        sceneController.LoadNextLevel();
    }

    public void SetGameData()
    {
        lives = 5;
        isGameOver = false;
        isLevelOver = false;
        isGameFinished = false;
        isGamePaused = false;
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        guiController.UpdateCoinCountText(0, 0);
        guiController.RemoveStars();
    }

    public void FinishGame()
    {
        gameFinishedMenu.SetActive(true);
        isGameFinished = true;
    }

    public bool IsGameStopped() {
        if (isGameOver || isGamePaused) {
            return true;
        }
        return false;
    }

}
