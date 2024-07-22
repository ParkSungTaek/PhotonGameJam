using Client;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{
    /// <summary>
    /// GameManager (MonoBehaviour 용)
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        static GameManager _instance;
        public static GameManager Instance { get { Init(); return _instance; } }
        GameManager() { }

        #endregion

        #region Managers

        //NetworkManager _networkManager = new NetworkManager();
        //public static NetworkManager Network { get { return Instance._networkManager; } }

        #endregion
        // Action 기반 Update (Update 등록이므로 조심해주길 바람)
        private Action OnUpdate { get; set; }
        // Joystick 의 방향백터
        public Vector2 JoystickDirection { get; set; } = Vector2.zero;
        
        private void Start()
        {
            Init();
            //TestManager.Instance.TestDebug();

        }
        private void Update()
        {
            OnUpdate.Invoke();
        }

        /// <summary> instance 생성 산하 매니저들 초기화 </summary>
        private static void Init()
        {
            if (_instance == null)
            {
                GameObject gm = GameObject.Find("GameManager");
                if (gm == null)
                {
                    gm = new GameObject { name = "GameManager" };
                    gm.AddComponent<GameManager>();
                }
                _instance = gm.GetComponent<GameManager>();
                DontDestroyOnLoad(gm);


                //_instance._networkManager.Init();

                //GoogleSheet googleSheet = _instance._networkManager.data;
                //_instance.StartCoroutine(_instance._networkManager.GoogleSheetsDataParsing(googleSheet.associatedSheet, googleSheet.GetData, googleSheet.associatedDataWorksheet));
            }

        }

        /// <summary>
        /// Update 등록
        /// </summary>
        /// <param name="onUpdate"></param>
        public void AddOnUpdate(Action onUpdate)
        {
            // 중복 방지
            OnUpdate -= onUpdate;
            OnUpdate += onUpdate;
        }

        /// <summary>
        ///  Update 제거
        /// </summary>
        /// <param name="onUpdate"></param>
        public void DeleteOnUpdate(Action onUpdate)
        {
            OnUpdate -= onUpdate;
        }

        /// <summary>
        /// (None MonoBehaviour용) StartCoroutine 
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns> coroutine 을 기억해둬야  Stop 가능</returns>
        public Coroutine GMStartCoroutine(IEnumerator enumerator)
        {
            Coroutine coroutine = StartCoroutine(enumerator);
            return coroutine;
        }

        /// <summary>
        /// (None MonoBehaviour용) StopCoroutine
        /// </summary>
        /// <param name="coroutine"></param>
        public void GMStopCoroutine(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}