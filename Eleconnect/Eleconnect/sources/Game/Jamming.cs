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
		
		//コンストラクタ
		public Jamming ()
		{
			Init();
		}
		
		// 初期化
		private void Init()
		{
			jamTex = new Texture2D(@"/Application/assets/img/Jamming.png", false);
			jamSp = new Sprite2D(jamTex);
			jamSp.size = new Vector2(0.22f,0.22f);
			jamSp.pos = new Vector3(0.0f);
			jamSp.angle = 1.0f;
		}
		
		// 更新
		public void Update()
		{
		}
		
		// 描画
		public void Draw(Vector3 pos,float angle)
		{
			jamSp.pos = new Vector3(pos.X,pos.Y,0.0f);
			jamSp.angle = angle;
			jamSp.Draw();
		}
		
		// 解放
		public void Term()
		{
			jamTex.Dispose();
		}
	}
}

