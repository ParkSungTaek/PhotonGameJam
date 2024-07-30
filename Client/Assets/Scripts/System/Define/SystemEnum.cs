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
            CharPlayer,
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
            NHP,     // 현재 HP
            NMHP,    // 현재 최대 HP
            MovSpd,  // 기본 Speed
            NMovSpd, // 현재 Speed
            Att,     // 기본 공격력
            NAtt,    // 현재 공격력
            Def,     // 기본 방어력
            NDef,    // 현재 방어력
            AtkSpd,  // 기본 공격속도
            NAtkSpd, // 현재 공격속도
            JumpP,   // 기본 점프 힘
            NJumpP,  // 현재 점프 힘

            MaxCount
        }

        /// 꾸미기 아이템 타입(부위)
        public enum DecoType
        {
            None,
            Face,
            Body,

            MaxCount
        }
    }
}