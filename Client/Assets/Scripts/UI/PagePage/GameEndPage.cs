// 2024/07/28 [�̼���]
// �ΰ��� UI ������

using UnityEngine;
using TMPro;

namespace Client
{
    public class GameEndPage : UI_Scene
    {
        [SerializeField] private TMP_Text winPlayer = null; // �÷��̾�1 �̸�
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