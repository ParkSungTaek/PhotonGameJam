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
    public class TestData2 : SheetData
    {

		public int index; // 인덱스
		
		public string name; // 이름
		
		public int attack; // 공격력
		
		public int defence; // 방어력
		
		public int speed; // 이동속도
		
		public SystemEnum.BuffType buff; // 버프
		

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
                
                TestData2 data = new TestData2();

                data.index = Convert.ToInt32(values[0]);
				data.name = Convert.ToString(values[1]);
				data.attack = Convert.ToInt32(values[2]);
				data.defence = Convert.ToInt32(values[3]);
				data.speed = Convert.ToInt32(values[4]);
				data.buff = (SystemEnum.BuffType)Enum.Parse(typeof(SystemEnum.BuffType), values[5]);
				

                dataList[data.index] = data;
            }

            return dataList;
        }
    }
}