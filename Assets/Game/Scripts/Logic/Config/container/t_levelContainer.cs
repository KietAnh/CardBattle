/**
 * Auto generated, do not edit it
 *
 * t_level
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;

namespace Data.Containers
{
	public class t_levelContainer : BaseContainer
	{
		private List<t_levelBean> list = new List<t_levelBean>();
		private Dictionary<int, t_levelBean> map = new Dictionary<int, t_levelBean>();

		//public override List<t_levelBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_levelBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_levelBean);

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
						t_levelBean bean = new t_levelBean();
						if(ConfigBean.IsServer)
							bean.LoadDataServer(data, ref offset);
						else
							bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Logging.Err("Exist duplicate Key: " + bean.t_id + " t_levelBean");
					}
				}
				catch (Exception ex)
				{
					Logging.Err("import data error: t_levelBean >>" + ex.ToString());
				}
			}
			else
			{
				Logging.Err("can not find conf data: t_levelBean.bytes");
			}
		}
		
		private byte[] getClientData()
		{
            byte[] data = ConfigManager.Singleton.GetData("t_levelBean");
			if(GameManager.GetMainFlag() < 14)
				PathUtil.Decode(data);
			return data;
		}
		
		private byte[] getServerData()
		{
			byte[] data = File.ReadAllBytes(System.Environment.CurrentDirectory + "/bean/t_levelBean.bytes");
			return data;
		}
	}
}


