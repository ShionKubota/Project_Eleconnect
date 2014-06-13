using System;
using System.Collections.Generic;

namespace Eleconnect
{
	public class StageDataList
	{
		List<Stage_Base> data;
		public StageDataList ()
		{
			data = new List<Stage_Base>();
			data.Add (new Stage_0());
			data.Add (new Stage_1());
			data.Add (new Stage_2());
			data.Add (new Stage_3());
		}
		
		public Stage_Base this[int index]
		{
			get
			{
				return data[index];
			}
		}
	}
}

