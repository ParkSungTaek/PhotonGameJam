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

        //�ϳ��� ������ ���� ������ �������� �ڵ�����
        public void ChooseMagicBook(string MagicBookName, Player player = null)
        {

            //NetworkManager.Instance.NetworkHandler._runner.SendCustomNetworkEvent(NetworkEventTarget.All, (byte)EventCodes.ButtonClicked);
        }

        // ������ ������ ����
        public bool IsChooseOne()
        {
            return true;
        }

        //������ ������ ����
        public void SelectMagicBook()
        {

            //NetworkManager.Instance.NetworkHandler._runner.SendCustomNetworkEvent(NetworkEventTarget.All, (byte)EventCodes.ButtonClicked);
        }

    }
}