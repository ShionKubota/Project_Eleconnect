using System;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class ChildPanel : NormalPanel
	{
		public struct ChildData
		{
			public int index;		// グループパネルで使う情報
			public int indexW;		// 外から見た配列番号
			public int indexH;
			public float initAngle; // グループの中心点からの角度の初期値
		}
		public ChildData childData;
		
		public ChildPanel (TypeId id, Vector2 pos, Vector2 groupCenterPos, int indexW, int indexH) : base(id, pos)
		{
			childData.indexH = indexH;
			childData.indexW = indexW;
			childData.initAngle = FMath.Atan2 (pos.Y - groupCenterPos.Y, pos.X - groupCenterPos.X);
			childData.index = 0;
			sp.color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
		}
		/*
		public ChildPanel (TypeId id, Vector2 pos, ) : base(id, pos)
		{
			childData.indexH = indexH;
			childData.indexW = indexW;
			childData.initAngle = FMath.Atan2 (pos.Y - groupCenterPos.Y, pos.X - groupCenterPos.X);
			childData.index = 0;
			sp.color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
		}
		*/
		
		public void SetPos(Vector3 pos)
		{
			sp.pos = pos;
		}
	}
}

