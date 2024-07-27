using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class WeaponData : SheetData
    {

        // ���� �׼��� �ʵ� (���� ���̺� ���ʷ����Ͱ� ��� �ϴ� �������)
        private WeaponType _player = WeaponType.Normal; // �÷��̾� Ÿ��

        private int _atkSpd = 30000;   // ���� �ӵ�

        // �ܺ� ������ �ʵ�
        public int AtkSpd => _atkSpd;   // ���� �ӵ�

        public override Dictionary<int, SheetData> LoadData()
        {
            return new Dictionary<int, SheetData>();
        }
    }
}
