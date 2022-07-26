using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class RoomInfoItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _playersCount;
        [SerializeField] private Button _joinRoomBtn;

        private string _roomName;

        public event Action<string> OnJoinToRoom;

        private void Start()
        {
            _joinRoomBtn.onClick.AddListener(OnJoinToRoomHandler);
        }

        private void OnDestroy()
        {
            _joinRoomBtn.onClick.RemoveAllListeners();
        }

        private void OnJoinToRoomHandler()
        {
            OnJoinToRoom?.Invoke(_roomName);
        }

        public void SetData(string roomName, int countPlayers, byte maxPlayers)
        {
            _roomName = roomName;
            _title.text = _roomName;
            _playersCount.text = $"Players: {countPlayers} / {maxPlayers}";
        }
    }
}