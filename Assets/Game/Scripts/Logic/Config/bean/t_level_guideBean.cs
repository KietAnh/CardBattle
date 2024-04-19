/**
 * Auto generated, do not edit it
 *
 * t_level_guide
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_level_guideBean : BaseBin
	{
		
		public int t_id; //Level
		public int t_fruit_count; //Fruit Count
		public string t_layer1; //Layer 1
		public string t_layer2; //Layer 2
		public string t_layer3; //Layer 3
		public string t_layer4; //Layer 4
		public string t_layer1_type; //Layer 1 Type
		public string t_layer2_type; //Layer 2 Type
		public string t_layer3_type; //Layer 3 Type
		public string t_layer4_type; //Layer 4 Type
		public int t_fruit_type_count; //Số loại fruit
		public string t_count_by_type; //Fruit Count by type
		
		public void LoadData(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadInt(data, ref offset);
			t_fruit_count = XBuffer.ReadInt(data, ref offset);
			t_layer1 = XBuffer.ReadString(data, ref offset); 
			t_layer2 = XBuffer.ReadString(data, ref offset); 
			t_layer3 = XBuffer.ReadString(data, ref offset); 
			t_layer4 = XBuffer.ReadString(data, ref offset); 
			t_layer1_type = XBuffer.ReadString(data, ref offset); 
			t_layer2_type = XBuffer.ReadString(data, ref offset); 
			t_layer3_type = XBuffer.ReadString(data, ref offset); 
			t_layer4_type = XBuffer.ReadString(data, ref offset); 
			t_fruit_type_count = XBuffer.ReadInt(data, ref offset);
			t_count_by_type = XBuffer.ReadString(data, ref offset); 
		}
		public void LoadDataServer(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadInt(data, ref offset);
			t_fruit_count = XBuffer.ReadInt(data, ref offset);
			t_layer1 = XBuffer.ReadString(data, ref offset); 
			t_layer2 = XBuffer.ReadString(data, ref offset); 
			t_layer3 = XBuffer.ReadString(data, ref offset); 
			t_layer4 = XBuffer.ReadString(data, ref offset); 
			t_layer1_type = XBuffer.ReadString(data, ref offset); 
			t_layer2_type = XBuffer.ReadString(data, ref offset); 
			t_layer3_type = XBuffer.ReadString(data, ref offset); 
			t_layer4_type = XBuffer.ReadString(data, ref offset); 
			t_fruit_type_count = XBuffer.ReadInt(data, ref offset);
			t_count_by_type = XBuffer.ReadString(data, ref offset); 
		}
	}
}


