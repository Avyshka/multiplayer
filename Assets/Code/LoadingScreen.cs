using TMPro;
using UnityEngine;

namespace Aivagames.multiplayer
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _loadingObject;
        [SerializeField] private Canvas _body;

        private bool _isActive;
        private Color _lerpedColor = Color.white;

        public void Show()
        {
            _isActive = true;
            _body.enabled = _isActive;
        }

        public void Hide()
        {
            _isActive = false;
            _body.enabled = _isActive;
        }

        private void Update()
        {
            if (_isActive)
            {
                _lerpedColor = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time, 1));
                _loadingObject.color = _lerpedColor;
            }
        }
    }
}