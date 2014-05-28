using System;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class NormalPanel : Panel
	{
		public NormalPanel(TypeId id, Vector2 pos) : base(id, pos)
		{
		}
		
		// ボタン押された際のイベント
		public override void ButtonEvent(bool pushR)
		{
			Rotate (pushR);
		}
	}
}

