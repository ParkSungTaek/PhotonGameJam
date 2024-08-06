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

        private BuffBase SetBuff(int i)
        {
            BuffBase buffBase = null;
            BuffData buffData = DataManager.Instance.GetData<BuffData>(i);

            buffBase = BuffFactory.InstanceBuff(buffData);

            return buffBase;
        }
        public void SetBuffToPlayer(int buffIndex, Player buffTarget = null, Player buffUser = null)
        {
            BuffBase buffBase = SetBuff(buffIndex);
            if (buffTarget != null)
            {
                buffBase.SetBuffUser(buffUser);
                buffBase.SetBuffTarget(buffTarget);
            }
            else
            {
                buffBase.SetBuffUser(EntityManager.Instance.MyPlayer);
                buffBase.SetBuffTarget(EntityManager.Instance.MyPlayer);
            }

            if(buffTarget != null)
                buffTarget.PlayerInfo.ExecuteBuff(buffBase);
        }

        //선택한 마법서 적용
        public void SelectMagicBook(MagicBookData magicData)
        {
            if (magicData != null)
            {
                if (magicData.Value1 != 0)
                {
                    SetBuffToPlayer(magicData.Value1);
                }
                if (magicData.Value2 != 0)
                {
                    SetBuffToPlayer(magicData.Value2);
                }
                if (magicData.Value3 != 0)
                {
                    SetBuffToPlayer(magicData.Value3);
                }
                if (magicData.Value4 != 0)
                {
                    SetBuffToPlayer(magicData.Value4);
                }
            }
            //NetworkManager.Instance.NetworkHandler._runner.SendCustomNetworkEvent(NetworkEventTarget.All, (byte)EventCodes.ButtonClicked);
        }

    }
}