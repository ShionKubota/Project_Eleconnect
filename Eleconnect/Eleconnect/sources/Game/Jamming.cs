using System;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class Jamming
	{
		// 画像設定
		public Texture2D jamTex;
		public Sprite2D jamSp;
		
		private int aniFrame;
		private int frameCnt;
		
		//コンストラクタ
		public Jamming ()
		{
			Init();
		}
		
		// 初期化
		private void Init()
		{
			jamTex = new Texture2D(@"/Application/assets/img/Jamming2.png", false);
			jamSp = new Sprite2D(jamTex);
			jamSp.size = new Vector2(1.0f/8.0f,0.6f);
			jamSp.pos = new Vector3(0.0f);
			jamSp.textureUV = new Vector4(0.0f, 0.0f, 1.0f/8.0f, 1.0f);
			jamSp.angle = 1.0f;
			
			aniFrame = 0;
			frameCnt = 0;
		}
		
		// 更新
		public void Update()
		{
			frameCnt++;
			aniFrame += (frameCnt % 2) == 0 ? 1 : 0;
			jamSp.textureUV.X = (aniFrame % 7) * 1.0f/8.0f;
			jamSp.textureUV.Z = jamSp.textureUV.X + 1.0f/8.0f;
		}
		
		// 描画
		public void Draw(Vector3 pos,float angle)
		{
			jamSp.pos = new Vector3(pos.X,pos.Y,0.0f);
			jamSp.angle = angle;
			jamSp.DrawAdd();
		}
		
		// 解放
		public void Term()
		{
			//jamTex.Dispose();
			jamSp.Term();
		}
	}
}

