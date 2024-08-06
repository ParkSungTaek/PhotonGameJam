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

        //�ϳ��� ������ ����
        public void ChooseMagicBook(MagicBookData MagicBookName)
        {
            Debug.Log($"{MagicBookName.name} �� ���� MagicBook ����");
            magicBook = MagicBookName;
            
        }

        // ������ ������ ����
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

        //������ ������ ����
        public void SelectMagicBook()
        {

            magicBookName = null;
            //NetworkManager.Instance.NetworkHandler._runner.SendCustomNetworkEvent(NetworkEventTarget.All, (byte)EventCodes.ButtonClicked);
        }

    }
}