using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SignInPanel;
    public GameObject SignUpPanel;
    public SaveLoadManager SaveAndLoadManager;
    public static MainMenu instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }
    public void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    public void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        Debug.Log("Scenes: " + currentName + ", " + next.name);
    }
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //string pureJson = "{\\\"SaveUnits\\\":[{\\\"realPosition\\\":{\\\"x\\\":6.55,\\\"y\\\":12.95,\\\"z\\\":0.0},\\\"unitType\\\":4},{\\\"realPosition\\\":{\\\"x\\\":7.05,\\\"y\\\":13.86,\\\"z\\\":0.0},\\\"unitType\\\":0},{\\\"realPosition\\\":{\\\"x\\\":2.452,\\\"y\\\":11.29,\\\"z\\\":0.0},\\\"unitType\\\":4}],\\\"SaveBuildings\\\":[{\\\"realPosition\\\":{\\\"x\\\":2.452,\\\"y\\\":11.29,\\\"z\\\":0.0},\\\"unitType\\\":0,\\\"moveSpeed\\\":5.0,\\\"isEnemy\\\":false,\\\"FireInterval\\\":10.0,\\\"Manufacturing\\\":4}],\\\"SaveResourceDeps\\\":[{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":1.01,\\\"y\\\":6.88,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":1,\\\"Position\\\":{\\\"x\\\":41.48,\\\"y\\\":0.86,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":23.54,\\\"y\\\":6.05,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":8.0,\\\"y\\\":12.17,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":16.07,\\\"y\\\":15.64,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":37.09,\\\"y\\\":15.64,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":2.05,\\\"y\\\":6.91,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":1,\\\"Position\\\":{\\\"x\\\":47.07,\\\"y\\\":15.6,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":29.5,\\\"y\\\":0.92,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":14.02,\\\"y\\\":15.61,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":24.06,\\\"y\\\":5.19,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":1,\\\"Position\\\":{\\\"x\\\":42.04,\\\"y\\\":-0.03,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":42.03,\\\"y\\\":15.59,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":25.05,\\\"y\\\":3.46,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":-0.02,\\\"y\\\":0.03,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":5.06,\\\"y\\\":0.0,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":9.03,\\\"y\\\":12.19,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":13.04,\\\"y\\\":3.54,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":34.56,\\\"y\\\":11.23,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":12.0,\\\"y\\\":3.51,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":33.56,\\\"y\\\":11.23,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":1,\\\"Position\\\":{\\\"x\\\":46.55,\\\"y\\\":14.71,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":10.02,\\\"y\\\":6.94,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":25.09,\\\"y\\\":1.73,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":42.99,\\\"y\\\":3.46,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":41.48,\\\"y\\\":14.71,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":33.06,\\\"y\\\":1.73,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":43.54,\\\"y\\\":4.38,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":30.08,\\\"y\\\":13.93,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":30.55,\\\"y\\\":12.94,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":35.01,\\\"y\\\":15.64,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":8.58,\\\"y\\\":13.03,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":2.03,\\\"y\\\":0.03,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":9.53,\\\"y\\\":7.8,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":6.02,\\\"y\\\":0.0,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":-0.4595794,\\\"y\\\":0.843643248,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":30.01,\\\"y\\\":0.04,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":21.07,\\\"y\\\":0.01,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null}],\\\"keyValuePairs\\\":{\\\"Titanium\\\":0.0,\\\"Ruby\\\":0.0,\\\"Silver\\\":0.0,\\\"Gold\\\":0.0}}";
        //int endIndex = pureJson.Length - (4 + 0);
        //string data = pureJson;
        //char[] characters = data.ToCharArray();
        //List<char> list = new List<char>();
        //foreach (char item in characters)
        //{
        //    if (item == '\\')
        //    {

        //    }
        //    else
        //    {
        //        list.Add(item);

        //    }
        //}
        //char[] listAsArr = list.ToArray();
        //string NewData = new string(listAsArr);
        //print("List:" + pureJson);
        //SaveFileOBJ saveFileOBJ = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveFileOBJ>(NewData);
        SaveAndLoadManager.Load(SaveTypes.Default);
    }
    public void GotoScene()
    {
        SceneManager.LoadScene("Game");
        SaveAndLoadManager.Load(SaveTypes.SaveFile);
        //SceneManager.activeSceneChanged
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