using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

namespace Aivagames.multiplayer
{
    public class PlayfabAccountManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _infoLabel;
        [SerializeField] protected LoadingScreen _loadingCanvas;

        private void Start()
        {
            _loadingCanvas.Show();
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetInfoSuccess, OnGetInfoFailure);
        }

        private void OnGetInfoSuccess(GetAccountInfoResult result)
        {
            _loadingCanvas.Hide();
            _infoLabel.text = $"{result.AccountInfo.Username},\n{result.AccountInfo.PlayFabId},\n{result.AccountInfo.PrivateInfo.Email}";
            Debug.Log($"{result.AccountInfo.Username} was registered");
        }

        private void OnGetInfoFailure(PlayFabError error)
        {
            _loadingCanvas.Hide();
            Debug.LogError(error.ErrorMessage);
        }
    }
}