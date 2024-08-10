// 2024/07/28 [이서연]
// 인게임 UI 페이지

using UnityEngine;
using TMPro;

namespace Client
{
    public class GameEndPage : UI_Scene
    {
        [SerializeField] private TMP_Text winPlayer = null; // 플레이어1 이름
        public override void Init()
        {
            base.Init();
        }

        public void SetPlayerName(string name)
        {
            winPlayer.text = name;
        }
    }
}