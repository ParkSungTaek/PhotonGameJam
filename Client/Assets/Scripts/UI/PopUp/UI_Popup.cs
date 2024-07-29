using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Client
{
    /// <summary>
    /// Popup UI를 위한 데이터 타입
    /// </summary>
    public abstract class UI_Popup : UI_Base
    {
        [SerializeField] private Button dim = null;
        [SerializeField] private Button closeBtn = null;
        public override void Init()
        {
            base.Init();
            UIManager.Instance.SetCanvas(gameObject, true);

            if(dim != null)
                dim.onClick.AddListener(Back);
            if (closeBtn != null)
                closeBtn.onClick.AddListener(Back);
        }

        /// <summary> pop up 다시 열 때마다 실행</summary>
        public virtual void ReOpenPopupUI() { }

        public override void Back()
        {
            base.Back();
            UIManager.Instance.PopPopupUI();
        }
    }
}