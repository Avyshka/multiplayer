using Photon.Pun;
using UnityEngine;

namespace Aivagames.multiplayer
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            Connect();
        }

        private void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = Application.version;
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("[Photon]: OnConnectedToMaster");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("[Photon]: OnJoinedRoom");
        }
    }
}