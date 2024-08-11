using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class BuffManager : Singleton<BuffManager>
    {
        private BuffManager()
        {

        }

        public BuffBase SetBuff(int i)
        {
            BuffBase buffBase = null;
            BuffData buffData = DataManager.Instance.GetData<BuffData>(i);

            buffBase = BuffFactory.InstanceBuff(buffData);

            return buffBase;
        }
        public void SetBuffToPlayer(int buffIndex, PlayerRef buffTarget, PlayerRef buffUser, Player targetPlayer)
        {
            if (buffIndex == 0)
                return;

            if (targetPlayer != null)
            {
                targetPlayer.RPC_SetBuff(buffIndex, buffTarget, buffUser);
            }
        }

        //선택한 마법서 적용
        public void SelectMagicBook(MagicBookData magicData, Player player)
        {
            if (magicData != null)
            {
                PlayerRef playerRef = default;
                if (player != null)
                {
                    playerRef = player.Object.InputAuthority;
                }
                player.RPC_SetMagicElement(magicData.index);
                if (magicData.Value1 != 0)
                {
                    SetBuffToPlayer(magicData.Value1, playerRef, playerRef, player);
                }
                if (magicData.Value2 != 0)
                {
                    SetBuffToPlayer(magicData.Value2, playerRef, playerRef, player);
                }
                if (magicData.Value3 != 0)
                {
                    SetBuffToPlayer(magicData.Value3, playerRef, playerRef, player);
                }
                if (magicData.Value4 != 0)
                {
                    SetBuffToPlayer(magicData.Value4, playerRef, playerRef, player);
                }
            }
            //NetworkManager.Instance.NetworkHandler._runner.SendCustomNetworkEvent(NetworkEventTarget.All, (byte)EventCodes.ButtonClicked);
        }

    }
}