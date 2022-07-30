using TMPro;
using UnityEngine;

namespace Aivagames.multiplayer
{
    public class ErrorsLogger : MonoBehaviour
    {
        private const float MSG_TIME = 4f;

        [SerializeField] private TMP_Text _msgLabel;

        private float _timer;

        public void Log(string msg)
        {
            _timer = MSG_TIME;
            _msgLabel.text = msg;
        }

        private void Update()
        {
            if (_msgLabel.text == "")
                return;

            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _msgLabel.text = "";
            }
        }
    }
}