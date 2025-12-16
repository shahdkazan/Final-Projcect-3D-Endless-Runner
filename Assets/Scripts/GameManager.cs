using UnityEngine;
using TMPro;                  // For handling TextMeshPro UI text
using UnityEngine.SceneManagement; // For scene reload (restart)
using System.Collections;


public class GameManager : MonoBehaviour
{
    // Singleton instance so other scripts can easily access GameManager
    public static GameManager Instance;

    // UI Elements
    public GameObject gameOverUI;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timesurvivedText;


    // Audio
    public AudioSource audioSource;
    public AudioClip correctSound;
    //public AudioClip wrongSound;
    //public AudioClip winClip;
    public AudioClip loseClip;
    float elapsedTime = 0f;


    public GameObject mainmenubutton;
    public GameObject panel;
    public GameObject gameResumebutton;
    // Game state variables
    private int score = 0;           // Player score
    private bool gameOver = false;   // Tracks if the game is over
    public bool IsPaused { get; private set; }



    // Awake is called before Start used to setup singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this; // Set singleton instance
        else
            Destroy(gameObject); // Destroy duplicates
    }

    void Start()
    {
        UpdateUI();           // Initialize UI

    }

    void Update()
    {
        if (gameOver) return; // stop counting only after player loses

        // Count up from zero
        elapsedTime += Time.deltaTime;

        // Update UI every frame
        UpdateUI();
    }
    public void HandlePickup()
    {
        if (gameOver) return;


        score += 1; // Increase score for correct pickup
        audioSource.PlayOneShot(correctSound);

        UpdateUI(); // Update score display
    }

    // Updates score and timer UI
    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        
    }

    // Handles end-of-game logic
    //public void EndGame()
    //{
    //    gameOver = true;

    //    //scoreText.gameObject.SetActive(false);


    //    // Show game over UI
    //    gameOverUI.SetActive(true);
    //    finalScoreText.text = "Final Score: " + score;
    //    timesurvivedText.text = "Time Survived: " + elapsedTime.ToString("F2"); // seconds with 2 decimals

    //    // Stop background music
    //    //MusicPlayer.Instance.StopMusic();
    //    Time.timeScale = 0f;

    //    audioSource.PlayOneShot(loseClip);
    //    GameOverText.text = "GAME OVER";

    //}

    public void EndGame()
    {
        if (gameOver) return;
        gameOver = true;

        // Trigger the fallback animation already triggered in PlayerController
        // Optionally you could trigger it here too if needed

        // Show game over UI after delay
        StartCoroutine(EndGameSequence());
    }

    private IEnumerator EndGameSequence()
    {
        // Play lose sound
        MusicPlayer.Instance.StopMusic();
        audioSource.PlayOneShot(loseClip);

        // Wait for fallback animation duration (e.g., 1 second)
        float fallAnimDuration = 3f; // match your animator's fallback clip length
        yield return new WaitForSeconds(fallAnimDuration);

        // Show UI
        gameOverUI.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
        timesurvivedText.text = "Time Survived: " + elapsedTime.ToString("F2");
        GameOverText.text = "GAME OVER";

        // Pause game
        Time.timeScale = 0f;
    }
    //Restart the current scene(game)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        MusicPlayer.Instance.PlayMusic(); // Restart background music
    }

    public void BackMainMenu()
    {
        Time.timeScale = 1f; // Ensure normal time scale
        SceneManager.LoadScene("Main Menu");
     
    }

 
    public void Pause()
    {
        if (gameOver) return;
        IsPaused = true;
        Time.timeScale = 0f;        // freezes Update, animations, physics
        panel.SetActive(true);

    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
    public void SetMusicVolume(float volume)
    {
        if (MusicPlayer.Instance != null && MusicPlayer.Instance.audioSource != null)
        {
            MusicPlayer.Instance.audioSource.volume = Mathf.Clamp01(volume);
        }
    }

}
