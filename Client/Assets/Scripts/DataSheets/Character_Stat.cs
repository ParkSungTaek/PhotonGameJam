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
    public partial class Character_Stat : SheetData
    {

		public int index; // 인덱스
		
		public string Name; // 플레이어명
		
		public int _HP; // 체력
		
		public int _Attack; // 공격력
		
		public int _Def; // 방어력
		
		public int _Speed; // 이동속도
		
		public int _AttackSpeed; // 공격속도
		
		public int _Reload; // 장전속도
		
		public int _Shield; // 쉴드 
		
		public int _ShieldCooltime_sec; // 쉴드 쿨타임
		

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
                
                Character_Stat data = new Character_Stat();

                data.index = Convert.ToInt32(values[0]);
				data.Name = Convert.ToString(values[1]);
				data._HP = Convert.ToInt32(values[2]);
				data._Attack = Convert.ToInt32(values[3]);
				data._Def = Convert.ToInt32(values[4]);
				data._Speed = Convert.ToInt32(values[5]);
				data._AttackSpeed = Convert.ToInt32(values[6]);
				data._Reload = Convert.ToInt32(values[7]);
				data._Shield = Convert.ToInt32(values[8]);
				data._ShieldCooltime_sec = Convert.ToInt32(values[9]);
				

                dataList[data.index] = data;
            }

            return dataList;
        }
    }
}