using SafeAreaRect.Internal;
using UnityEngine;

namespace SafeAreaRect
{
    /// Apply SafeArea only to the horizontal direction of the device, mainly intended for use in Landscape
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaRectSideOnly : MonoBehaviour
    {
        private RectTransform _rectTf;

        private Rect _lastSafeArea;
        private int _lastScreenWidth;
        private int _lastScreenHeight;

        private void Awake()
        {
            _rectTf = GetComponent<RectTransform>();
            UpdateRect();
        }

        private void Update()
        {
            UpdateRect();
        }

        private void UpdateRect()
        {
            var safeArea = Screen.safeArea;
            var screenWidth = Screen.width;

            // is same values
            if (safeArea.Equals(_lastSafeArea) && _lastScreenWidth == screenWidth)
            {
                return;
            }

            ApplySafeArea(safeArea, screenWidth);

            _lastSafeArea = safeArea;
            _lastScreenWidth = screenWidth;
        }

        private void ApplySafeArea(Rect safeArea, int screenWidth)
        {
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= screenWidth;
            anchorMax.x /= screenWidth;
            // ignore y for side only
            anchorMin.y = 0;
            anchorMax.y = 1;

            _rectTf.anchoredPosition = Vector2.zero;
            _rectTf.sizeDelta = Vector2.zero;
            _rectTf.anchorMin = anchorMin.IsFinite() ? anchorMin : Vector2.zero;
            _rectTf.anchorMax = anchorMax.IsFinite() ? anchorMax : Vector2.one;
        }
    }
}
