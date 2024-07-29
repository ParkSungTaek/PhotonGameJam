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
                        EntityPlayerData data = new EntityPlayerData();


						if (row[0] != DBNull.Value)
						{
						    data.index = Convert.ToInt32(row[0]);
						}
						
						
						if (row[1] != DBNull.Value)
						{
						    data._PlayerCharName = (SystemEnum.PlayerCharName)Enum.Parse(typeof(SystemEnum.PlayerCharName), row[1].ToString());
						}
						
						
						if (row[2] != DBNull.Value)
						{
						    data._movSpd = Convert.ToInt32(row[2]);
						}
						
						
						if (row[3] != DBNull.Value)
						{
						    data._jumpPower = Convert.ToInt32(row[3]);
						}
						
						
						if (row[4] != DBNull.Value)
						{
						    data._hp = Convert.ToInt32(row[4]);
						}
						
						

                        dataList[data.index] = data;
                    }
                }
            }

            return dataList;
        }
    }
}