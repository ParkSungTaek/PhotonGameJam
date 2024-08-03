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
    public partial class ProjectileData : SheetData
    {

		public int index; // 인덱스
		
		public SystemEnum.ProjectileName _ProjectileName; // 투사체 명
		
		public int _projectileSpd; // 투사체 속도
		
		public int _lifeTime; // 생존시간 (10000 = 1초)
		

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
                
                ProjectileData data = new ProjectileData();

                data.index = Convert.ToInt32(values[0]);
				data._ProjectileName = (SystemEnum.ProjectileName)Enum.Parse(typeof(SystemEnum.ProjectileName), values[1]);
				data._projectileSpd = Convert.ToInt32(values[2]);
				data._lifeTime = Convert.ToInt32(values[3]);
				

                dataList[data.index] = data;
            }

            return dataList;
        }
    }
}