using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class LinePanel : Panel
	{
		private List<ChildPanel> panels;
		private int baseIndex;
		private bool isSideLine;
		
		public LinePanel(TypeId id, Vector2 pos, bool isSideLine) : base(id, pos)
		{
			this.isSideLine = isSideLine;
			
		}
		
		public override void ButtonEvent(bool ispushR)
		{
			
		}
	}// END OF CLASS
}

