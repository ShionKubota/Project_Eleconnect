using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class NormalPanel : Panel
	{
		public NormalPanel(TypeId id, Vector2 pos) : base(id, pos)
		{
		}
		
		public override void Init(TypeId id, Vector2 pos)
		{
			switch(id)
			{
			case TypeId.Straight:
				//sp.textureUV = new Vector4(0.0f, 0.0f, 0.5f, 0.5f);
				route[DirId.UP] = route[DirId.DOWN] = true;
				route[DirId.LEFT] = route[DirId.RIGHT] = false;
				break;
				
			case TypeId.RightAngle:
				route[DirId.RIGHT] = route[DirId.DOWN] = true;
				route[DirId.UP] = route[DirId.LEFT] =  false;
				break;
				
			case TypeId.T:
				route[DirId.RIGHT] = route[DirId.LEFT] = route[DirId.DOWN] = true;
				route[DirId.UP] = false;
				break;
				
			case TypeId.Cross:
				route[DirId.RIGHT] = route[DirId.LEFT] = route[DirId.UP] = route[DirId.DOWN] = true;
				break;
			}
			
			for(int i = 0; i < 4; i++)
			{
				if(tex[i] == null)
					tex[i] = new Texture2D(@"/Application/assets/img/paneru" + i + ".png", false);
			}
			
			sp =  new Sprite2D(tex[(int)id]);
			sp.pos = new Vector3(pos.X, pos.Y, 0.0f);
			sp.textureUV = new Vector4(0.0f, 0.0f, 0.5f, 1.0f);
			sp.size = new Vector2(SCALE / 2.0f, SCALE);
			sp.color = new Vector4(0.3f, 0.3f, 0.3f, 1.0f);
			
			lightSp = new Sprite2D(tex[(int)id]);
			lightSp.pos = sp.pos;
			lightSp.textureUV = new Vector4(0.5f, 0.0f, 1.0f, 1.0f);
			lightSp.size = sp.size;
			
			repeaterTex = new Texture2D(@"/Application/assets/img/ianzuma.png", false);
			repeaterSp =  new Sprite2D(repeaterTex);
			repeaterSp.pos = sp.pos;
			repeaterSp.size = new Vector2(0.2f, 0.2f);
			repeaterSp.color.W = 0.0f;
			
			base.Init(id, pos);
		}
		
		// ボタン押された際のイベント
		public override void ButtonEvent(bool pushR)
		{
			Rotate (pushR);
		}
	}
}

