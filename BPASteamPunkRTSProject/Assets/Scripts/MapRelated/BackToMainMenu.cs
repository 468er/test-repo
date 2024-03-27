using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackToMainMenu : MonoBehaviour
{
    public GameObject PromptMenu;
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void CloseMenu()
    {
        PromptMenu.SetActive(false);
        Time.timeScale = 0;
    }
    public void OpenMenu()
    {
        PromptMenu.SetActive(true);
        Time.timeScale = 1;

    }
}
