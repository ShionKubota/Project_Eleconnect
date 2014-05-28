using System;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class ChildPanel : NormalPanel
	{
		protected struct ChildData
		{
			public int index;		// グループパネルで使う情報
			public int indexW;		// 外から見た配列番号
			public int indexH;
		}
		protected ChildData childData;
		
		public ChildPanel (TypeId id, Vector2 pos, int indexW, int indexH) : base(id, pos)
		{
			childData.indexH = indexH;
			childData.indexW = indexW;
			childData.index = 0;
		}
	}
}

