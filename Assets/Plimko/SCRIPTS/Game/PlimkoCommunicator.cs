using UnityEngine;
using UnityEngine.Serialization;

namespace rIAEugth.vseioAW.Game
{
    public class PlimkoCommunicator : MonoBehaviour
    {
        public PlimkoAuer plimkoAuer;

        public void OnEnable()
        {
            plimkoAuer.InitData();
        }

        public string spaceLocator;

        public string PlimkoStringData
        {
            get => plimkoStringData;
            set => plimkoStringData = value;
        }

        public int toolbarHeightSpace = 70;

        private string plimkoStringData;
        private UniWebView webView;
        private GameObject loadingIndicator;

        private void Start()
        {
            SetupUI();
            LoadWebPage(plimkoStringData);
            HideLoadingIndicator();
        }

        private void SetupUI()
        {
            InitializeWebView();

            switch (plimkoStringData)
            {
                case "0":
                    webView.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    webView.SetShowToolbar(false);
                    break;
            }

            webView.Frame = new Rect(0, toolbarHeightSpace, Screen.width, Screen.height - toolbarHeightSpace);

            // Other setup logic...

            webView.OnPageFinished += (_, _, url) =>
            {
                if (PlayerPrefs.GetString("LastLoadedPage", string.Empty) == string.Empty)
                {
                    PlayerPrefs.SetString("LastLoadedPage", url);
                }
            };
        }

        private void InitializeWebView()
        {
            webView = GetComponent<UniWebView>();
            if (webView == null)
            {
                webView = gameObject.AddComponent<UniWebView>();
            }

            webView.OnShouldClose += _ => false;
        }

        private void LoadWebPage(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                webView.Load(url);
            }
        }

        private void HideLoadingIndicator()
        {
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
        }
    }
}