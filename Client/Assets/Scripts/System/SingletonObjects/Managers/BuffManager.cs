using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class BuffManager : Singleton<BuffManager>
    {
        MagicBookData magicBook = null;
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

            buffTarget.PlayerInfo.ExecuteBuff(buffBase);
        }

        //하나의 마법서 선택
        public void ChooseMagicBook(MagicBookData MagicBookName)
        {
            Debug.Log($"{MagicBookName.name} 로 선택 MagicBook 갱신");
            magicBook = MagicBookName;
            
        }

        // 선택한 마법서 있음
        public bool IsChooseOne()
        {
            if (magicBook != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //선택한 마법서 적용
        public void SelectMagicBook()
        {
            if (magicBook != null)
            {
                if (magicBook.Value1 != 0)
                {
                    SetBuffToPlayer(magicBook.Value1);
                }
                if (magicBook.Value2 != 0)
                {
                    SetBuffToPlayer(magicBook.Value2);
                }
                if (magicBook.Value3 != 0)
                {
                    SetBuffToPlayer(magicBook.Value3);
                }
                if (magicBook.Value4 != 0)
                {
                    SetBuffToPlayer(magicBook.Value4);
                }
            }
        }

    }
}