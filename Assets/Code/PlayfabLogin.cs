using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Aivagames.multiplayer
{
    public class PlayfabLogin : MonoBehaviour
    {
        private void Start()
        {
            var request = new LoginWithCustomIDRequest {CustomId = "PlayerTest", CreateAccount = true};
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        }

        private void OnLoginSuccess(LoginResult response)
        {
            var requestData = response.Request as LoginWithCustomIDRequest;
            Debug.Log($"[Playfab]: {requestData.CustomId} connected");
        }

        private void OnLoginFailure(PlayFabError error)
        {
            var errorMessage = error.GenerateErrorReport();
            Debug.LogError($"[Playfab]: something went wrong {errorMessage}");
        }
    }
}