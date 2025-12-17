using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName = "Game"; 
    public GameObject panel;
    public GameObject infobutton;
    public GameObject backbutton;
    public GameObject gametitle;
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
        gametitle.SetActive(false);
        backbutton.SetActive(true);
    }
    public void mainmenu()
    {
        panel.SetActive(false);
        infobutton.SetActive(true);
        backbutton.SetActive(false);
        gametitle.SetActive(true);
    }
}
