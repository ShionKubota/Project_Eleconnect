using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class TutorialScene : GameScene
	{
		ChargePar chargePar;
		public TutorialScene ()
		{
			Init ();
		}
		
		public override void Init ()
		{
			PlayData.GetInstance().stageNo = -1;
			stage.mapData = new int[]{2, 3, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 2, 1};
			stage.width = 9;
			stage.height = 1;
			stage.startX = 0;
			stage.startY = 0;
			stage.goalX = 8;
			stage.goalY = 0;
			stage.jamming = -1;
			
			panelManager = new PanelManager();
			chargePar = new ChargePar();
			CommonInit();
			
			backTex = new Texture2D(@"/Application/assets/img/eleconnect_titlebackground03.png", false);
			backSp = new Sprite2D(backTex);
			backSp.center = new Vector2(0.0f);
			backSp.size = new Vector2(1.0f);
			backSp.pos = new Vector3(0.0f);
		}
		
		public override void Update ()
		{
			chargePar.Update();
			base.Update ();
			
			if(nowState == GameScene.StateId.CLEAR)
			{
				fadeOutColor = new Vector3(1.0f);
				fadeOutTime = 120;
				AppMain.sceneManager.Switch(SceneId.TITLE);
			}
		}
		
		public override void Draw ()
		{
			backSp.Draw();
			panelManager.Draw();
			cursor.Draw();
			electhManager.Draw();
			chargePar.Draw();
		}
		
		public override void Term ()
		{
			chargePar.Term();
			base.Term();
		}
	}
}

