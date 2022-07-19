using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

namespace Aivagames.multiplayer
{
    public class PlayfabAccountManager : MonoBehaviour
    {
        private const string REAL_MONEY_ID = "RM";
        private const string TEST_COINS_ID = "TC";
        
        [SerializeField] private TMP_Text _infoLabel;
        [SerializeField] private LoadingScreen _loadingCanvas;
        [SerializeField] private GameObject _catalogList;
        [SerializeField] private CatalogItemInfo _catalogItemPrefab;

        private List<CatalogItem> _catalog;

        private void Start()
        {
            _loadingCanvas.Show();
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetInfoSuccess, OnGetInfoFailure);
        }

        private void OnGetInfoSuccess(GetAccountInfoResult result)
        {
            _loadingCanvas.Hide();
            _infoLabel.text = $"{result.AccountInfo.Username},\n{result.AccountInfo.PrivateInfo.Email}";

            _loadingCanvas.Show();
            PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalogSuccess, OnGetInfoFailure);
        }

        private void OnGetCatalogSuccess(GetCatalogItemsResult result)
        {
            _loadingCanvas.Hide();
            _catalog = result.Catalog;
            ShowCatalog();
        }

        private void ShowCatalog()
        {
            foreach (var item in _catalog)
            {
                var itemPrefab = Instantiate(_catalogItemPrefab, _catalogList.transform);
                if (itemPrefab.TryGetComponent(out CatalogItemInfo catalogItem))
                {
                    catalogItem.ItemTitle.text = item.DisplayName;
                    catalogItem.Description.text = item.Description;
                    if (item.VirtualCurrencyPrices.TryGetValue(REAL_MONEY_ID, out var priceRM))
                    {
                        catalogItem.RealMoney.text = $"{REAL_MONEY_ID}: {GetConvertedPrice(priceRM)}";
                    }

                    if (item.VirtualCurrencyPrices.TryGetValue(TEST_COINS_ID, out var priceTC))
                    {
                        catalogItem.TestCoins.text = $"{TEST_COINS_ID}: {priceTC}";
                    }
                }
            }
        }

        private string GetConvertedPrice(uint value)
        {
            var price = (float) value / 100;
            return price.ToString("0.00");
        }

        private void OnGetInfoFailure(PlayFabError error)
        {
            _loadingCanvas.Hide();
            Debug.LogError(error.ErrorMessage);
        }
    }
}