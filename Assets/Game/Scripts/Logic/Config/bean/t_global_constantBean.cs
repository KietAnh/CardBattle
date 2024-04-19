/**
 * Auto generated, do not edit it
 *
 * t_global_constant
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_global_constantBean : BaseBin
	{
		
		public string t_id; //ID
		public int t_int_param; //int param
		public string t_string_param; //string param
		
		public void LoadData(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadString(data, ref offset); 
			t_int_param = XBuffer.ReadInt(data, ref offset);
			t_string_param = XBuffer.ReadString(data, ref offset); 
		}
		public void LoadDataServer(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadString(data, ref offset); 
			t_int_param = XBuffer.ReadInt(data, ref offset);
			t_string_param = XBuffer.ReadString(data, ref offset); 
		}
	}
}


