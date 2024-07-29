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
    public class WeaponData : SheetData
    {

		public int index; // 인덱스
		
		public SystemEnum.WeaponName _WeaponName; // 공격력
		
		public int _atkSpd; // 공격속도
		
		public int _Att; // 공격력
		

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
                        WeaponData data = new WeaponData();


						if (row[0] != DBNull.Value)
						{
						    data.index = Convert.ToInt32(row[0]);
						}
						
						
						if (row[1] != DBNull.Value)
						{
						    data._WeaponName = (SystemEnum.WeaponName)Enum.Parse(typeof(SystemEnum.WeaponName), row[1].ToString());
						}
						
						
						if (row[2] != DBNull.Value)
						{
						    data._atkSpd = Convert.ToInt32(row[2]);
						}
						
						
						if (row[3] != DBNull.Value)
						{
						    data._Att = Convert.ToInt32(row[3]);
						}
						
						

                        dataList[data.index] = data;
                    }
                }
            }

            return dataList;
        }
    }
}