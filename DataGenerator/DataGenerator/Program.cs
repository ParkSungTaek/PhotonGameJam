using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

// <패킷 제네레이터 6#> 22.03.04 - 클라 / 서버 패킷 레지스터 분리
namespace PacketGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string folderPath = "../../Client/Assets/Data/XLSXS";
            string[] excelFiles = Directory.GetFiles(folderPath, "*.xlsx");

            foreach (string file in excelFiles)
            {
                Console.WriteLine("Generate file: " + Path.GetFileName(file));
                ParseExcel(file);
            }
        }

        public static void ParseExcel(string file)
        {
            string excelName = Path.GetFileNameWithoutExtension(Path.GetFileName(file));
            string dataRegister = string.Empty;
            string dataParse = string.Empty;


            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    DataTable table = result.Tables[0];


                    for (int columnIndex = 0; columnIndex <= table.Columns.Count - 1; columnIndex++)
                    {
                        var dataDescRow = table.Rows[0][columnIndex];
                        var dataNameRow = table.Rows[1][columnIndex];
                        var dataTypeRow = table.Rows[2][columnIndex];

                        string dataDesc;
                        string dataName;
                        string dataType;

                        DataColumn column = table.Columns[columnIndex];

                        if (dataNameRow == DBNull.Value)
                        {
                            continue;
                        }

                        if (dataTypeRow == DBNull.Value)
                        {
                            continue;
                        }

                        dataDesc = dataDescRow.ToString();
                        dataName = dataNameRow.ToString();
                        dataType = dataTypeRow.ToString();

                        dataRegister += string.Format(PacketFormat.dataRegisterFormat, dataType, dataName, dataDesc) + Environment.NewLine;
                        dataParse += string.Format(PacketFormat.dataParseFomat, columnIndex.ToString(), dataName, ToMemberType2(dataType)) + Environment.NewLine;
                    }
                }
            }


            dataRegister = dataRegister.Replace("\n", "\n\t");
            dataParse = dataParse.Replace("\n", "\n\t\t\t\t\t");
            var dataManagerText = string.Format(PacketFormat.dataFormat, excelName, dataRegister, dataParse);

            File.WriteAllText($"../../Client/Assets/Scripts/DataSheets/{excelName}.cs", dataManagerText);
        }

        public static string ToMemberType2(string memberType)
        {
            switch (memberType)
            {
                case "bool":
                    return "ToBoolean";
                // case "byte": // 따로 정의되어있지는 않음
                case "short":
                    return "ToInt16";
                case "ushort":
                    return "ToUInt16";
                case "int":
                    return "ToInt32";
                case "long":
                    return "ToInt64";
                case "float":
                    return "ToSingle";
                case "double":
                    return "ToDouble";
                case "string":
                    return "ToString";
                default:
                    return string.Empty;
            }
        }
    }
}