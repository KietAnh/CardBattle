/**
 * Auto generated, do not edit it
 *
 * t_guide
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_guideBean : BaseBin
	{
		
		public int t_id; //Guide id
		public int t_next_guide; //Next guide id
		public int t_next_guide_skip; //Next guide id if skip
		public string t_trigger; //Điều kiện trigger
		public string t_objects; //Object hightlight
		public string t_main_object; //Object hand trỏ vào
		public int t_delay; //Time delay show
		public int t_text_dialogue; //Text dialogue
		
		public void LoadData(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadInt(data, ref offset);
			t_next_guide = XBuffer.ReadInt(data, ref offset);
			t_next_guide_skip = XBuffer.ReadInt(data, ref offset);
			t_trigger = XBuffer.ReadString(data, ref offset); 
			t_objects = XBuffer.ReadString(data, ref offset); 
			t_main_object = XBuffer.ReadString(data, ref offset); 
			t_delay = XBuffer.ReadInt(data, ref offset);
			t_text_dialogue = XBuffer.ReadInt(data, ref offset);
		}
		public void LoadDataServer(byte[] data, ref int offset)
		{
			t_id = XBuffer.ReadInt(data, ref offset);
			t_next_guide = XBuffer.ReadInt(data, ref offset);
			t_next_guide_skip = XBuffer.ReadInt(data, ref offset);
			t_trigger = XBuffer.ReadString(data, ref offset); 
			t_objects = XBuffer.ReadString(data, ref offset); 
			t_main_object = XBuffer.ReadString(data, ref offset); 
			t_delay = XBuffer.ReadInt(data, ref offset);
			t_text_dialogue = XBuffer.ReadInt(data, ref offset);
		}
	}
}


