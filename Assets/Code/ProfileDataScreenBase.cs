using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class ProfileDataScreenBase : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_InputField _userNameField;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private Button _backButton;
        [SerializeField] private Canvas _optionsCanvas;
        [SerializeField] private Canvas _currentCanvas;

        protected string _userName;
        protected string _password;

        private void Start()
        {
            SubscribeUiElements();
        }

        protected virtual void SubscribeUiElements()
        {
            _userNameField.onValueChanged.AddListener(OnUserNameChanged);
            _passwordField.onValueChanged.AddListener(OnPasswordChanged);
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        protected virtual void UnSubscribeUiElements()
        {
            _userNameField.onValueChanged.RemoveAllListeners();
            _passwordField.onValueChanged.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        private void OnUserNameChanged(string value)
        {
            _userName = value;
        }

        private void OnPasswordChanged(string value)
        {
            _password = value;
        }

        private void OnBackButtonClicked()
        {
            _currentCanvas.enabled = false;
            _optionsCanvas.enabled = true;
        }

        public void Dispose()
        {
            UnSubscribeUiElements();
        }
    }
}