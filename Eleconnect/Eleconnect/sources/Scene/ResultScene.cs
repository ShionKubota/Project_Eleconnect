//--------------------------------------------------------------
// クラス名： Result.cs
// コメント： リザルト画面クラス
// 製作者　： MasayukiTada
// 制作日時： 2014/02/28
// 更新日時： 2014/02/28
//--------------------------------------------------------------
using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.UI;

namespace Eleconnect
{
	public class ResultScene : BaseScene
	{
		private Sprite2D 
			resultLogoSp,
			chargeSp,
			perSp,
			pakSp;
		private Texture2D 
			resultLogoTex,
			chargeTex,
			perTex,
			pakTex;
		
		private float
			frameCnt,
			alfaGain;
		
		private bool 
			stageChange,
			seFlg;
		
		private MusicEffect musicEffect;
		
		private Score score;
		
		Input input = Input.GetInstance();
		
		// コンストラクタ
		public ResultScene ()
		{
			Init ();
		}
		
		// 初期化
		override public void Init()
		{
			resultLogoTex = new Texture2D(@"/Application/assets/img/result.png", false);
			resultLogoSp = new Sprite2D(resultLogoTex);
			resultLogoSp.size = new Vector2(1.0f);
			resultLogoSp.pos = new Vector3(AppMain.ScreenWidth/2.0f,resultLogoTex.Height/2.0f+30,0.0f);
			
			chargeTex = new Texture2D(@"/Application/assets/img/charge.png", false);
			chargeSp = new Sprite2D(chargeTex);
			chargeSp.size = new Vector2(0.5f);
			chargeSp.pos = new Vector3(AppMain.ScreenWidth / 4.0f,AppMain.ScreenHeight/2.0f,0.0f);
			
			// "/500"画像
			perTex = new Texture2D(@"/Application/assets/img/percentage.png", false);
			perSp = new Sprite2D(perTex);
			perSp.size = new Vector2(0.5f);
			perSp.pos = AppMain.ScreenCenter + new Vector3(230.0f, 20.0f, 0.0f);
			
			pakTex = new Texture2D(@"/Application/assets/img/pushlogo.png", false);
			pakSp = new Sprite2D(pakTex);
			pakSp.size = new Vector2(0.8f);
			pakSp.pos = new Vector3(AppMain.ScreenWidth/2.0f,AppMain.ScreenHeight - 128.0f,0.0f);
			
			frameCnt = 0;
			alfaGain = 0.05f;
			stageChange = false;
			
			musicEffect = new MusicEffect(@"/Application/assets/se/Title_SE.wav");
			
		}
		
		
		// 更新
		override public void Update()
		{
			if(score == null)
			{
				score = new Score();
			}
			frameCnt++;
			score.Update();
			pakSp.color.W = 0.3f + 
						 FMath.Sin(FMath.Radians(frameCnt * 5)) * 0.6f;
			
			// 押したらタイトルに戻る
			if((input.circle.isPushEnd || input.cross.isPushEnd || input.triangle.isPushEnd ||
			   input.square.isPushEnd || input.triggerR.isPushEnd || input.triggerL.isPushEnd ||
			   input.start.isPushEnd || input.select.isPushEnd) && !stageChange)
			{
				if(seFlg == false && score.randEnd == true)
				{
					musicEffect.Set();
					seFlg = true;
				}
				if(PlayData.GetInstance().stageNo == 8)
				{
					AppMain.sceneManager.Switch(SceneId.TITLE);
					PlayData.GetInstance().stageNo = 0;
					stageChange = true;
					return;
				}
			}
			else if(seFlg == true && !stageChange)
			{
				PlayData.GetInstance().stageNo++;
				AppMain.sceneManager.Switch(SceneId.GAME);
				stageChange = true;
			}
			
		}
		
		// 描画
		override public void Draw()
		{
			resultLogoSp.Draw();
			chargeSp.Draw();
			score.Draw();
			perSp.Draw ();
			if(score.randEnd == true)
			{
				pakSp.Draw();
			}
		}
		
		// 解放
		public override void Term()
		{
			resultLogoSp.Term();
			chargeSp.Term();
			pakSp.Term();
			//score.Term();
			seFlg = false;
			stageChange = false;
			perSp.Term();
		}
	}
}

