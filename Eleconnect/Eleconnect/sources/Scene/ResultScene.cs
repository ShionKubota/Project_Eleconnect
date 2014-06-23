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
			scoreSp,
			pakSp;
		private Texture2D 
			resultLogoTex,
			scoreTex,
			pakTex;
		
		private float
			frameCnt,
			alfaGain;
		
		private bool 
			gainChange,
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
			
			scoreTex = new Texture2D(@"/Application/assets/img/Score.png", false);
			scoreSp = new Sprite2D(scoreTex);
			scoreSp.size = new Vector2(0.5f);
			scoreSp.pos = new Vector3(AppMain.ScreenWidth / 4.0f,AppMain.ScreenHeight/2.0f,0.0f);
			
			pakTex = new Texture2D(@"/Application/assets/img/pushlogo.png", false);
			pakSp = new Sprite2D(pakTex);
			pakSp.size = new Vector2(0.8f);
			pakSp.pos = new Vector3(AppMain.ScreenWidth/2.0f,AppMain.ScreenHeight - 128.0f,0.0f);
			
			frameCnt = 0;
			alfaGain = 0.05f;
			gainChange = false;
			
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
			if(input.circle.isPushEnd || input.cross.isPushEnd || input.triangle.isPushEnd ||
			   input.square.isPushEnd || input.triggerR.isPushEnd || input.triggerL.isPushEnd ||
			   input.start.isPushEnd || input.select.isPushEnd)
			{
				if(seFlg == false && score.randEnd == true)
				{
					musicEffect.Set();
					seFlg = true;
				}
			}
			
			if(seFlg == true)
			{
				PlayData.GetInstance().stageNo++;
				AppMain.sceneManager.Switch(SceneId.GAME);
				seFlg=false;
			}
		}
		
		// 描画
		override public void Draw()
		{
			resultLogoSp.Draw();
			scoreSp.Draw();
			score.Draw();
			if(score.randEnd == true)
			{
				pakSp.Draw();
			}
		}
		
		// 解放
		public override void Term()
		{
			resultLogoSp.Term();
			scoreSp.Term();
			pakSp.Term();
			score.Term();
			seFlg = false;
		}
	}
}

