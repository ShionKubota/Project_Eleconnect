//--------------------------------------------------------------
// クラス名： SelectScene.cs
// コメント： 楽曲セレクトシーン
// 製作者　： ShionKubota
// 制作日時： 2014/03/06
// 更新日時： 2014/03/06
//--------------------------------------------------------------
using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class SelectScene : BaseScene
	{
		// ボタン設置
		public SelectButton selectBtn;
		// 画像設定
		private Sprite2D selectBackSp;
		private Texture2D selectBackTex;
		
		private MusicEffect musicEffect;
		
		Input input = Input.GetInstance();
		public SelectScene ()
		{
			Init ();
		}
		
		// 初期化
		public override void Init()
		{
			selectBtn = new SelectButton();
			
			selectBackTex = new Texture2D(@"/Application/assets/img/eleconnect_background01.png", false);
			selectBackSp = new Sprite2D(selectBackTex);
			selectBackSp.center = new Vector2(0.0f,0.0f);
			selectBackSp.size = new Vector2(1.0f,1.0f);
			selectBackSp.pos = new Vector3(0.0f,0.0f,0.0f);
			
			musicEffect = new MusicEffect(@"/Application/assets/se/Choice_SE.wav");
		}
		
		// 更新
		public override void Update()
		{
			// ボタンの移動等更新
			selectBtn.Update();
			
			// 決定したときの処理
			if(input.circle.isPushEnd)
			{
				if(TitleScene.seFlg == false)
				{
					musicEffect.Set();
					TitleScene.seFlg = true;
				}
			}
			
			// 選んだステージに移動
			if( TitleScene.seFlg == true)
			{
				for(int i = 0;i <= SelectButton.STAGE_NUM;i++)
				{
					if(i == selectBtn.selectBtnNo)
					{
						PlayData.GetInstance().stageNo = (i - 1);
						AppMain.sceneManager.Switch(SceneId.GAME);
					}
				}
			}
		}
		
		
		// 描画
		public override void Draw()
		{
			selectBackSp.Draw();
			selectBtn.Draw();
		}
		
		// 解放
		public override void Term()
		{
			TitleScene.music.Term();
			musicEffect.Term();
			selectBackTex.Dispose();
			selectBtn.Term();
			TitleScene.seFlg = false;
		}
	}
}

