// 2024/07/28 [이서연]
// 첫 타이틀 UI 페이지

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Client
{
    public class SelectSkillPage : UI_Popup
    {
        [SerializeField] private Button            selectBtn = null; // 선택 버튼
        [SerializeField] private SkillScrollSlot[] skillSlot = null; // 스킬 스크롤

        private List<MagicBookData> magicList = new(); // 마법 데이터들
        private SkillScrollSlot selectedMagic = new(); // 선택 된 마법

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

        // 무작위 마법 스크롤을 세팅합니다.
        public void SetRandomMagic()
        {
            foreach(SkillScrollSlot slot in skillSlot)
            {
                slot.SetData(GetRandomMagicBook(), SetRandomMagic, SelectMagic);
            }
        }

        // 리롤 요청이 왔을 때 호출 됩니다.
        public void SetRandomMagic(SkillScrollSlot slot)
        {
            slot.SetData(GetRandomMagicBook(), SetRandomMagic, SelectMagic);
        }

        // 어떤 마법을 선택했을 때 호출 됩니다.
        public void SelectMagic(SkillScrollSlot slot)
        {
            selectedMagic = slot;
            foreach ( var magic in skillSlot)
            {
                magic.OnSelectMagic(selectedMagic);
            }
        }

        // 무작위 마법 스크롤을 뽑습니다.
        private MagicBookData GetRandomMagicBook()
        {
            int randomIndex = Random.Range(0, magicList.Count - 1);
            return magicList[randomIndex];
        }

        // 마법 선택 버튼을 눌렀을 때 호출됩니다.
        private void OnClickSelectBtn()
        {
            var myPlayer = EntityManager.Instance.MyPlayer;
            if (myPlayer == null)
            {
                return;
            }
            BuffManager.Instance.SelectMagicBook(selectedMagic.MagicBookData);

            // TODO : 김선중 플레이어 죽어서 스크롤 선택 시 부활
            if (myPlayer.PlayerInfo.IsLive == false )
            {
                EntityManager.Instance.MyPlayer.ReSpawn();
            }
        }
      
    }
}