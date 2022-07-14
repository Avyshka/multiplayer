using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class ProfileDataRegistrationScreen : ProfileDataScreenBase
    {
        [SerializeField] private TMP_InputField _emailField;
        [SerializeField] private Button _signUpButton;

        private string _email;

        protected override void SubscribeUiElements()
        {
            base.SubscribeUiElements();
            _emailField.onValueChanged.AddListener(OnEmailChanged);
            _signUpButton.onClick.AddListener(OnSignUpButtonClicked);
        }

        protected override void UnSubscribeUiElements()
        {
            base.UnSubscribeUiElements();
            _emailField.onValueChanged.RemoveAllListeners();
            _signUpButton.onClick.RemoveAllListeners();
        }

        private void OnEmailChanged(string value)
        {
            _email = value;
        }

        private void OnSignUpButtonClicked()
        {
            PlayFabClientAPI.RegisterPlayFabUser(GetUserRequest(), OnRegistrationSuccess, OnRegistrationFailure);
        }

        private RegisterPlayFabUserRequest GetUserRequest()
        {
            return new RegisterPlayFabUserRequest
            {
                Username = _userName,
                Email = _email,
                Password = _password
            };
        }

        private void OnRegistrationSuccess(RegisterPlayFabUserResult result)
        {
            Debug.Log($"{result.Username} was registered");
        }

        private void OnRegistrationFailure(PlayFabError error)
        {
            Debug.LogError(error.ErrorMessage);
        }
    }
}