// 2024/07/28 [�̼���]
// ù Ÿ��Ʋ UI ������

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Client.SystemEnum;

namespace Client
{
    public class DecoPage : UI_Scene
    {
        [SerializeField] private Button        backBtn    = null; // �ڷ� ���� ��ư
        [SerializeField] private Button        saveBtn    = null; // ���� �Ա� ��ư
        [SerializeField] private DecoSlot      decoPrepeb = null; // �ٹ̱� ���� ������
        [SerializeField] private ScrollRect    scroll     = null; // �ٹ̱� ���� ��ũ��
        [SerializeField] private TopPageBar    topBar     = null; // ��ܹ�
        [SerializeField] private PlayerFaceUI  playerUI   = null; // �÷��̾�
        [SerializeField] private DecoTabSlot[] tabBtns    = null; // �ٹ̱� ���� �� ������

        private List<DecoSlot>                       items      = new(); // ���� �ٹ̱� ������ ���� List
        private Dictionary<DecoType, DecoData>       selectData = new(); // ���õ� �ٹ̱� ������ ����
        private Dictionary<DecoType, List<DecoData>> itemData   = new(); // �ٹ̱� ������ ����

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

        // ó�� �������� ������ ��, �÷��̾��� �������� ����ϴ�. ( ���ٸ� default )
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

        // Ư�� ���� ������ �� ȣ��˴ϴ�.
        private void OnClickTabBtn(DecoType type)
        {
            foreach (DecoTabSlot tab in tabBtns)
            {
                tab.OnTabSelected(type);
            }
            RefreshItemList(type);
            OnClickItemBtn(type, selectData[type]);
        }

        // Ư�� �������� ������ �� ȣ��˴ϴ�.
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

        // ������ ��ũ���� �ش� Ÿ������ �����մϴ�.
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

        // ���� �Ա� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickSaveBtn()
        {
            foreach (var decoInfo in selectData)
            {
                MyInfoManager.Instance.SetDecoData(decoInfo.Key, selectData[decoInfo.Key]);
            }
        }

        // �ٹ̱� ������ ������ �ҷ��ɴϴ�.
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