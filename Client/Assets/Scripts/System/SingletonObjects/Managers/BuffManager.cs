using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class BuffManager : Singleton<BuffManager>
    {
        private string magicBookName = SystemString.NoData;
        MagicBookData magicBook = null;
        private BuffManager()
        {

        }

        public void SetScroll(Player player, ScrollName scrollName)
        {

            //NetworkManager.Instance.NetworkHandler._runner.SendCustomNetworkEvent(NetworkEventTarget.All, (byte)EventCodes.ButtonClicked);
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

            magicBookName = null;
            //NetworkManager.Instance.NetworkHandler._runner.SendCustomNetworkEvent(NetworkEventTarget.All, (byte)EventCodes.ButtonClicked);
        }

    }
}