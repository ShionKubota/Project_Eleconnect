//--------------------------------------------------------------
// クラス名： SceneManager.cs
// コメント： シーン管理クラス
// 製作者　： ShionKubota
// 制作日時： 2014/02/28
// 更新日時： 2014/02/28
//--------------------------------------------------------------
using System;
using Sce.PlayStation.HighLevel.UI;

namespace Eleconnect
{
	public sealed class SceneManager
	{
		private Fade fade;
		private static SceneManager sceneManager;
		private BaseScene nowScene;
		
		private SceneId nextId;
		
		// 唯一のインスタンスを取得
		public static SceneManager GetInstance()
		{
			if(sceneManager == null)
			{
				sceneManager = new SceneManager();
			}
			return sceneManager;
		}
		
		private SceneManager ()
		{
			Init ();
		}
		
		public void Init()
		{
			// 最初に開始するシーン
			PlayData.GetInstance().stageNo = 8;
			nowScene = new TitleScene();
			
			fade = new Fade();
		}
		
		public void Update()
		{
			nowScene.Update();
			fade.Update();
			
			/*
			 シーンのフェードアウト終了後
			 	・UIシーン初期化
			 	・シーンインスタンスを入れ替え
			 	・フェードイン開始
			*/
			if(fade.state == Fade.StateId.OUT_END)
			{
				nowScene.Term ();
				
				switch(nextId)
				{
				case SceneId.LOGO:
					nowScene = new LogoScene();
					break;
				case SceneId.TUTORIAL:
					nowScene = new TutorialScene();
					break;
				case SceneId.TITLE:
					nowScene = new TitleScene();
					break;
				case SceneId.SELECT:
					nowScene = new SelectScene();
					break;
				case SceneId.GAME:
					nowScene = new GameScene();
					break;
				case SceneId.EDIT:
					nowScene = new EditScene();
					break;
				case SceneId.RESULT:
					nowScene = new ResultScene();
					break;
				}
				fade.In(nowScene.fadeInColor, nowScene.fadeInTime);
				GC.Collect();
			}
		}
		
		public void Term()
		{
			nowScene.Term();
			fade.Term ();
		}
		
		public void Draw()
		{
			nowScene.Draw();
			UISystem.Render();
			fade.Draw ();
		}
		
		// シーン切り替え(フェードアウト開始)
		public void Switch(SceneId nextSceneId)
		{
			if(fade.state == Fade.StateId.OUT || fade.state == Fade.StateId.OUT_END) return;
			
			nextId = nextSceneId;
			fade.Out(nowScene.fadeOutColor, nowScene.fadeOutTime);	// フェードアウト開始
		}
	}
}