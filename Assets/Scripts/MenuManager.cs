using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject optionMenuUI;

    public TextMeshProUGUI gameoverRunScore;
    public TextMeshProUGUI gameoverCoinsCount;

    public AudioMixer audioMixer;
    public Slider musicSlider;

    public RunScore runScore;
    public CoinsManager finalCoinsCount;

    //music setting checking
    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            LoadVolume();
        }

        else
        {
            SetVolume();
        }
    }

    //for load music slider setting
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Volume");
        SetVolume();
    }

    //for set music slider
    public void SetVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

   public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
   public void SinglePlayerGame()
    {
        SceneManager.LoadScene("SinglePlayer Game");
        Time.timeScale = 1;
    }

    public void PauseMenuUI()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OptionMenu()
    {
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
    }

    public void Back()
    {
        optionMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        gameoverRunScore.text = "You ran for " + runScore.runningScore.ToString() + " M!";
        gameoverCoinsCount.text = "You have collected " + finalCoinsCount.coinsCount.ToString() + " gold coins!";
        Time.timeScale = 0;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
