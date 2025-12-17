using UnityEngine;
using TMPro;                  // For handling TextMeshPro UI text
using UnityEngine.SceneManagement; // For scene reload (restart)
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Singleton instance for easy access from other scripts
    public static GameManager Instance;

    // UI Elements
    public GameObject gameOverUI;              // Game over panel
    public TextMeshProUGUI finalScoreText;     // Final score display
    public TextMeshProUGUI GameOverText;       // "Game Over" text
    public TextMeshProUGUI scoreText;          // Current score display
    public TextMeshProUGUI timesurvivedText;   // Timer display
    public GameObject mainmenubutton;          // Main menu button
    public GameObject panel;                   // Pause menu panel
    public GameObject gameResumebutton;        // Resume button

    // Audio
    public AudioSource audioSource;            // Plays sound effects
    public AudioClip correctSound;             // Pickup sound
    public AudioClip loseClip;                 // Game over sound

    // Timer
    float elapsedTime = 0f;                    // Tracks time survived

    // Game state variables
    private int score = 0;                      // Player score
    private bool gameOver = false;              // Tracks if the game is over
    public bool IsPaused { get; private set; } // Tracks if the game is paused

    // Awake is called before Start; sets up singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;      // Assign singleton instance
        else
            Destroy(gameObject);  // Destroy duplicates
    }

    void Start()
    {
        UpdateUI();                // Initialize UI at start
    }

    void Update()
    {
        if (gameOver) return;      // Stop updating timer after game over

        // Increment timer
        elapsedTime += Time.deltaTime;

        // Update UI every frame
        UpdateUI();
    }

    // Called when player collects a correct item
    public void HandlePickup()
    {
        if (gameOver) return;

        score += 1;                            // Increase score
        audioSource.PlayOneShot(correctSound); // Play pickup sound
        UpdateUI();                            // Refresh score display
    }

    // Updates score and other UI elements
    void UpdateUI()
    {
        scoreText.text = "Score: " + score;    // Update current score
    }

    // Ends the game
    public void EndGame()
    {
        if (gameOver) return;
        gameOver = true;

        // Start coroutine to handle end game sequence
        StartCoroutine(EndGameSequence());
    }

    // Coroutine handles game over sequence
    private IEnumerator EndGameSequence()
    {
        MusicPlayer.Instance.StopMusic();      // Stop background music
        audioSource.PlayOneShot(loseClip);     // Play lose sound

        // Wait for fallback animation duration
        float fallAnimDuration = 3f;           // Match animator fallback clip length
        yield return new WaitForSeconds(fallAnimDuration);

        // Show game over UI
        gameOverUI.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
        timesurvivedText.text = "Time Survived: " + elapsedTime.ToString("F2");
        GameOverText.text = "GAME OVER";

        // Pause game
        Time.timeScale = 0f;
    }

    // Restart the current scene/game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene
        Time.timeScale = 1f;                                        // Reset time scale
        MusicPlayer.Instance.PlayMusic();                            // Restart background music
    }

    // Return to main menu
    public void BackMainMenu()
    {
        Time.timeScale = 1f;               // Ensure normal time scale
        SceneManager.LoadScene("Main Menu");
    }

    // Pause the game
    public void Pause()
    {
        if (gameOver) return;
        IsPaused = true;
        Time.timeScale = 0f;              // Freeze game
        panel.SetActive(true);             // Show pause panel
    }

    // Resume the game
    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;              // Resume game
        panel.SetActive(false);            // Hide pause panel
    }

    // Set background music volume
    public void SetMusicVolume(float volume)
    {
        if (MusicPlayer.Instance != null && MusicPlayer.Instance.audioSource != null)
        {
            MusicPlayer.Instance.audioSource.volume = Mathf.Clamp01(volume); // Clamp between 0 and 1
        }
    }
}
