using System;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class PlayfabLogin : MonoBehaviour
    {
        private const string AUTH_GUID_KEY = "autharithation_guid";

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _infoLabel;
        [SerializeField] private Button _loginButton;

        private void Start()
        {
            _title.text = "Playfab login";
            _loginButton.onClick.AddListener(Connect);
        }

        private void Connect()
        {
            _loginButton.interactable = false;
            _infoLabel.color = Color.white;
            _infoLabel.text = "Connecting...";

            var needCreation = PlayerPrefs.HasKey(AUTH_GUID_KEY);
            var id = PlayerPrefs.GetString(AUTH_GUID_KEY, Guid.NewGuid().ToString());

            var request = new LoginWithCustomIDRequest {CustomId = id, CreateAccount = !needCreation};
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        }

        private void OnLoginSuccess(LoginResult response)
        {
            _loginButton.onClick.RemoveAllListeners();
            
            var requestData = response.Request as LoginWithCustomIDRequest;
            PlayerPrefs.SetString(AUTH_GUID_KEY, requestData.CustomId);
            
            var msg = $"[Playfab]: user <{requestData.CustomId}> connected";
            _infoLabel.color = Color.green;
            _infoLabel.text = msg;
            Debug.Log(msg);
        }

        private void OnLoginFailure(PlayFabError error)
        {
            var msg = $"[Playfab]: something went wrong {error.GenerateErrorReport()}";
            _infoLabel.color = Color.red;
            _infoLabel.text = msg;
            Debug.LogError(msg);
            _loginButton.interactable = true;
        }
    }
}