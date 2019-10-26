using SafeAreaRect.Screens;
using UnityEngine;

namespace SafeAreaRect
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaRect : MonoBehaviour
    {
        public bool UpdateEveryFrame = true;

        private RectTransform _rectTf;
        private Rect _lastSafeArea;

        private void Awake()
        {
            _rectTf = GetComponent<RectTransform>();
            UpdateRect();
        }

        private void Update()
        {
            if (UpdateEveryFrame || Application.isEditor)
            {
                UpdateRect();
            }
        }

        public void UpdateRect()
        {
#if UNITY_EDITOR && UNITY_IOS
            var safeArea = iOSScreenTypeResolver.Resolve().SafeArea;
#else
            var safeArea = Screen.safeArea;
#endif
            ApplySafeArea(safeArea);
        }

        private void ApplySafeArea(Rect safeArea)
        {
            if (safeArea == _lastSafeArea) return;
            _rectTf.anchoredPosition = Vector2.zero;
            _rectTf.sizeDelta = Vector2.zero;

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            _rectTf.anchorMin = anchorMin;
            _rectTf.anchorMax = anchorMax;

            _lastSafeArea = safeArea;
        }
    }
}