// 2024/07/28 [�̼���]
// ù Ÿ��Ʋ UI ������

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Client
{
    public class SelectSkillPage : UI_Popup
    {
        [SerializeField] private Button            selectBtn = null; // ���� ��ư
        [SerializeField] private SkillScrollSlot[] skillSlot = null; // ��ų ��ũ��

        private List<MagicBookData> magicList = new();

        public override void Init()
        {
            base.Init();
            selectBtn.onClick.AddListener(OnClickSelectBtn);
            magicList = DataManager.Instance.GetAllData<MagicBookData>();
            SetRandomMagic();
        }

        public override void ReOpenPopupUI()
        {
            SetRandomMagic();
        }

        // ������ ���� ��ũ���� �����մϴ�.
        public void SetRandomMagic()
        {
            foreach(SkillScrollSlot slot in skillSlot)
            {
                slot.SetData(GetRandomMagicBook(), SetRandomMagic);
            }
        }

        // ���� ��û�� ���� �� ȣ�� �˴ϴ�.
        public void SetRandomMagic(SkillScrollSlot slot)
        {
            slot.SetData(GetRandomMagicBook(), SetRandomMagic);
        }

        // ������ ���� ��ũ���� �̽��ϴ�.
        private MagicBookData GetRandomMagicBook()
        {
            int randomIndex = Random.Range(0, magicList.Count - 1);
            return magicList[randomIndex];
        }

        // ���� ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickSelectBtn()
        {
            Debug.Log("� ���� �����Կ�");
            if (!BuffManager.Instance.IsChooseOne())
            {
                Debug.Log("�� ���� ���ߴµ���?");
                return;
            }
            Back();

            var myPlayer = EntityManager.Instance.MyPlayer;
            if (myPlayer == null)
            {
                return;
            }
            BuffManager.Instance.SelectMagicBook();
            // TODO : �輱�� �÷��̾� �׾ ��ũ�� ���� �� ��Ȱ
            if (myPlayer.PlayerInfo.IsLive == false )
            {
                EntityManager.Instance.MyPlayer.ReSpawn();
            }
        }
      
    }
}