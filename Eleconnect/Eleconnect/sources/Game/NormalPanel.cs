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
			sp.color = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
			
			lightSp = new Sprite2D(tex[(int)id]);
			lightSp.pos = sp.pos;
			lightSp.textureUV = new Vector4(0.5f, 0.0f, 1.0f, 1.0f);
			lightSp.size = sp.size;
			
			base.Init(id, pos);
		}
		
		// ボタン押された際のイベント
		public override void ButtonEvent(bool pushR)
		{
			if(lineId == LineId.None)
			{
				//PanelManager.Displace(2, lineId == LineId.Side);
				Rotate (pushR);
			}
			else
			{
				PanelManager.Displace(this, pushR);
			}
		}
	}
}