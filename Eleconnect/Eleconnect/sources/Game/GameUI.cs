//--------------------------------------------------------------
// クラス名： GameUI.cs
// コメント： ゲーム中のUIを管理するクラス
// 製作者　： ShionKubota
// 制作日時： 2014/03/05
//--------------------------------------------------------------
using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class GameUI
	{
		protected TimeManager timeManager;
		protected ChargePar charge;
		
		protected Sprite2D uiAreaSp;
		protected Texture2D uiAreaTex;
		
		// コンストラクタ
		public GameUI ()
		{
			Init ();
		}
		
		// 初期化
		private void Init()
		{
			timeManager = new TimeManager();
			charge = new ChargePar();
			
			uiAreaTex = new Texture2D(@"/Application/assets/img/UIArea.png", false);
			uiAreaSp = new Sprite2D(uiAreaTex);
			uiAreaSp.center = new Vector2(0.0f, 0.0f);
			uiAreaSp.pos = new Vector3(0.0f);
		}
		
		// 更新
		public void Update(GameScene.StateId nowState)
		{
			// タイムの更新
			if(nowState == GameScene.StateId.GAME)
			{
				timeManager.Update();
			}
			// チャージ率
			charge.Update();
		}
		
		
		// 描画
		public void Draw()
		{
			uiAreaSp.Draw ();
			timeManager.Draw();
			charge.Draw();
		}
		
		// 解放
		public void Term()
		{
			timeManager.Term();
			charge.Term();
		}
	}
}

