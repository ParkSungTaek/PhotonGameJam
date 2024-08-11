// 2024/08/03 [이서연]
// 플레이어 슬롯 ( 친구 리스트나 보이스 표시에서 사용 예정 )

using UnityEngine;
using TMPro;
using static Client.SystemEnum;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Client
{
    public class PlayerInfoSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text          name         = null; // 닉네임
        [SerializeField] private PlayerFaceUI      playerUI     = null; // 플레이어 외형
        [SerializeField] private SkillScrollSlot[] skills       = null; // 스킬들

        // 플레이어 데이터를 세팅합니다.
        public void SetData(PlayerInfo player)
        {
            this.name.SetText(player.CharName);
            playerUI.SetPlayerDeco(player.DecoData);
            foreach( var slot in skills )
            {
                slot.gameObject.SetActive(false);
            }

            if( player.MagicLists.Count > 0)
            {
                for (int i = 0; i < player.MagicLists.Count; ++i)
                {
                    skills[i].gameObject.SetActive(true);
                    skills[i].SetData(player.MagicLists[i], null, null);
                }
            }

        }
    }
}