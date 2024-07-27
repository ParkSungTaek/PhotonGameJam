using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// UI매니저 
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        /// <summary>
        /// 팝업 UI 관리를 위한 stack
        /// </summary>
        [Header("Pop Up")]
        Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
        Stack<UI_Scene> _pageStack = new Stack<UI_Scene>();
        /// <summary>
        /// popup ui 정렬 순서를 위한 변수
        /// </summary>
        int _order = 1;

        /// <summary>
        /// popup 재사용을 위한 캐싱
        /// </summary>
        Dictionary<System.Type, GameObject> _popupInstances = new Dictionary<System.Type, GameObject>();
        Dictionary<System.Type, GameObject> _pageInstances = new Dictionary<System.Type, GameObject>();

        #region 생성자
        UIManager() { }
        #endregion 생성자

        /// <summary>
        /// UI의 부모 
        /// </summary>
        [Header("Root")]
        GameObject _root = null;
        public GameObject Root
        {
            get
            {
                if (_root == null)
                {
                    if ((_root = GameObject.Find("UIRoot")) == null)
                        _root = new GameObject { name = "UIRoot" };
                }
                return _root;
            }
        }

        /// <summary>
        /// game object에 canvas 속성 부여, 정렬 설정
        /// </summary>
        /// <param name="go">canvas 속성이 있는 게임 오브젝트</param>
        /// <param name="sort">canvas 정렬 여부(popup->true, scene->false)</param>
        public void SetCanvas(GameObject go, bool sort = true, int order = 0)
        {
            Canvas canvas = Util.GetOrAddComponent<Canvas>(go);

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;

            if (sort)
                canvas.sortingOrder = _order++;
            else
                canvas.sortingOrder = order;
        }

        // 최상단 페이지를 삭제합니다.
        public void PopSceneUI()
        {
            if (_pageStack.Count <= 1) return;

            UI_Scene page = _pageStack.Pop();
            page.gameObject.SetActive(false);
            _pageStack.Peek().gameObject.SetActive(true);
        }

        // 최상단 페이지를 삭제합니다.
        public void PopPopupUI()
        {
            if (_popupStack.Count <= 0) return;

            UI_Popup popup = _popupStack.Pop();
            popup.gameObject.SetActive(false);
        }

        public T ShowSceneUI<T>(string name = null) where T : UI_Scene
        {
            if(string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject page;
            T pageUI;

            // 다른 페이지들 모두 비활성화
            foreach( var p in _pageInstances)
            {
                p.Value.SetActive(false);
            }

            //이전에 띄운 기록 없음 -> 생성
            if (_pageInstances.TryGetValue(typeof(T), out page) == false)
            {
                page = ObjectManager.Instance.Instantiate($"UI/Page/{name}");
                _pageInstances.Add(typeof(T), page);
                pageUI = Util.GetOrAddComponent<T>(page);
            }
            //이전에 띄운 기록 있음 -> 재활성화
            else
            {
                pageUI = Util.GetOrAddComponent<T>(page);
                pageUI.GetComponent<Canvas>().sortingOrder = _order++;
            }
            _pageStack.Push(pageUI);
            page.transform.SetParent(Root.transform);
            page.SetActive(true);
            return pageUI;
        }

        /// <summary>
        /// Pop Up UI 띄우기
        /// </summary>
        /// <typeparam name="T">UI_PopUp을 상속받은 Pop up UI</typeparam>
        /// <param name="name">Pop Up UI 이름, null이면 T 이름</param>
        public T ShowPopupUI<T>(string name = null) where T : UI_Popup
        {
            if(string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject popup;
            T popupUI;

            //이전에 띄운 기록 없음 -> 생성
            if (_popupInstances.TryGetValue(typeof(T), out popup) == false)
            {
                popup = ObjectManager.Instance.Instantiate($"UI/PopupPage/{name}");
                _popupInstances.Add(typeof(T), popup);

                popupUI = Util.GetOrAddComponent<T>(popup);
                
            }
            //이전에 띄운 기록 있음 -> 재활성화
            else
            {
                popupUI = Util.GetOrAddComponent<T>(popup);
                popupUI.ReOpenPopupUI();
                popupUI.GetComponent<Canvas>().sortingOrder = _order++;
            }

            _popupStack.Push(popupUI);

            popup.transform.SetParent(Root.transform);
            popup.SetActive(true);

            return popupUI;
        }

        /// <summary>
        /// 모든 pop up UI 닫기
        /// </summary>
        public void CloseAllPopUpUI()
        {
            while (_popupStack.Count > 0)
                PopPopupUI();
        }

        /// <summary>
        /// 이미 Scene에 존재하는 SceneUI를 출력한다. 없다면 그대로null (작업 무거움 주의)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T FindSceneUI<T>(string name = null) where T : UI_Scene
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            T sceneUI = GameObject.FindAnyObjectByType<T>();

            return sceneUI;
        }

        /// <summary>
        /// 이미 Scene에 존재하는 PopupUI를 출력한다. 없다면 그대로null (작업 무거움 주의)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T FindPopUpUI<T>(string name = null) where T : UI_Popup
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            T sceneUI = GameObject.FindAnyObjectByType<T>();

            return sceneUI;
        }

        /// <summary>
        /// UI 초기화
        /// </summary>
        public void Clear()
        {
            CloseAllPopUpUI();
            _popupInstances.Clear();
            _pageInstances.Clear();
        }
    }
}