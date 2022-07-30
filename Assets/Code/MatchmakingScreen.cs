using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Aivagames.multiplayer
{
    public class MatchmakingScreen : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks,
        ILobbyCallbacks
    {
        [SerializeField] private ServerSettings _serverSettings;
        [SerializeField] private LobbyPopup _lobby;
        [SerializeField] private RoomPopup _room;
        [SerializeField] private CreateRoomPopup _createRoomPopup;
        [SerializeField] private JoinRoomPopup _joinRoomPopup;
        [SerializeField] private TMP_Text _stateLabel;
        [SerializeField] private ErrorsLogger _errorLogger;

        private LoadBalancingClient _loadBalancingClient;

        private void Start()
        {
            _lobby.OnJoinRoomRequest += OnJoinRoomHandler;
            _lobby.OnCreateRoomRequest += OnCreateRoomHandler;
            _lobby.OnJoinToSelectedRoomRequest += OnJoinToSelectedRoomRequest;

            _joinRoomPopup.OnJoinRoomRequest += OnStartJoinRoom;
            _createRoomPopup.OnCreateRoomRequest += OnStartCreateRoom;

            _room.OnLeaveRoomRequest += OnLeaveRoom;
            _room.OnCloseRoomRequest += OnCloseRoom;

            _loadBalancingClient = new LoadBalancingClient();
            _loadBalancingClient.AddCallbackTarget(this);
            _loadBalancingClient.ConnectUsingSettings(_serverSettings.AppSettings);
        }

        private void OnDestroy()
        {
            _loadBalancingClient.RemoveCallbackTarget(this);

            _lobby.OnJoinRoomRequest -= OnJoinRoomHandler;
            _lobby.OnCreateRoomRequest -= OnCreateRoomHandler;
            _lobby.OnJoinToSelectedRoomRequest -= OnJoinToSelectedRoomRequest;

            _joinRoomPopup.OnJoinRoomRequest -= OnStartJoinRoom;
            _createRoomPopup.OnCreateRoomRequest -= OnStartCreateRoom;

            _room.OnLeaveRoomRequest -= OnLeaveRoom;
            _room.OnCloseRoomRequest -= OnCloseRoom;
        }

        private void Update()
        {
            _loadBalancingClient?.Service();
            _stateLabel.text =
                $"State: {_loadBalancingClient.State}, Region: {_loadBalancingClient.RegionHandler?.BestRegion.Code}, userId: {_loadBalancingClient.UserId}";
        }

        private void OnCreateRoomHandler()
        {
            _createRoomPopup.Show();
        }

        private void OnJoinRoomHandler()
        {
            _lobby.Hide();
            _joinRoomPopup.Show();
        }

        private void OnStartJoinRoom()
        {
            JoinToRoom(_joinRoomPopup.RoomName);
        }

        private void OnJoinToSelectedRoomRequest(string roomName)
        {
            _lobby.Hide();
            JoinToRoom(roomName);
        }

        private void JoinToRoom(string roomName)
        {
            var enterRoomParams = new EnterRoomParams
            {
                RoomName = roomName,
                Lobby = TypedLobby.Default
            };
            _loadBalancingClient.OpJoinRoom(enterRoomParams);
        }

        private void OnLeaveRoom()
        {
            _loadBalancingClient.OpLeaveRoom(false);
            _lobby.Show();
        }

        private void OnCloseRoom()
        {
            _loadBalancingClient.CurrentRoom.IsOpen = false;
        }

        private void OnStartCreateRoom()
        {
            var roomOptions = new RoomOptions
            {
                IsVisible = !_createRoomPopup.IsPrivateRoom,
                MaxPlayers = _createRoomPopup.CountPlayers
            };
            var enterRoomParams = new EnterRoomParams
            {
                RoomName = _createRoomPopup.RoomName,
                RoomOptions = roomOptions,
                Lobby = TypedLobby.Default
            };
            _loadBalancingClient.OpCreateRoom(enterRoomParams);
        }

        private void UpdatePlayersDataInRoom()
        {
            if (_loadBalancingClient.CurrentRoom == null)
            {
                return;
            }

            _room.UpdatePlayersData(_loadBalancingClient.CurrentRoom.Players);
        }

        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
            _lobby.Show();
            _loadBalancingClient.OpJoinLobby(TypedLobby.Default);
        }

        public void OnDisconnected(DisconnectCause cause)
        {
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
        }

        public void OnCreatedRoom()
        {
            _room.Show(_loadBalancingClient.CurrentRoom.Name);
            UpdatePlayersDataInRoom();
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
            _errorLogger.Log($"OnCreateRoomFailed: {message}");
            _lobby.Show();
        }

        public void OnJoinedRoom()
        {
            _room.Show(_loadBalancingClient.CurrentRoom.Name);
            UpdatePlayersDataInRoom();
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            _errorLogger.Log($"OnJoinRoomFailed: {message}");
            _lobby.Show();
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            _errorLogger.Log($"OnJoinRandomFailed: {message}");
            _lobby.Show();
        }

        public void OnLeftRoom()
        {
            UpdatePlayersDataInRoom();
            _lobby.Show();
        }

        public void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
        }

        public void OnLeftLobby()
        {
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log($"OnRoomListUpdate: {roomList}");
            _lobby.UpdateRoomsInfo(roomList);
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            Debug.Log($"OnLobbyStatisticsUpdate: {lobbyStatistics}");
        }

        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            UpdatePlayersDataInRoom();
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdatePlayersDataInRoom();
        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            Debug.Log($"OnRoomPropertiesUpdate: {propertiesThatChanged}");
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            Debug.Log($"OnPlayerPropertiesUpdate: {changedProps}");
        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {
            Debug.Log($"OnMasterClientSwitched: {newMasterClient}");
        }
    }
}