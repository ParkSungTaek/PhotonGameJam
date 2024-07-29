using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    /// <summary>
    /// ���� ���з� ���� Raw��
    /// </summary>
    public class PlayerData : SheetData
    {
        // ���� �׼��� �ʵ� (���� ���̺� ���ʷ����Ͱ� ��� �ϴ� �������)
        private ValidityType _player = ValidityType.Normal; // �÷��̾� Ÿ��

        private int _movSpd    = 30000;   // �÷��̾� �ӵ�
        private int _jumpPower = 50000;   // �÷��̾� ���� ��
        private int _hp        = 1000000; // HP


        // �ܺ� �׼��� �ʵ�
        public ValidityType Player => _player; // �÷��̾� Ÿ��

        public int Speed => _movSpd;        // �÷��̾� �ӵ�
        public int JumpPower => _jumpPower; // �÷��̾� ���� ��

        public int HP => _hp;   // HP

        public override Dictionary<int, SheetData> LoadData()
        {
            return new Dictionary<int, SheetData>();
        }
    }
}