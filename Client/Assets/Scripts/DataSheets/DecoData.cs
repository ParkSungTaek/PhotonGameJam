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
    public partial class DecoData : SheetData
    {

		public int index; // 인덱스
		
		public string _name; // 이름
		
		public SystemEnum.DecoType _type; // 부위
		
		public string _desc; // 설명
		
		public string _resource; // 리소스 이름
		

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
                
                DecoData data = new DecoData();

                data.index = Convert.ToInt32(values[0]);
				data._name = Convert.ToString(values[1]);
				data._type = (SystemEnum.DecoType)Enum.Parse(typeof(SystemEnum.DecoType), values[2]);
				data._desc = Convert.ToString(values[3]);
				data._resource = Convert.ToString(values[4]);
				

                dataList[data.index] = data;
            }

            return dataList;
        }
    }
}