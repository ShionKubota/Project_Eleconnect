using System;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class NormalPanel : Panel
	{
		public NormalPanel(RouteId id, Vector2 pos) : base(id, pos)
		{
			typeId = TypeId.Normal;
		}
		
		// ボタン押された際のイベント
		public override TypeId ButtonEvent(bool pushR)
		{
			Rotate (pushR);
			return typeId;
		}
	}
}

