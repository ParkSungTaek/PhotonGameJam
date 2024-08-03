// 2024/08/03 [이서연]
// 플레이어 슬롯 ( 친구 리스트나 보이스 표시에서 사용 예정 )

using UnityEngine;
using TMPro;
using static Client.SystemEnum;
using System.Collections.Generic;

namespace Client
{
    public class FriendSlot : UI_Base
    {
        [SerializeField] private TMP_Text     name         = null; // 닉네임
        [SerializeField] private GameObject   onlineGroup  = null; // 온라인 그룹
        [SerializeField] private GameObject   offlineGroup = null; // 오프라인 그룹
        [SerializeField] private PlayerFaceUI playerUI     = null; // 플레이어 외형

        public void SetData(string name, bool isOnline, Dictionary<DecoType, DecoData> decoData)
        {
            this.name.SetText(name);
            SetOnline(isOnline);
            playerUI.SetPlayerDeco(decoData);
        }

        // 온/오프라인을 세팅합니다.
        public void SetOnline(bool isOnline)
        {
            onlineGroup.SetActive(isOnline);
            offlineGroup.SetActive(!isOnline);
        }
    }
}