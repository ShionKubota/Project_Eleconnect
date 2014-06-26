using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class TutorialScene : GameScene
	{
		ChargePar chargePar;
		Sprite2D  vitaSp, guideSp;
		Texture2D guideTex;
		
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
			
			// 操作方法説明用画像
			guideTex = new Texture2D(@"/Application/assets/img/tutorial.png", false);
			guideSp = new Sprite2D(guideTex);
			guideSp.size = new Vector2(1.0f, 1.0f / 3.0f);
			guideSp.size *= new Vector2(0.7f);
			guideSp.color.W = 0.0f;
			guideSp.textureUV = new Vector4(0.0f, 1.0f / 3.0f, 1.0f, 2.0f / 3.0f);
			guideSp.pos = AppMain.ScreenCenter;
			guideSp.pos.Y += 150;
			
			// vita画像
			vitaSp = new Sprite2D(guideTex);
			vitaSp.size = new Vector2(1.0f, 1.0f / 3.0f);
			vitaSp.size *= new Vector2(0.7f);
			vitaSp.color.W = 0.0f;
			vitaSp.textureUV = new Vector4(0.0f, 0.0f, 1.0f, 1.0f / 3.0f);
			vitaSp.pos = guideSp.pos;
			
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
			
			// vita表示
			vitaSp.color.W += 0.1f;
			// 操作説明画像を明滅させる
			guideSp.color.W = FMath.Sin(frameCnt * 0.05f);
			
			// 操作方法画像を切り替える(回転のさせ方　→　エレクスの流し方)
			if(panelManager[stage.goalX, stage.goalY].elecPow > 0 &&
			   guideSp.color.W <= 0.0f)
			{
				guideSp.textureUV = new Vector4(0.0f, 2.0f / 3.0f, 1.0f, 1.0f);
			}
			
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
			electhSp.DrawAdd();
			lowElecthSp.DrawAdd();
			chargePar.Draw();
			vitaSp.Draw ();
			guideSp.Draw();
		}
		
		public override void Term ()
		{
			chargePar.Term();
			vitaSp.Term();
			guideSp.Term ();
			base.Term();
		}
	}
}

