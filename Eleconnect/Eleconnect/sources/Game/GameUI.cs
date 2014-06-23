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
			
		}
		
		// 更新
		public void Update(GameScene.StateId nowState)
		{
			Console.WriteLine(nowState);
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
			timeManager.Draw();
			charge.Draw();
		}
		
		// 解放
		public void Term()
		{
			timeManager.Term();
		}
	}
}

