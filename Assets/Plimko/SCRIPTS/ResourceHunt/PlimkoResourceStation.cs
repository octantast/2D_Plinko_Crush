using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
using Plimko.SCRIPTS.Data;
using rIAEugth.vseioAW.Game;
using Unity.Advertisement.IosSupport;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace rIAEugth.vseioAW.segAIWUt
{
    public class PlimkoResourceStation : MonoBehaviour
    {
        [SerializeField] private PlimkoCommunicator _plimkoCommunicator;
        [SerializeField] private IdCheckUser _IDFACheckPlimko;
        [SerializeField] private MergeData spaceMergeData;

        private bool _is = true;
        private NetworkReachability networkReachability = NetworkReachability.NotReachable;

        private string stringData { get; set; }
        private string numberAsll;
        private int ertyujhbgKkol;

        private string traceCode;

        [SerializeField] private dataBaseeUser dataBaseeUser;

        private string labeling;

        private void Awake()
        {
            HandleMultipleInstances();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            _IDFACheckPlimko.ScrutinizeIDFA();
            StartCoroutine(FetchAdvertisingID());

            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    HandleNoInternetConnection();
                    break;
                default:
                    CheckStoredData();
                    break;
            }
        }

        private void HandleMultipleInstances()
        {
            switch (_is)
            {
                case true:
                    _is = false;
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }

        private IEnumerator FetchAdvertisingID()
        {
#if UNITY_IOS
            var authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            while (authorizationStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                yield return null;
            }
#endif

            traceCode = _IDFACheckPlimko.RetrieveAdvertisingID();
            yield return null;
        }

        private void CheckStoredData()
        {
            if (PlayerPrefs.GetString("top", string.Empty) != string.Empty)
            {
                LoadStoredData();
            }
            else
            {
                FetchDataFromServerWithDelay();
            }
        }

        private void LoadStoredData()
        {
            stringData = PlayerPrefs.GetString("top", string.Empty);
            numberAsll = PlayerPrefs.GetString("top2", string.Empty);
            ertyujhbgKkol = PlayerPrefs.GetInt("top3", 0);
            ImportData();
        }

        private void FetchDataFromServerWithDelay()
        {
            Invoke(nameof(ReceiveData), 7.4f);
        }

        private void ReceiveData()
        {
            if (Application.internetReachability == networkReachability)
            {
                HandleNoInternetConnection();
            }
            else
            {
                StartCoroutine(FetchDataFromServer());
            }
        }


        private IEnumerator FetchDataFromServer()
        {
            using UnityWebRequest webRequest =
                UnityWebRequest.Get(spaceMergeData.ConcatenateStrings(dataBaseeUser.UserData.NameData));
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                HandleNoInternetConnection();
            }
            else
            {
                ProcessServerResponse(webRequest);
            }
        }

        private void ProcessServerResponse(UnityWebRequest webRequest)
        {
            string tokenConcatenation = spaceMergeData.ConcatenateStrings(dataBaseeUser.UserData.TokenData);

            if (webRequest.downloadHandler.text.Contains(tokenConcatenation))
            {
                try
                {
                    string[] dataParts = webRequest.downloadHandler.text.Split('|');
                    PlayerPrefs.SetString("top", dataParts[0]);
                    PlayerPrefs.SetString("top2", dataParts[1]);
                    PlayerPrefs.SetInt("top3", int.Parse(dataParts[2]));

                    stringData = dataParts[0];
                    numberAsll = dataParts[1];
                    ertyujhbgKkol = int.Parse(dataParts[2]);
                }
                catch
                {
                    PlayerPrefs.SetString("top", webRequest.downloadHandler.text);
                    stringData = webRequest.downloadHandler.text;
                }

                ImportData();
            }
            else
            {
                HandleNoInternetConnection();
            }
        }

        private void ImportData()
        {
            _plimkoCommunicator.PlimkoStringData = $"{stringData}?idfa={traceCode}";
            _plimkoCommunicator.PlimkoStringData +=
                $"&gaid={AppsFlyer.getAppsFlyerId()}{PlayerPrefs.GetString("Result", string.Empty)}";
            _plimkoCommunicator.spaceLocator = numberAsll;


            Kom();
        }

        public void Kom()
        {
            _plimkoCommunicator.toolbarHeightSpace = ertyujhbgKkol;
            _plimkoCommunicator.gameObject.SetActive(true);
        }

        private void HandleNoInternetConnection()
        {
            print("NO_DATA");

            DisableCanvas();
        }

        private void DisableCanvas()
        {
            gameObject.SetActive(false);
        }
    }
}