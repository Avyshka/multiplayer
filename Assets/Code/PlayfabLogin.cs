using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class PlayfabLogin : MonoBehaviour
    {
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
            var request = new LoginWithCustomIDRequest {CustomId = "PlayerTest", CreateAccount = true};
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        }

        private void OnLoginSuccess(LoginResult response)
        {
            _loginButton.onClick.RemoveAllListeners();
            var requestData = response.Request as LoginWithCustomIDRequest;
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