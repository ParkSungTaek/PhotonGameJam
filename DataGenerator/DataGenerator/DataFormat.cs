using System;
using System.Collections.Generic;
using System.Text;

namespace DataGenerator
{
    class DataFormat
    {
        // {0} 자료형
        // {1} 변수명
        // {2} 설명
        public static string dataRegisterFormat =
@"
public {0} {1}; // {2}";

        // {0} : row index
        // {1} : 자료형 이름
        // {2} : 자료형 변환
        public static string dataParseFomat =
@"
if (row[{0}] != DBNull.Value)
{{
    data.{1} = Convert.{2}(row[{0}]);
}}
";
        public static string dataEnumRegisterFormat =
@"
public SystemEnum.{0} {1}; // {2}";

        // {0} : row index
        // {1} : 자료형 이름
        // {2} : Enum 자료형
        public static string dataEnumParseFomat =
@"
if (row[{0}] != DBNull.Value)
{{
    data.{1} = (SystemEnum.{2})Enum.Parse(typeof(SystemEnum.{2}), row[{0}].ToString());
}}
";

        // {0} : 엑셀 이름 (ex: TestData)
        // {1} : 자료형들 (ex: int a)
        // {2} : 파싱
        public static string dataFormat =
@"using Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ExcelDataReader;
using System.Data;

namespace Client
{{
    public class {0} : SheetData
    {{
{1}

        public override Dictionary<int, SheetData> LoadData()
        {{
            var dataList = new Dictionary<int, SheetData>();

            string filePath = $""Assets/Data/XLSXS/{{this.GetType().Name}}.xlsx"";
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {{
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {{
                    var result = reader.AsDataSet();
                    DataTable table = result.Tables[0];

                    for (int rowIndex = 3; rowIndex <= table.Rows.Count - 1; rowIndex++)
                    {{
                        DataRow row = table.Rows[rowIndex];
                        {0} data = new {0}();

{2}

                        dataList[data.index] = data;
                    }}
                }}
            }}

            return dataList;
        }}
    }}
}}";
    }
}