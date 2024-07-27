using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    /// <summary>
    /// 주의 만분률 이전 Raw값
    /// </summary>
    public class PlayerData : SheetData
    {
        // 내부 액세스 필드 (지금 테이블 제너레이터가 없어서 일단 상수넣음)
        private PlayerCharType _player = PlayerCharType.Normal; // 플레이어 타입

        private int _movSpd    = 30000;   // 플레이어 속도
        private int _jumpPower = 50000;   // 플레이어 점프 힘
        private int _att       = 15000;   // 공격속도
        private int _hp        = 1000000; // HP


        // 외부 액세스 필드
        public PlayerCharType Player => _player; // 플레이어 타입

        public int Speed => _movSpd;        // 플레이어 속도
        public int JumpPower => _jumpPower; // 플레이어 점프 힘

        public int HP => _hp;   // HP
        public int Att => _att; // Atk

        public override Dictionary<int, SheetData> LoadData()
        {
            return new Dictionary<int, SheetData>();
        }
    }
}