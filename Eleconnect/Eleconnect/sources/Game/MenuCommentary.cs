using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class MenuCommentary
	{
		// 画像
		private Texture2D
			commentaryBarTex,
			commentaryBackTex,
			commentaryRetryTex,
			commentarySelectTex,
			commentaryTitleTex;
		
		private Sprite2D[] commentarySp = new Sprite2D[4];	// メニューの説明[数]
		private Sprite2D commentaryBarSp;					// メニュー説明背景
		
		// コンストラクタ
		public MenuCommentary ()
		{
			Init();
		}
		
		// 初期化
		private void Init()
		{
			// 画像
			commentaryBarTex = new Texture2D(@"/Application/assets/img/menuimg/commentarybar.png", false);
			commentaryBackTex = new Texture2D(@"/Application/assets/img/menuimg/commentaryback.png", false);
			commentaryRetryTex = new Texture2D(@"/Application/assets/img/menuimg/commentaryretry.png", false);
			commentarySelectTex = new Texture2D(@"/Application/assets/img/menuimg/commentaryselect.png", false);
			commentaryTitleTex = new Texture2D(@"/Application/assets/img/menuimg/commentarytitle.png", false);
			
			commentaryBarSp = new Sprite2D(commentaryBarTex);
			commentarySp[0] = new Sprite2D(commentaryBackTex);
			commentarySp[1] = new Sprite2D(commentaryRetryTex);
			commentarySp[2] = new Sprite2D(commentarySelectTex);
			commentarySp[3] = new Sprite2D(commentaryTitleTex);
			
			for(int i = 0;i < 4;i++)
			{
				commentarySp[i].center = new Vector2(0.0f);
				commentarySp[i].pos = new Vector3(7.0f,AppMain.ScreenHeight * 7.0f / 8.0f,0.0f);		// x軸、y軸微調整
				commentarySp[i].size = new Vector2(1.0f);
			}
			commentaryBarSp.center = new Vector2(0.0f);
			commentaryBarSp.size = new Vector2(1.0f);
			commentaryBarSp.pos = new Vector3(0.0f,(AppMain.ScreenHeight * 7.0f / 8.0f) - 7.0f,0.0f); 	// y軸微調整
			commentaryBarSp.color.W = 0.5f;																// 半透明に
		}
		
		// 更新(未使用)
		public void Update()
		{
			
		}
		
		// 描画
		public void Draw()
		{
			commentaryBarSp.Draw();
			commentarySp[MenuManager.menuNum - 1].Draw();
		}
		
		// 解放
		public void Term()
		{
			commentaryBarTex.Dispose ();
			commentaryBackTex.Dispose ();
			commentaryRetryTex.Dispose ();
			commentarySelectTex.Dispose ();
			commentaryTitleTex.Dispose ();
		}
	}
}

