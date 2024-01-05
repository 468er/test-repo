using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using UnityEngine.UI;

public class AuthenticationManager : MonoBehaviour
{
    public string playerId;
    // Start is called before the first frame update
    async void Awake()
    {
        //initializes the authenticator
        await UnityServices.InitializeAsync();
        SignIn();
    }

    // Update is called once per frame
    public async void SignIn()
    {
        await signInAnon();
    }
    async Task signInAnon()
    {
        //signs in without a credential, which basically just means computer-specific
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            print("Player id: " + AuthenticationService.Instance.PlayerId);
            playerId = AuthenticationService.Instance.PlayerId;
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
    }
}
