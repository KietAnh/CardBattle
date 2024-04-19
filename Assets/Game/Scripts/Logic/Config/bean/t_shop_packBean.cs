/**
 * Auto generated, do not edit it
 *
 * t_shop_pack
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_shop_packBean : BaseBin
	{
		
		public int t_id; //ID
		public string t_product_id; //Product ID
		public string t_rewards; //Bộ item nhận được
		public int t_price; //Giá (USD) (chia 100)
		public int t_store; //Store
		public int t_product_type; //Product Type
		
		public void LoadData(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadInt(data, ref offset);
			t_product_id = XBuffer.ReadString(data, ref offset); 
			t_rewards = XBuffer.ReadString(data, ref offset); 
			t_price = XBuffer.ReadInt(data, ref offset);
			t_store = XBuffer.ReadInt(data, ref offset);
			t_product_type = XBuffer.ReadInt(data, ref offset);
		}
		public void LoadDataServer(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadInt(data, ref offset);
			t_product_id = XBuffer.ReadString(data, ref offset); 
			t_rewards = XBuffer.ReadString(data, ref offset); 
			t_price = XBuffer.ReadInt(data, ref offset);
			t_store = XBuffer.ReadInt(data, ref offset);
			t_product_type = XBuffer.ReadInt(data, ref offset);
		}
	}
}


