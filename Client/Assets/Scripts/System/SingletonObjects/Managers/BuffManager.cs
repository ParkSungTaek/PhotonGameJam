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

        public void SetScroll(Player player, ScrollName scrollName)
        {

            //NetworkManager.Instance.NetworkHandler._runner.SendCustomNetworkEvent(NetworkEventTarget.All, (byte)EventCodes.ButtonClicked);
        }

        //하나의 마법서 선택 마지막 선택으로 자동갱신
        public void ChooseMagicBook(string MagicBookName, Player player = null)
        {

            //NetworkManager.Instance.NetworkHandler._runner.SendCustomNetworkEvent(NetworkEventTarget.All, (byte)EventCodes.ButtonClicked);
        }

        // 선택한 마법서 있음
        public bool IsChooseOne()
        {
            return true;
        }

        //선택한 마법서 적용
        public void SelectMagicBook()
        {

            //NetworkManager.Instance.NetworkHandler._runner.SendCustomNetworkEvent(NetworkEventTarget.All, (byte)EventCodes.ButtonClicked);
        }

    }
}