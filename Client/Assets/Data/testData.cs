

using System.Collections.Generic;

namespace Client
{
	public class testData : SheetData
	{
		int ID; // ���̵�
		string name; // �̸�

        public override Dictionary<int, SheetData> LoadData()
        {
            return new Dictionary<int, SheetData>();
        }
    }
}