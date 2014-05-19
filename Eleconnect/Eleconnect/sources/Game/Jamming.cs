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
		public Jamming (Vector2 pos,float angle)
		{
			Init(pos,angle);
		}
		
		// 初期化
		private void Init(Vector2 pos,float angle)
		{
			jamTex = new Texture2D(@"/Application/assets/img/Jamming.png", false);
			jamSp = new Sprite2D(jamTex);
			jamSp.size = new Vector2(1.0f,1.0f);
			jamSp.pos = new Vector3(pos.X,pos.Y,0.0f);
		}
		
		// 更新
		public void Update()
		{
		}
		
		// 描画
		public void Draw()
		{
			jamSp.Draw();
		}
		
		// 解放
		public void Term()
		{
			jamTex.Dispose();
		}
	}
}

