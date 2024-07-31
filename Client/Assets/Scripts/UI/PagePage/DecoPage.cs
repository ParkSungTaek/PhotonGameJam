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
        [SerializeField] private PlayerFaceUI  playerUI   = null; // 플레이어
        [SerializeField] private DecoTabSlot[] tabBtns    = null; // 꾸미기 슬롯 탭 프리펩

        private List<DecoSlot>                       items      = new(); // 현재 꾸미기 아이템 슬롯 List
        private Dictionary<DecoType, DecoData>       selectData = new(); // 선택된 꾸미기 아이템 정보
        private Dictionary<DecoType, List<DecoData>> itemData   = new(); // 꾸미기 아이템 정보

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
            SetPlayerDeco();
        }

        // 처음 페이지에 들어왔을 때, 플레이어의 세팅으로 맞춥니다. ( 없다면 default )
        private void SetPlayerDeco()
        {
            Dictionary<DecoType, DecoData> myDecoData = MyInfoManager.Instance.GetDecoData();
            foreach ( var decoData in itemData )
            {
                if( myDecoData.ContainsKey(decoData.Key))
                {
                    selectData[decoData.Key] = myDecoData[decoData.Key];
                    continue;
                }
                selectData[decoData.Key] = itemData[decoData.Key][0];
            }
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
            OnClickItemBtn(type, selectData[type]);
        }

        // 특정 아이템이 눌렸을 때 호출됩니다.
        private void OnClickItemBtn(DecoType type, DecoData data)
        {
            switch (type)
            {
                case DecoType.Face:
                    if(data == null)
                    {
                        data = itemData[DecoType.Face][0];
                    }
                    playerUI.SetPlayerDeco(DecoType.Face, data);
                    selectData[DecoType.Face] = data;
                    break;

                case DecoType.Body:
                    if (data == null)
                    {
                        data = itemData[DecoType.Body][0];
                    }
                    playerUI.SetPlayerDeco(DecoType.Body, data);
                    selectData[DecoType.Body] = data;
                    break;
            }
            foreach (DecoSlot slot in items)
            {
                slot.OnTabSelected(type, selectData[type]);
            }
        }

        // 아이템 스크롤을 해당 타입으로 갱신합니다.
        private void RefreshItemList(DecoType type)
        {
            items.Clear();
            Transform content = scroll.content;
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
            foreach (DecoData data in itemData[type])
            {
                DecoSlot newSlot = Instantiate(decoPrepeb, content);
                newSlot.SetData(OnClickItemBtn, data);
                items.Add(newSlot);
            }
        }

        // 갈아 입기 버튼을 눌렀을 때 호출됩니다.
        private void OnClickSaveBtn()
        {
            foreach (var decoInfo in selectData)
            {
                MyInfoManager.Instance.SetDecoData(decoInfo.Key, selectData[decoInfo.Key]);
            }
        }

        // 꾸미기 아이템 정보를 불러옵니다.
        public void LoadDecoItemDatas()
        {
            itemData.Clear();
            selectData.Clear();
            for (int i = 0; i < (int)DecoType.MaxCount; ++i)
            {
                itemData.Add((DecoType)i, new List<DecoData>());
                selectData.Add((DecoType)i, null);
            }

            List<DecoData> itemDataList = DataManager.Instance.GetAllData<DecoData>();
            if (itemDataList == null) return;

            foreach (DecoData data in itemDataList)
            {
                itemData[data._type].Add(data);
            }
        }
    }
}