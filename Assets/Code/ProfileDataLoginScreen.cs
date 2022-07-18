using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class ProfileDataLoginScreen : ProfileDataScreenBase
    {
        [SerializeField] private Button _signInButton;

        protected override void SubscribeUiElements()
        {
            base.SubscribeUiElements();
            _signInButton.onClick.AddListener(OnSignUpButtonClicked);
        }

        protected override void UnSubscribeUiElements()
        {
            base.UnSubscribeUiElements();
            _signInButton.onClick.RemoveAllListeners();
        }

        protected override void ChangeEnabledUI(bool value)
        {
            base.ChangeEnabledUI(value);
            _signInButton.enabled = value;
        }

        private void OnSignUpButtonClicked()
        {
            ChangeEnabledUI(false);
            _loadingCanvas.Show();
            PlayFabClientAPI.LoginWithPlayFab(GetUserRequest(), OnLoginSuccess, OnLoginFailure);
        }

        private LoginWithPlayFabRequest GetUserRequest()
        {
            return new LoginWithPlayFabRequest
            {
                Username = _userName,
                Password = _password
            };
        }

        private void OnLoginSuccess(LoginResult result)
        {
            ChangeEnabledUI(true);
            _loadingCanvas.Hide();
            EnterInLobbyScene();
            Debug.Log($"{_userName} was logged in");
        }

        private void OnLoginFailure(PlayFabError error)
        {
            ChangeEnabledUI(true);
            _loadingCanvas.Hide();
            Debug.LogError(error.ErrorMessage);
        }
    }
}