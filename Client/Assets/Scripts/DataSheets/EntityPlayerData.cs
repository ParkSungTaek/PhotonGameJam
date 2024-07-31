using Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ExcelDataReader;
using System.Data;

namespace Client
{
    public class EntityPlayerData : SheetData
    {

		public int index; // 인덱스
		
		public SystemEnum.PlayerCharName _PlayerCharName; // 플레이어 캐릭터명
		
		public int _movSpd; // 이름
		
		public int _jumpPower; // 플레이어 점프 힘
		
		public int _hp; // HP
		

        public override Dictionary<int, SheetData> LoadData()
        {
            var dataList = new Dictionary<int, SheetData>();

            TextAsset csvFile = Resources.Load<TextAsset>($"CSV/{this.GetType().Name}");
            string csvContent = csvFile.text;
            string[] lines = csvContent.Split('\n');
            for (int i = 3; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] values = lines[i].Split(',');
                
                EntityPlayerData data = new EntityPlayerData();

                data.index = Convert.ToInt32(values[0]);
				data._PlayerCharName = (SystemEnum.PlayerCharName)Enum.Parse(typeof(SystemEnum.PlayerCharName), values[1]);
				data._movSpd = Convert.ToInt32(values[2]);
				data._jumpPower = Convert.ToInt32(values[3]);
				data._hp = Convert.ToInt32(values[4]);
				

                dataList[data.index] = data;
            }

            return dataList;
        }
    }
}