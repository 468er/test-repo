using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SignInPanel;
    public GameObject SignUpPanel;
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }
    public void GotoScene()
    {
        SceneManager.LoadScene("New");

    }
    public void Back()
    {
        SignUpPanel.SetActive(false);
        SignInPanel.SetActive(false);
        MainMenuPanel.SetActive(true); 
    }
    public void OpenSignInMenu()
    {

        SignUpPanel.SetActive(false);
        SignInPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
    public void OpenSignUpMenu()
    {

        SignUpPanel.SetActive(true);
        SignInPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
    }  
    public void Continue()
    {

        SignUpPanel.SetActive(false);
        SignInPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
    }
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit.");

    }
}