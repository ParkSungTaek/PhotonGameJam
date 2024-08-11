// 2024/08/03 [�̼���]
// �÷��̾� ���� ( ģ�� ����Ʈ�� ���̽� ǥ�ÿ��� ��� ���� )

using UnityEngine;
using TMPro;
using static Client.SystemEnum;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Client
{
    public class PlayerInfoSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text          name         = null; // �г���
        [SerializeField] private PlayerFaceUI      playerUI     = null; // �÷��̾� ����
        [SerializeField] private SkillScrollSlot[] skills       = null; // ��ų��

        // �÷��̾� �����͸� �����մϴ�.
        public void SetData(string name, int face, int body, int hair, int weapon, int hat, int cape, int magic1, int magic2, int magic3, int magic4)
        {
            this.name.SetText(name);
            playerUI.SetPlayerDeco(DecoType.Face, DataManager.Instance.GetData<DecoData>(face));
            playerUI.SetPlayerDeco(DecoType.Body, DataManager.Instance.GetData<DecoData>(body));
            playerUI.SetPlayerDeco(DecoType.Hair, DataManager.Instance.GetData<DecoData>(hair));
            playerUI.SetPlayerDeco(DecoType.Weapon, DataManager.Instance.GetData<DecoData>(weapon));
            playerUI.SetPlayerDeco(DecoType.Hat, DataManager.Instance.GetData<DecoData>(hat));
            playerUI.SetPlayerDeco(DecoType.Cape, DataManager.Instance.GetData<DecoData>(cape));

            foreach ( var slot in skills )
            {
                slot.gameObject.SetActive(false);
            }

            if(magic1 != 0)
            {
                skills[0].gameObject.SetActive(true);
                MagicBookData data = DataManager.Instance.GetData<MagicBookData>(magic1);
                skills[0].SetData(data , null, null);
            }

            if (magic2 != 0)
            {
                skills[1].gameObject.SetActive(true);
                MagicBookData data = DataManager.Instance.GetData<MagicBookData>(magic2);
                skills[1].SetData(data, null, null);
            }

            if (magic3 != 0)
            {
                skills[2].gameObject.SetActive(true);
                MagicBookData data = DataManager.Instance.GetData<MagicBookData>(magic3);
                skills[2].SetData(data, null, null);
            }

            if (magic4 != 0)
            {
                skills[3].gameObject.SetActive(true);
                MagicBookData data = DataManager.Instance.GetData<MagicBookData>(magic4);
                skills[3].SetData(data, null, null);
            }
        }
    }
}