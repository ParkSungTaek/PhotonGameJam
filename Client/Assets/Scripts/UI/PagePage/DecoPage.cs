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
        [SerializeField] private PlayerFace    playerUI   = null; // �÷��̾�
        [SerializeField] private DecoTabSlot[] tabBtns    = null; // �ٹ̱� ���� �� ������

        private int faceNum = 0; // ���� ���õ� �� ��ȣ
        private int bodyNum = 0; // ���� ���õ� �� ��ȣ

        private List<DecoSlot>                       items    = new(); // ���� �ٹ̱� ������ ���� List
        private Dictionary<DecoType, List<DecoData>> itemData = new(); // �ٹ̱� ������ ����

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

        // Ư�� ���� ������ �� ȣ��˴ϴ�.
        private void OnClickTabBtn(DecoType type)
        {
            foreach (DecoTabSlot tab in tabBtns)
            {
                tab.OnTabSelected(type);
            }
            RefreshItemList(type);
        }

        // Ư�� �������� ������ �� ȣ��˴ϴ�.
        private void OnClickItemBtn(DecoType type, int selectIndex)
        {
            foreach (DecoSlot slot in items)
            {
                slot.OnTabSelected(type, selectIndex);
            }
            switch (type)
            {
                case DecoType.Face:
                    faceNum = selectIndex;
                    playerUI.SetPlayerDeco(DecoType.Face, faceNum);
                    break;

                case DecoType.Body:
                    bodyNum = selectIndex;
                    playerUI.SetPlayerDeco(DecoType.Body, bodyNum);
                    break;
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
        }

        // �ٹ̱� ������ ������ �ҷ��ɴϴ�.
        public void LoadDecoItemDatas()
        {
            itemData.Clear();
            for (int i = 0; i < (int)DecoType.MaxCount; ++i)
            {
                itemData.Add((DecoType)i, new List<DecoData>());
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