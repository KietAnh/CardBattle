/**
 * Auto generated, do not edit it
 *
 * t_level_guide
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;

namespace Data.Containers
{
	public class t_level_guideContainer : BaseContainer
	{
		private List<t_level_guideBean> list = new List<t_level_guideBean>();
		private Dictionary<int, t_level_guideBean> map = new Dictionary<int, t_level_guideBean>();

		//public override List<t_level_guideBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_level_guideBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_level_guideBean);

		public override void loadDataFromBin()
		{    
			map.Clear();
			list.Clear();
			Loaded = true;
			
			byte[] data = null;
			if(ConfigBean.IsServer)
				data = getServerData();
			else
				data = getClientData();
			
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_level_guideBean bean = new t_level_guideBean();
						if(ConfigBean.IsServer)
							bean.LoadDataServer(data, ref offset);
						else
							bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Logging.Err("Exist duplicate Key: " + bean.t_id + " t_level_guideBean");
					}
				}
				catch (Exception ex)
				{
					Logging.Err("import data error: t_level_guideBean >>" + ex.ToString());
				}
			}
			else
			{
				Logging.Err("can not find conf data: t_level_guideBean.bytes");
			}
		}
		
		private byte[] getClientData()
		{
            byte[] data = ConfigManager.Singleton.GetData("t_level_guideBean");
			if(GameManager.GetMainFlag() < 14)
				PathUtil.Decode(data);
			return data;
		}
		
		private byte[] getServerData()
		{
			byte[] data = File.ReadAllBytes(System.Environment.CurrentDirectory + "/bean/t_level_guideBean.bytes");
			return data;
		}
	}
}


