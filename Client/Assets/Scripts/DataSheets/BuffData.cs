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
    public partial class BuffData : SheetData
    {

		public int index; // 인덱스
		
		public int _GroupType ; // 버프 그룹 (같으면 중첩불가)
		
		public int _GroupOrder; // 우선순위(같은 그룹 내 우선순위)
		
		public SystemEnum.BuffName _BuffName; // 버프 타입
		
		public SystemEnum.BuffType _BuffType; // 버프 타입
		
		public SystemEnum.EntityStat Stat; // 스텟
		
		public int Value1; // Value1
		
		public int Value2; // Value2
		
		public int Value3; // Value3
		
		public int Value4; // Value4
		
		public int Time; // Time
		

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

                string[] values = lines[i].Trim().Split(',');
                
                BuffData data = new BuffData();

                data.index = Convert.ToInt32(values[0]);
				data._GroupType  = Convert.ToInt32(values[2]);
				data._GroupOrder = Convert.ToInt32(values[3]);
				data._BuffName = (SystemEnum.BuffName)Enum.Parse(typeof(SystemEnum.BuffName), values[4]);
				data._BuffType = (SystemEnum.BuffType)Enum.Parse(typeof(SystemEnum.BuffType), values[5]);
				data.Stat = (SystemEnum.EntityStat)Enum.Parse(typeof(SystemEnum.EntityStat), values[6]);
				data.Value1 = Convert.ToInt32(values[7]);
				data.Value2 = Convert.ToInt32(values[8]);
				data.Value3 = Convert.ToInt32(values[9]);
				data.Value4 = Convert.ToInt32(values[10]);
				data.Time = Convert.ToInt32(values[11]);
				

                dataList[data.index] = data;
            }

            return dataList;
        }
    }
}