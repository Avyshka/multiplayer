using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.multiplayer
{
    public class Launcher : MonoBehaviourPunCallbacks, IDisposable
    {
        private const string ConnectText = "CONNECT";
        private const string DisconnectText = "DISCONNECT";
        private readonly Color ConnectColor = new Color(0.85f, 1.0f, 0.3f);
        private readonly Color DisconnectColor = new Color(1.0f, 0.6f, 0.7f);

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _infoLabel;
        [SerializeField] private TMP_Text _loginButtonLabel;
        [SerializeField] private Button _loginButton;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            _title.text = "Photon login";
            _loginButton.onClick.AddListener(OnConnectButtonClicked);
        }

        private void OnConnectButtonClicked()
        {
            _loginButton.interactable = false;
            if (PhotonNetwork.IsConnected)
            {
                Disconnect();
            }
            else
            {
                Connect();
            }
        }

        private void Connect()
        {
            UpdateConnectionInfo("Connecting...", Color.white);
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = Application.version;
        }

        private void Disconnect()
        {
            UpdateConnectionInfo("Disconnecting...", Color.white);
            PhotonNetwork.Disconnect();
        }

        private void JoinOrCreateRoom()
        {
            PhotonNetwork.JoinOrCreateRoom(
                "RoomName",
                new RoomOptions {MaxPlayers = 2, IsVisible = true},
                TypedLobby.Default
            );
        }

        private void UpdateLoginButton(string txt, Color color)
        {
            _loginButton.image.color = color;
            _loginButtonLabel.text = txt;
        }

        private void UpdateConnectionInfo(string txt, Color color = default)
        {
            _infoLabel.color = color;
            _infoLabel.text = txt;
            Debug.Log(txt);
        }

        public override void OnConnectedToMaster()
        {
            UpdateConnectionInfo("[Photon]: OnConnectedToMaster", Color.green);
            JoinOrCreateRoom();
        }

        public override void OnJoinedRoom()
        {
            UpdateConnectionInfo("[Photon]: OnJoinedRoom", Color.green);
            UpdateLoginButton(DisconnectText, DisconnectColor);
            _loginButton.interactable = true;
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            UpdateConnectionInfo("[Photon]: OnDisconnected", Color.red);
            UpdateLoginButton(ConnectText, ConnectColor);
            _loginButton.interactable = true;
        }

        public void Dispose()
        {
            _loginButton.onClick.RemoveAllListeners();
        }
    }
}