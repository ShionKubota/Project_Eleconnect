//--------------------------------------------------------------
// クラス名： LogoScene.cs
// コメント： ロゴシーンクラス
// 製作者　： MasatMio
// 制作日時： 2014/02/28
//--------------------------------------------------------------
using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Environment;
using System.Collections;
using System.Collections.Generic;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Eleconnect
{
	public class LogoScene : BaseScene
	{
		// スプライト宣言
		public Sprite2D backSp, logoSp;
		public Texture2D backTex, logoTex;
		static int fadeTime;
		
		// コンストラクタ
		public LogoScene ()
		{
			Init ();
		}
		
		// 初期化
		override public void Init()
		{
			// ロゴ
			logoTex = new Texture2D(@"/Application/assets/img/Logo.png", false);
			logoSp = new Sprite2D(logoTex);
			logoSp.pos = AppMain.ScreenCenter;
			
			// 背景色
			backTex = new Texture2D(@"/Application/assets/img/White.png", false);
			backSp = new Sprite2D(backTex);
			backSp.pos = AppMain.ScreenCenter;
			
			// フェードの起こる時間
			fadeTime = 0;
		}
		
		// 更新
		override public void Update()
		{
			// (Vita)STARTボタンで移行	(PC)Xボタンで移行
			/*if((Eleconnect.gamePadData.Buttons & GamePadButtons.Cross) != 0)
			{
				Eleconnect.fade.FadeOut();
			}*/
			
			fadeTime++;
			
			if(fadeTime > 100)
			{
				SceneManager.GetInstance().Switch(SceneId.TITLE);
			}
		}
		
		// 描画
		override public void Draw()
		{
			backSp.Draw();
			logoSp.Draw();
		}
		
		// 解放
		override public void Term()
		{
			backTex.Dispose();
			logoTex.Dispose();
		}
	}
}

