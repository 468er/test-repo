using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

public class AuthenticationManager : MonoBehaviour
{
    public string playerId;

    public TMP_InputField SignUpUsername;
    public TMP_InputField SignUpPassword;
    public TMP_InputField SignInUsername;
    public TMP_InputField SignInPassword;

    public MainMenu MenuManager;
    public static AuthenticationManager instance;


    // Start is called before the first frame update
    async void Awake()
    {
        await UnityServices.InitializeAsync();
        //initializes the authenticator
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        if (AuthenticationService.Instance.IsSignedIn== true)
        {
            MenuManager.SignInPanel.SetActive(false);
            MenuManager.SignUpPanel.SetActive(false);
        }
        else
        {
            await UnityServices.InitializeAsync();
        }
    }

   public async Task SignUpWithUsernamePassword(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }
    public async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        MenuManager.Continue();
    }
    // Update is called once per frame
    public async void SignIn()
    {
        await signInAnon();
    }   
    public async void SignUP()
    {
        string username = SignUpUsername.text;
        string password = SignUpPassword.text;
        await SignUpWithUsernamePassword( username,  password);
        await SignInWithUsernamePasswordAsync(username, password);
    }    
    public async void SignInUserPass()
    {
        string username= SignInUsername.text;
        string password= SignInPassword.text;
        await SignInWithUsernamePasswordAsync(username, password);
    }
    async Task signInAnon()
    {
        //signs in without a credential, which basically just means computer-specific
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Player id: " + AuthenticationService.Instance.PlayerId);
            playerId = AuthenticationService.Instance.PlayerId;
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
    }
}
