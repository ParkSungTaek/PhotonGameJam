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

        // 무작위 마법 스크롤을 세팅합니다.
        public void SetRandomMagic()
        {
            foreach(SkillScrollSlot slot in skillSlot)
            {
                slot.SetData(GetRandomMagicBook(), SetRandomMagic);
            }
        }

        // 리롤 요청이 왔을 때 호출 됩니다.
        public void SetRandomMagic(SkillScrollSlot slot)
        {
            slot.SetData(GetRandomMagicBook(), SetRandomMagic);
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
            Debug.Log("어떤 마법 선택함요");
            if (!BuffManager.Instance.IsChooseOne())
            {
                Debug.Log("엥 선택 안했는데요?");
                return;
            }
            Back();

            var myPlayer = EntityManager.Instance.MyPlayer;
            if (myPlayer == null)
            {
                return;
            }
            BuffManager.Instance.SelectMagicBook();
            // TODO : 김선중 플레이어 죽어서 스크롤 선택 시 부활
            if (myPlayer.PlayerInfo.IsLive == false )
            {
                EntityManager.Instance.MyPlayer.ReSpawn();
            }
        }
      
    }
}