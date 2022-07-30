using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class CreateRoomPopup : MonoBehaviour
    {
        [SerializeField] private Canvas _lobby;
        [SerializeField] private Canvas _current;
        [SerializeField] private Button _back;
        [SerializeField] private Button _createBtn;
        [SerializeField] private TMP_InputField _roomNameField;
        [SerializeField] private Slider _countPlayersSlider;
        [SerializeField] private TMP_Text _countPlayersLabel;
        [SerializeField] private Toggle _privateRoomToggle;

        public string RoomName { get; private set; }
        public byte CountPlayers { get; private set; }
        public bool IsPrivateRoom { get; private set; }

        public event Action OnCreateRoomRequest;

        public void Show()
        {
            _lobby.enabled = false;
            _current.enabled = true;
        }
        
        private void Start()
        {
            OnCountPlayersChanged(_countPlayersSlider.value);

            _back.onClick.AddListener(OnBackToLobby);
            _createBtn.onClick.AddListener(OnCreateRoom);
            _roomNameField.onValueChanged.AddListener(OnRoomNameChanged);
            _countPlayersSlider.onValueChanged.AddListener(OnCountPlayersChanged);
            _privateRoomToggle.onValueChanged.AddListener(OnPrivacyChanged);
        }

        private void OnDestroy()
        {
            _back.onClick.RemoveAllListeners();
            _createBtn.onClick.RemoveAllListeners();
            _roomNameField.onValueChanged.RemoveAllListeners();
            _countPlayersSlider.onValueChanged.RemoveAllListeners();
            _privateRoomToggle.onValueChanged.RemoveAllListeners();
        }

        private void OnBackToLobby()
        {
            _current.enabled = false;
            _lobby.enabled = true;
        }

        private void OnCreateRoom()
        {
            _current.enabled = false;
            OnCreateRoomRequest?.Invoke();
        }

        private void OnRoomNameChanged(string value)
        {
            RoomName = value;
        }

        private void OnCountPlayersChanged(float value)
        {
            CountPlayers = (byte) value;
            _countPlayersLabel.text = CountPlayers.ToString();
        }

        private void OnPrivacyChanged(bool value)
        {
            IsPrivateRoom = value;
        }
    }
}