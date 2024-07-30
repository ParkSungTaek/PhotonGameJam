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
    public class DecoData : SheetData
    {

		public int index; // 인덱스
		
		public string _name; // 이름
		
		public SystemEnum.DecoType _type; // 부위
		
		public string _desc; // 설명
		
		public string _resource; // 리소스 이름
		

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
                        DecoData data = new DecoData();


						if (row[0] != DBNull.Value)
						{
						    data.index = Convert.ToInt32(row[0]);
						}
						
						
						if (row[1] != DBNull.Value)
						{
						    data._name = Convert.ToString(row[1]);
						}
						
						
						if (row[2] != DBNull.Value)
						{
						    data._type = (SystemEnum.DecoType)Enum.Parse(typeof(SystemEnum.DecoType), row[2].ToString());
						}
						
						
						if (row[3] != DBNull.Value)
						{
						    data._desc = Convert.ToString(row[3]);
						}
						
						
						if (row[4] != DBNull.Value)
						{
						    data._resource = Convert.ToString(row[4]);
						}
						
						

                        dataList[data.index] = data;
                    }
                }
            }

            return dataList;
        }
    }
}