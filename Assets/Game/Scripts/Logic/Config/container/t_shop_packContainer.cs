/**
 * Auto generated, do not edit it
 *
 * t_shop_pack
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;

namespace Data.Containers
{
	public class t_shop_packContainer : BaseContainer
	{
		private List<t_shop_packBean> list = new List<t_shop_packBean>();
		private Dictionary<int, t_shop_packBean> map = new Dictionary<int, t_shop_packBean>();

		//public override List<t_shop_packBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_shop_packBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_shop_packBean);

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
						t_shop_packBean bean = new t_shop_packBean();
						if(ConfigBean.IsServer)
							bean.LoadDataServer(data, ref offset);
						else
							bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Logging.Err("Exist duplicate Key: " + bean.t_id + " t_shop_packBean");
					}
				}
				catch (Exception ex)
				{
					Logging.Err("import data error: t_shop_packBean >>" + ex.ToString());
				}
			}
			else
			{
				Logging.Err("can not find conf data: t_shop_packBean.bytes");
			}
		}
		
		private byte[] getClientData()
		{
            byte[] data = ConfigManager.Singleton.GetData("t_shop_packBean");
			if(GameManager.GetMainFlag() < 14)
				PathUtil.Decode(data);
			return data;
		}
		
		private byte[] getServerData()
		{
			byte[] data = File.ReadAllBytes(System.Environment.CurrentDirectory + "/bean/t_shop_packBean.bytes");
			return data;
		}
	}
}


