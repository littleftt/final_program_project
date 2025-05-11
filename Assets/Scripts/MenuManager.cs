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

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Volume");
        SetVolume();
    }

    public void SetVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

   public void Resume()
    {
        pauseMenuUI.SetActive(false);
    }
   public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
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
    public void QuitGame()
    {
        Application.Quit();
    }
}
