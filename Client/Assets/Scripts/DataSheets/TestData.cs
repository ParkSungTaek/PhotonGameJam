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
    public class TestData : SheetData
    {

		public int index; // 인덱스
		
		public int attack; // 공격력
		
		public string name; // 이름
		

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
                        TestData data = new TestData();


						if (row[0] != DBNull.Value)
						{
						    data.index = Convert.ToInt32(row[0]);
						}
						
						
						if (row[1] != DBNull.Value)
						{
						    data.attack = Convert.ToInt32(row[1]);
						}
						
						
						if (row[2] != DBNull.Value)
						{
						    data.name = Convert.ToString(row[2]);
						}
						
						

                        dataList[data.index] = data;
                    }
                }
            }

            return dataList;
        }
    }
}