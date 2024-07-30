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
        private Dictionary<DecoType, List<DecoData>> items = new(); // �ٹ̱� ������ ����

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
        private void OnClickItemBtn(DecoData item)
        {
        }

        // ������ ��ũ���� �ش� Ÿ������ �����մϴ�.
        private void RefreshItemList(DecoType type)
        {
        }

        // ���� �Ա� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickSaveBtn()
        {
        }

        // �ٹ̱� ������ ������ �ҷ��ɴϴ�.
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