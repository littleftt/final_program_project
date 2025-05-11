using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject optionMenuUI;

    public AudioMixer audioMixer;
    public Slider musicSlider;

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
   public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
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
        Time.timeScale = 0;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
