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
    public partial class WeaponData : SheetData
    {

		public int index; // 인덱스
		
		public SystemEnum.WeaponName _WeaponName; // 공격력
		
		public int _atkSpd; // 공격속도
		
		public int _Att; // 공격력
		

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
                
                WeaponData data = new WeaponData();

                data.index = Convert.ToInt32(values[0]);
				data._WeaponName = (SystemEnum.WeaponName)Enum.Parse(typeof(SystemEnum.WeaponName), values[1]);
				data._atkSpd = Convert.ToInt32(values[2]);
				data._Att = Convert.ToInt32(values[3]);
				

                dataList[data.index] = data;
            }

            return dataList;
        }
    }
}