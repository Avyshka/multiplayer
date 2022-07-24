using TMPro;
using UnityEngine;

namespace Aivagames.multiplayer
{
    public class PlayerDataItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerNick;

        public void setData(string playerNick)
        {
            _playerNick.text = playerNick;
        }
    }
}