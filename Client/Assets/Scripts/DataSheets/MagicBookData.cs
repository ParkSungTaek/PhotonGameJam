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
    public partial class MagicBookData : SheetData
    {

		public int index; // 인덱스
		
		public string name; // 마법서 명
		
		public SystemEnum.MagicElement element; // 속성
		
		public bool isActive; // 타입
		
		public string desc; // 효과 설명
		
		public string iconResource; // 아이콘 리소스
		

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
                
                MagicBookData data = new MagicBookData();

                data.index = Convert.ToInt32(values[0]);
				data.name = Convert.ToString(values[1]);
				data.element = (SystemEnum.MagicElement)Enum.Parse(typeof(SystemEnum.MagicElement), values[2]);
				data.isActive = Convert.ToBoolean(values[3]);
				data.desc = Convert.ToString(values[4]);
				data.iconResource = Convert.ToString(values[5]);
				

                dataList[data.index] = data;
            }

            return dataList;
        }
    }
}