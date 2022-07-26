using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class JoinRoomPopup : MonoBehaviour
    {
        [SerializeField] private Canvas _lobby;
        [SerializeField] private Canvas _current;
        [SerializeField] private Button _back;
        [SerializeField] private Button _joinBtn;
        [SerializeField] private TMP_InputField _roomNameField;

        public string RoomName { get; private set; }

        public event Action OnJoinRoomRequest;

        public void Show()
        {
            _lobby.enabled = false;
            _current.enabled = true;
        }

        private void Start()
        {
            _back.onClick.AddListener(OnBackToLobby);
            _joinBtn.onClick.AddListener(OnCreateRoom);
            _roomNameField.onValueChanged.AddListener(OnRoomNameChanged);
        }

        private void OnDestroy()
        {
            _back.onClick.RemoveAllListeners();
            _joinBtn.onClick.RemoveAllListeners();
            _roomNameField.onValueChanged.RemoveAllListeners();
        }

        private void OnBackToLobby()
        {
            _current.enabled = false;
            _lobby.enabled = true;
        }

        private void OnCreateRoom()
        {
            _current.enabled = false;
            OnJoinRoomRequest?.Invoke();
        }

        private void OnRoomNameChanged(string value)
        {
            RoomName = value;
        }
    }
}