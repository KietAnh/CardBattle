/**
 * Auto generated, do not edit it
 *
 * t_item
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_itemBean : BaseBin
	{
		
		public int t_id; //ID
		public string t_pref_key; //Pref Key
		public int t_max_value; //Số lượng tối đa
		public string t_object; //Tên object để load lên
		public int t_show_num; //Có show số lượng ko?
		
		public void LoadData(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadInt(data, ref offset);
			t_pref_key = XBuffer.ReadString(data, ref offset); 
			t_max_value = XBuffer.ReadInt(data, ref offset);
			t_object = XBuffer.ReadString(data, ref offset); 
			t_show_num = XBuffer.ReadInt(data, ref offset);
		}
		public void LoadDataServer(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadInt(data, ref offset);
			t_pref_key = XBuffer.ReadString(data, ref offset); 
			t_max_value = XBuffer.ReadInt(data, ref offset);
			t_object = XBuffer.ReadString(data, ref offset); 
			t_show_num = XBuffer.ReadInt(data, ref offset);
		}
	}
}


