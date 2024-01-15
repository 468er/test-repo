using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class TestingDebugLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private Button Button;
    string recoverableStatus = "";
    [SerializeField] string ErrorFilename = "";
    [SerializeField] string LogFileName = "";
    // Start is called before the first frame update
    private void Awake()
    {
        Button.onClick.AddListener(() =>
        {
            Hide();
        });
    }
    void Start()
    {
  
        //Subscribes method to log message event, which fires every time a new Debug log is recorded
        Application.logMessageReceived += Application_logMessageReceived;

        Hide();
        ErrorFilename = Application.persistentDataPath + "/ErrorFile.text";
        LogFileName = Application.persistentDataPath + "/LogFile.text";
    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Exception:
                {
                    recoverableStatus = ". There's likely nothing you as the player can do about this! ";
                    break;

                }
            default:
                {
                    recoverableStatus = ". Likely not gameplay-breaking! ";
                    break;
                }
        }
        //if an error or exception, we alert the player and send to a seperate text file than normal logs.
        if (type == LogType.Error || type == LogType.Exception)
        {
            TextWriter tw = new StreamWriter(ErrorFilename, true);
            tw.WriteLine("[" + System.DateTime.Now + "]" + condition + ". StackTrace: " + stackTrace + recoverableStatus + ". Type: " + type.ToString() + "\n");
            tw.Close();
            Show();

            errorText.text = "Error: " + condition + ". StackTrace: " + stackTrace + recoverableStatus ;
        }
        else
        {
            TextWriter tw = new StreamWriter(LogFileName, true);
            tw.WriteLine("[" + System.DateTime.Now + "]" + condition + ". StackTrace: " + stackTrace + recoverableStatus + ". Type: " + type.ToString() + "\n");
            tw.Close();
        }
    }
    private void OnDestroy()
    {
        Application.logMessageReceived -= Application_logMessageReceived;

    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
