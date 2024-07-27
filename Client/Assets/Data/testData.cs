

using System.Collections.Generic;

namespace Client
{
	public class testData : SheetData
	{
		int ID; // 아이디
		string name; // 이름

        public override Dictionary<int, SheetData> LoadData()
        {
            return new Dictionary<int, SheetData>();
        }
    }
}