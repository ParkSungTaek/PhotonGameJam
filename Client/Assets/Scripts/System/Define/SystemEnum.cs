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
        public enum State
        {
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
    }
}