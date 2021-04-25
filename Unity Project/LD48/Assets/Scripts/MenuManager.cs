using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu; // these are toggled off and on for each menu

    public static float volume = 0.5f;

    public Slider vol;

    private void Start()
    {
        ToggleMenu(0);
        volume = 0.5f;
        vol.value = 0.5f;
    }

    void ToggleMenu(int activeMenu)
    {
        if (activeMenu == 0)
        {
            optionsMenu.SetActive(false);
            creditsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (activeMenu == 1)
        {
            optionsMenu.SetActive(false);
            creditsMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
        else
        {
            optionsMenu.SetActive(true);
            creditsMenu.SetActive(false);
            mainMenu.SetActive(false);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void CreditsButton()
    {
        ToggleMenu(1);
    }

    public void OptionsButton()
    {
        ToggleMenu(2);
    }

    public void VolumeUpdate()
    {
        volume = vol.value;
    }

    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackButton()
    {
        ToggleMenu(0);
    }
}
