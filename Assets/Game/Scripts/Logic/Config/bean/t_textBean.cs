/**
 * Auto generated, do not edit it
 *
 * t_text
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_textBean : BaseBin
	{
		
		public int t_id; //ID
		private string t_english; //nội dung tiếng anh
		private string t_vietnamese; //nội dung tiếng việt

		public string t_content  // update later
		{
			get
			{
				return t_english;
			}
		}
		
		public void LoadData(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadInt(data, ref offset);
			t_english = XBuffer.ReadString(data, ref offset); 
			t_vietnamese = XBuffer.ReadString(data, ref offset); 
		}
		public void LoadDataServer(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadInt(data, ref offset);
			t_english = XBuffer.ReadString(data, ref offset); 
			t_vietnamese = XBuffer.ReadString(data, ref offset); 
		}
	}
}


