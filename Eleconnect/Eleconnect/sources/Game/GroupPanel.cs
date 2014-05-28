using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class GroupPanel : Panel
	{
		List<List<ChildPanel>> panels;
		
		public GroupPanel(TypeId id, Vector2 pos) : base(id, pos)
		{
		}
		
		// ボタン押された際のイベント
		public override void ButtonEvent(bool pushR)
		{
		}
		
		// グループにパネル追加
		public override void AddPanel(TypeId id, Vector2 pos, int indexW, int indexH)
		{
			
		}
		
		public override void Update ()
		{
			base.Update();
		}
	}
}

