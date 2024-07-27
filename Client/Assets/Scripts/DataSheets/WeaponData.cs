using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class WeaponData : SheetData
    {

        // 내부 액세스 필드 (지금 테이블 제너레이터가 없어서 일단 상수넣음)
        private WeaponType _player = WeaponType.Normal; // 플레이어 타입

        private int _atkSpd = 30000;   // 공격 속도

        // 외부 엑세스 필드
        public int AtkSpd => _atkSpd;   // 공격 속도

        public override Dictionary<int, SheetData> LoadData()
        {
            return new Dictionary<int, SheetData>();
        }
    }
}
