// 2024/07/28 [이서연]
// 첫 타이틀 UI 페이지

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Client.SystemEnum;

namespace Client
{
    public class DecoPage : UI_Scene
    {
        [SerializeField] private Button        backBtn    = null; // 뒤로 가기 버튼
        [SerializeField] private Button        saveBtn    = null; // 갈아 입기 버튼
        [SerializeField] private DecoSlot      decoPrepeb = null; // 꾸미기 슬롯 프리펩
        [SerializeField] private ScrollRect    scroll     = null; // 꾸미기 슬롯 스크롤
        [SerializeField] private TopPageBar    topBar     = null; // 상단바
        [SerializeField] private PlayerFace    playerUI   = null; // 플레이어
        [SerializeField] private DecoTabSlot[] tabBtns    = null; // 꾸미기 슬롯 탭 프리펩

        private int faceNum = 0; // 현재 선택된 얼굴 번호
        private int bodyNum = 0; // 현재 선택된 몸 번호
        private Dictionary<DecoType, List<DecoData>> items = new(); // 꾸미기 아이템 정보

        public override void Init()
        {
            base.Init();
            topBar.Init(this);
            backBtn.onClick.AddListener(Back);
            saveBtn.onClick.AddListener(OnClickSaveBtn);
            foreach(DecoTabSlot tab in tabBtns)
            {
                tab.SetData(OnClickTabBtn);
            }
            LoadDecoItemDatas();

            OnClickTabBtn(DecoType.Body);
        }

        // 특정 탭이 눌렸을 때 호출됩니다.
        private void OnClickTabBtn(DecoType type)
        {
            foreach (DecoTabSlot tab in tabBtns)
            {
                tab.OnTabSelected(type);
            }
            RefreshItemList(type);
        }

        // 특정 아이템이 눌렸을 때 호출됩니다.
        private void OnClickItemBtn(DecoData item)
        {
        }

        // 아이템 스크롤을 해당 타입으로 갱신합니다.
        private void RefreshItemList(DecoType type)
        {
        }

        // 갈아 입기 버튼을 눌렀을 때 호출됩니다.
        private void OnClickSaveBtn()
        {
        }

        // 꾸미기 아이템 정보를 불러옵니다.
        public void LoadDecoItemDatas()
        {
            items.Clear();
            for (int i = 0; i < (int)DecoType.MaxCount; ++i)
            {
                items.Add((DecoType)i, new List<DecoData>());
            }

            List<DecoData> itemDataList = DataManager.Instance.GetAllData<DecoData>();
            if (itemDataList == null) return;

            foreach (DecoData data in itemDataList)
            {
                items[data._type].Add(data);
            }
        }
    }
}