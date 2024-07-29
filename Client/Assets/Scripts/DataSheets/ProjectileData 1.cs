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
    public class ProjectileData 1 : SheetData
    {

		public int index; // 인덱스
		
		public SystemEnum.ProjectileName _ProjectileName; // 투사체 명
		
		public int _projectileSpd; // 투사체 속도
		
		public int _lifeTime; // 생존시간 (10000 = 1초)
		

        public override Dictionary<int, SheetData> LoadData()
        {
            var dataList = new Dictionary<int, SheetData>();

            string filePath = $"Assets/Data/XLSXS/{this.GetType().Name}.xlsx";
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    DataTable table = result.Tables[0];

                    for (int rowIndex = 3; rowIndex <= table.Rows.Count - 1; rowIndex++)
                    {
                        DataRow row = table.Rows[rowIndex];
                        ProjectileData 1 data = new ProjectileData 1();


						if (row[0] != DBNull.Value)
						{
						    data.index = Convert.ToInt32(row[0]);
						}
						
						
						if (row[1] != DBNull.Value)
						{
						    data._ProjectileName = (SystemEnum.ProjectileName)Enum.Parse(typeof(SystemEnum.ProjectileName), row[1].ToString());
						}
						
						
						if (row[2] != DBNull.Value)
						{
						    data._projectileSpd = Convert.ToInt32(row[2]);
						}
						
						
						if (row[3] != DBNull.Value)
						{
						    data._lifeTime = Convert.ToInt32(row[3]);
						}
						
						

                        dataList[data.index] = data;
                    }
                }
            }

            return dataList;
        }
    }
}