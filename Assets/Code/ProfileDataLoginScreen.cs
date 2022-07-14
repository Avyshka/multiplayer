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

        private void OnSignUpButtonClicked()
        {
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
            Debug.Log($"{_userName} was logged in");
        }

        private void OnLoginFailure(PlayFabError error)
        {
            Debug.LogError(error.ErrorMessage);
        }
    }
}