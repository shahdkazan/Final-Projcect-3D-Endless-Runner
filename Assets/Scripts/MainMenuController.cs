using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName = "Game"; // Replace with your game scene name
    public GameObject panel;
    public GameObject infobutton;
    public GameObject backbutton;
    private void Start()
    {
       
        MusicPlayer.Instance.PlayMusic();

    }
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
       
    }

    public void submenu()
    {
        panel.SetActive(!panel.activeSelf);
        infobutton.SetActive(false);
        backbutton.SetActive(true);
    }
    public void mainmenu()
    {
        panel.SetActive(false);
        infobutton.SetActive(true);
        backbutton.SetActive(false);
    }
}
