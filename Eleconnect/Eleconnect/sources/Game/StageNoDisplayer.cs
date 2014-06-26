using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class StageNoDisplayer
	{
		private Number number;
		private Sprite2D labelSp;
		private Texture2D labelTex;
		
		public StageNoDisplayer ()
		{
			number = new Number();
			//number.numSp.
			
			labelTex = new Texture2D(@"/Application/assets/img/Stage.png", false);
			labelSp  = new Sprite2D(labelTex);
			labelSp.size = new Vector2(0.35f);
			labelSp.pos = new Vector3(96.0f,32.0f,0.0f);
		}
		
		public void Draw()
		{
			labelSp.Draw();
			//number.Draw();
		}
		
		public void Term()
		{
			labelSp.Term();
			number.Term();
		}
	}
}

