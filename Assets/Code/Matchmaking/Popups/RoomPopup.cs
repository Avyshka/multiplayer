using System;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class RoomPopup : MonoBehaviour
    {
        [SerializeField] private Canvas _current;
        [SerializeField] private GameObject _playerDataContainer;
        [SerializeField] private GameObject _playerDataPrefab;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _leaveRoomBtn;
        [SerializeField] private Button _closeRoomBtn;
        [SerializeField] private Button _startMatchBtn;

        private readonly List<GameObject> _playersData = new List<GameObject>();

        public event Action OnLeaveRoomRequest;
        public event Action OnCloseRoomRequest;
        public event Action OnStartMatchRequest;

        private void Start()
        {
            _leaveRoomBtn.onClick.AddListener(OnLeaveRoom);
            _closeRoomBtn.onClick.AddListener(OnCloseRoom);
            _startMatchBtn.onClick.AddListener(OnStartMatch);
        }

        private void OnDestroy()
        {
            _leaveRoomBtn.onClick.RemoveAllListeners();
            _closeRoomBtn.onClick.RemoveAllListeners();
            _startMatchBtn.onClick.RemoveAllListeners();
        }

        public void Show(string roomName)
        {
            _title.text = $"ROOM: {roomName}";
            _current.enabled = true;
        }

        public void UpdatePlayersData(Dictionary<int, Player> players)
        {
            foreach (var playerData in _playersData)
            {
                Destroy(playerData);
            }

            _playersData.Clear();
            foreach (var player in players)
            {
                var playerData = Instantiate(_playerDataPrefab, _playerDataContainer.transform);
                if (playerData.TryGetComponent(out PlayerDataItem playerDataItem))
                {
                    var playerNick = player.Value.NickName == "" ? $"Player {player.Key}" : player.Value.NickName;
                    playerDataItem.setData(playerNick);
                    _playersData.Add(playerData);
                }
            }
        }

        private void OnLeaveRoom()
        {
            _current.enabled = false;
            OnLeaveRoomRequest?.Invoke();
        }

        private void OnCloseRoom()
        {
            OnCloseRoomRequest?.Invoke();
        }

        private void OnStartMatch()
        {
            OnStartMatchRequest?.Invoke();
        }
    }
}