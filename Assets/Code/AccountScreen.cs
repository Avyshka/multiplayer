using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class AccountScreen : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _signUpButton;
        [SerializeField] private Canvas _optionsCanvas;
        [SerializeField] private Canvas _signInCanvas;
        [SerializeField] private Canvas _signUpCanvas;

        private void Start()
        {
            _signInButton.onClick.AddListener(OnShowSignInCanvas);
            _signUpButton.onClick.AddListener(OnShowSignUpCanvas);
        }

        private void OnShowSignInCanvas()
        {
            _optionsCanvas.enabled = false;
            _signInCanvas.enabled = true;
        }

        private void OnShowSignUpCanvas()
        {
            _optionsCanvas.enabled = false;
            _signUpCanvas.enabled = true;
        }

        public void Dispose()
        {
            _signInButton.onClick.RemoveAllListeners();
            _signUpButton.onClick.RemoveAllListeners();
        }
    }
}