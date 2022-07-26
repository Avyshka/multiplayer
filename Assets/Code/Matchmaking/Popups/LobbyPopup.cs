using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class LobbyPopup : MonoBehaviour
    {
        [SerializeField] private Canvas _current;
        [SerializeField] private GameObject _roomsInfoContainer;
        [SerializeField] private GameObject _roomInfoPrefab;
        [SerializeField] private Button _joinRoomBtn;
        [SerializeField] private Button _createRoomBtn;

        private readonly List<GameObject> _roomsInfo = new List<GameObject>();

        public event Action OnJoinRoomRequest;
        public event Action OnCreateRoomRequest;
        public event Action<string> OnJoinToSelectedRoomRequest;

        private void Start()
        {
            _joinRoomBtn.onClick.AddListener(OnJoinRoom);
            _createRoomBtn.onClick.AddListener(OnCreateRoom);
        }

        private void OnDestroy()
        {
            _joinRoomBtn.onClick.RemoveAllListeners();
            _createRoomBtn.onClick.RemoveAllListeners();
        }

        public void Show()
        {
            _current.enabled = true;
        }

        public void Hide()
        {
            _current.enabled = false;
        }

        public void UpdateRoomsInfo(List<RoomInfo> roomList)
        {
            RemoveAllRoomsInfo();
            AddNewRoomsInfo(roomList);
        }

        private void RemoveAllRoomsInfo()
        {
            foreach (var roomInfo in _roomsInfo)
            {
                if (roomInfo.TryGetComponent(out RoomInfoItem roomInfoItem))
                {
                    roomInfoItem.OnJoinToRoom -= OnJoinToRoomHandler;
                }
                Destroy(roomInfo);
            }

            _roomsInfo.Clear();
        }

        private void AddNewRoomsInfo(List<RoomInfo> roomList)
        {
            foreach (var room in roomList)
            {
                var roomInfo = Instantiate(_roomInfoPrefab, _roomsInfoContainer.transform);
                if (roomInfo.TryGetComponent(out RoomInfoItem roomInfoItem))
                {
                    roomInfoItem.SetData(room.Name, room.PlayerCount, room.MaxPlayers);
                    roomInfoItem.OnJoinToRoom += OnJoinToRoomHandler;
                    _roomsInfo.Add(roomInfo);
                }
            }
        }

        private void OnJoinToRoomHandler(string roomName)
        {
            OnJoinToSelectedRoomRequest?.Invoke(roomName);
        }

        private void OnJoinRoom()
        {
            OnJoinRoomRequest?.Invoke();
        }

        private void OnCreateRoom()
        {
            OnCreateRoomRequest?.Invoke();
        }
    }
}