using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{
    /// <summary>
    /// 게임에 필요한 Enum
    /// </summary>
    public class SystemEnum
    {
        public enum Sounds
        {
            BGM,
            SFX,
            Voice,
            MaxCount
        }
        public enum BGM
        {

            MaxCount
        }
        public enum SFX
        {
            MaxCount
        }
        public enum Condition
        {
            None,
            Stun,
            MaxCount
        }

        public enum Tag
        {
            MaxCount
        }

        public enum CommonPopuptype
        {
            OneBtn,
            TwoBtn,
            MaxVount
        }

        /// <summary>
        /// UI Event 종류 지정
        /// </summary>
        public enum UIEvent
        {
            Click,
            Drag,
            DragEnd,

            MaxCount
        }

        public enum Scenes
        {
            Lobby,
            Loading,
            InGame,
            MaxCount
        }
        public enum EntityType
        {
            None,
            Player,
            Weapon,
            Projectile,

            MaxCount
        }

        /// <summary>
        /// 버프 대분류
        /// </summary>
        public enum BuffType
        {
            None,
            Buff,       // 긍정적 버프
            Debuff,     // 부정정 버프
            State,      // 이동속도 감소, 스턴 등

            MaxCount

        }

        /// <summary>
        /// 버프 소분류
        /// </summary>
        public enum Buffs
        {
            None,


            MaxCount
        }

        /// <summary>
        /// 유 불리 보정용 데이터
        /// </summary>
        public enum ValidityType
        {
            None,
            Normal,
            Fire,
            Water,
            Grass,

            MaxCount
        }

        /// <summary>
        /// 마법 스크롤 타입(속성)
        /// </summary>
        public enum MagicElement
        {
            None,
            Normal,
            Fire,
            Water,
            Nature,

            MaxCount
        }

        /// <summary>
        /// 플레이어 데이터테이블 인덱스와 대응용 enumm
        /// </summary>
        public enum PlayerCharName
        {
            None   = 0,
            Normal = 1,

            MaxCount
        }

        public enum WeaponName
        {
            None,
            Pistol,
            MaxCount
        }

        public enum ProjectileName
        {
            None,
            Bullet,
            MaxCount
        }


        /// <summary>
        /// 유 불리 보정용 데이터
        /// </summary>
        public enum WeaponType
        {
            None,
            Normal,
            Magic,
            Explosion,

            MaxCount
        }


        // N : Now 현재/ M : Max 최대
        public enum EntityStat
        {
            None,

            HP,      // 기본 HP
            MovSpd,  // 기본 Speed
            Att,     // 기본 공격력
            Def,     // 기본 방어력
            AtkSpd,  // 기본 공격속도
            JumpP,   // 기본 점프 힘
            Reload,  // 장전 속도

            MaxCount
        }

        /// 꾸미기 아이템 타입(부위)
        public enum DecoType
        {
            Face,
            Body,

            MaxCount
        }

        /// <summary>
        /// 플레이어 온라인 상태
        /// </summary>
        public enum OnlineState
        {
            Offline,    // 오프라인
            Lobby,      // 로비에 있음
            Game,       // 게임중임

            MaxCount
        }
    }
}