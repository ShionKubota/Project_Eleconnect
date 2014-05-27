using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class GroupPanel : Panel
	{
		List<List<ChildPanel>> panels;
		
		public GroupPanel(RouteId id, Vector2 pos) : base(id, pos)
		{
			typeId = TypeId.Group;
		}
		
		// ボタン押された際のイベント
		public override TypeId ButtonEvent(bool pushR)
		{
			
			return typeId;
		}
		
		// グループにパネル追加
		public override void AddPanel(RouteId id, Vector2 pos, int indexW, int indexH)
		{
			//panels.Add (new ChildPanel(id, pos, indexW, indexH));
			
		}
		
		public override void Update ()
		{
			base.Update();
		}
	}
}

