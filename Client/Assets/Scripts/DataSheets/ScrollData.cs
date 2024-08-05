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
    public partial class ScrollData : SheetData
    {

		public int index; // 인덱스
		
		public SystemEnum.ScrollName Scroll; // 스크롤Enum
		
		public string ScrollNameInGame; // 스크롤명(인게임 확인용)
		
		public SystemEnum.ElementType Element; // 속성 타입
		
		public int Value1; // Value1
		
		public int Value2; // Value2
		
		public int Value3; // Value3
		
		public int Value4; // Value4
		

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
                
                ScrollData data = new ScrollData();

                data.index = Convert.ToInt32(values[0]);
				data.Scroll = (SystemEnum.ScrollName)Enum.Parse(typeof(SystemEnum.ScrollName), values[1]);
				data.ScrollNameInGame = Convert.ToString(values[2]);
				data.Element = (SystemEnum.ElementType)Enum.Parse(typeof(SystemEnum.ElementType), values[3]);
				data.Value1 = Convert.ToInt32(values[4]);
				data.Value2 = Convert.ToInt32(values[5]);
				data.Value3 = Convert.ToInt32(values[6]);
				data.Value4 = Convert.ToInt32(values[7]);
				

                dataList[data.index] = data;
            }

            return dataList;
        }
    }
}