using System;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class JammingSwitch : Panel
	{
		private Texture2D tex;
		
		public static bool isJamming;
		
		// コンストラクタ
		public JammingSwitch (Vector2 pos) : base(TypeId.JammSwitch, pos)
		{
			//Init();
		}
		
		// 初期化
		public override void Init(TypeId id, Vector2 pos)
		{
			tex = new Texture2D(@"/Application/assets\img\switch.png", false);
			
			const float SWITCH_SCALE = 0.25f;
			sp = new Sprite2D(tex);
			sp.pos = new Vector3(pos.X, pos.Y, 0.0f);
			sp.size = new Vector2(SWITCH_SCALE / 2.0f, SWITCH_SCALE);
			sp.textureUV = new Vector4(0.0f, 0.0f, 0.5f, 1.0f);
			
			route[DirId.RIGHT] = route[DirId.LEFT] = route[DirId.UP] = route[DirId.DOWN] = true;
			
			isJamming = true;
		}
		
		// 更新
		public override void Update()
		{
			sp.textureUV.X = (isJamming) ? 0.0f : 0.5f;
			sp.textureUV.Z = sp.textureUV.X + 0.5f;
		}
		
		// 描画
		public override void Draw()
		{
			sp.Draw ();
		}
		
		// 解放
		public void Term()
		{
			tex.Dispose();
		}
		
		public override void ButtonEvent (bool pushR)
		{
		}
	}
}

