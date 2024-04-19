/**
 * Auto generated, do not edit it
 *
 * t_global_constant
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;

namespace Data.Containers
{
	public class t_global_constantContainer : BaseContainer
	{
		private List<t_global_constantBean> list = new List<t_global_constantBean>();
		private Dictionary<String, t_global_constantBean> map = new Dictionary<String, t_global_constantBean>();

		//public override List<t_global_constantBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<String, t_global_constantBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_global_constantBean);

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
						t_global_constantBean bean = new t_global_constantBean();
						if(ConfigBean.IsServer)
							bean.LoadDataServer(data, ref offset);
						else
							bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Logging.Err("Exist duplicate Key: " + bean.t_id + " t_global_constantBean");
					}
				}
				catch (Exception ex)
				{
					Logging.Err("import data error: t_global_constantBean >>" + ex.ToString());
				}
			}
			else
			{
				Logging.Err("can not find conf data: t_global_constantBean.bytes");
			}
		}
		
		private byte[] getClientData()
		{
            byte[] data = ConfigManager.Singleton.GetData("t_global_constantBean");
			if(GameManager.GetMainFlag() < 14)
				PathUtil.Decode(data);
			return data;
		}
		
		private byte[] getServerData()
		{
			byte[] data = File.ReadAllBytes(System.Environment.CurrentDirectory + "/bean/t_global_constantBean.bytes");
			return data;
		}
	}
}


