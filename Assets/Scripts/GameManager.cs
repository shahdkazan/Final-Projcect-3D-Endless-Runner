using UnityEngine;
using TMPro;                  // For handling TextMeshPro UI text
using UnityEngine.SceneManagement; // For scene reload (restart)

public class GameManager : MonoBehaviour
{
    // Singleton instance so other scripts can easily access GameManager
    public static GameManager Instance;

    // UI Elements
    //public GameObject gameOverUI;
    //public TextMeshProUGUI finalScoreText;
    //public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timesurvivedText;


    // Audio
    public AudioSource audioSource;
    public AudioClip correctSound;
    //public AudioClip wrongSound;
    //public AudioClip winClip;
    //public AudioClip loseClip;
    float elapsedTime = 0f;


    public GameObject mainmenubutton;
    public GameObject panel;
    public GameObject gameResumebutton;
    // Game state variables
    private int score = 0;           // Player score
    //private bool gameOver = false;   // Tracks if the game is over


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
        //if (gameOver) return; // stop counting only after player loses

        // Count up from zero
        elapsedTime += Time.deltaTime;

        // Update UI every frame
        UpdateUI();
    }
    public void HandlePickup()
    {
        //if (gameOver) return;

      
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
    void EndGame()
    {
        //gameOver = true;

        scoreText.gameObject.SetActive(false);

        // Show game over UI
        //gameOverUI.SetActive(true);
        //finalScoreText.text = "Final Score: " + score;
        //timesurvivedText.text = elapsedTime.ToString("F2"); // seconds with 2 decimals

        // Stop background music
        //MusicPlayer.Instance.StopMusic();


        //audioSource.PlayOneShot(winClip);
        //GameOverText.text = "GAME OVER YOU WIN";

    }
    // Restart the current scene (game)
    //public void RestartGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //    MusicPlayer.Instance.PlayMusic(); // Restart background music
    //}

    public void BackMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Pause()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void Resume()
    { panel.SetActive(false);
    }
}
