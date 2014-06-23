using System;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class JammingSwitch : Panel
	{
		private Texture2D tex;
		
		public static bool isJamming;
		private int aniFrame;
		private int frameCnt;
		
		// コンストラクタ
		public JammingSwitch (Vector2 pos) : base(TypeId.JammSwitch, pos)
		{
			//Init();
		}
		
		// 初期化
		public override void Init(TypeId id, Vector2 pos)
		{
			tex = new Texture2D(@"/Application/assets\img\jamm_anime.png", false);
			const float SWITCH_SCALE = 0.25f;
			sp = new Sprite2D(tex);
			sp.pos = new Vector3(pos.X, pos.Y, 0.0f);
			sp.size = new Vector2(SWITCH_SCALE / 5.0f, SWITCH_SCALE);
			sp.textureUV = new Vector4(0.0f, 0.0f, 1.0f/5.0f, 1.0f);
			
			route[DirId.RIGHT] = route[DirId.LEFT] = route[DirId.UP] = route[DirId.DOWN] = true;
			
			isJamming = true;
			aniFrame = 0;
			frameCnt = 0;
		}
		
		// 更新
		public override void Update()
		{
			frameCnt++;
			if(isJamming)
			{
				aniFrame += (frameCnt % 5) == 0 ? 1 : 0;
				sp.textureUV.X = (aniFrame % 4) * 1.0f/5.0f;
				sp.textureUV.Z = sp.textureUV.X + 1.0f/5.0f;
			}
			else
			{
				sp.textureUV.X = 4.0f/5.0f;
				sp.textureUV.Z = sp.textureUV.X + 1.0f/5.0f;
			}
		}
		
		// 描画
		public override void Draw()
		{
			sp.Draw ();
		}
		
		// 解放
		public void Term()
		{
			//tex.Dispose();
			sp.Term();
		}
		
		public override void ButtonEvent (bool pushR)
		{
		}
	}
}

