//--------------------------------------------------------------
// クラス名： TitleScene.cs
// コメント： タイトルシーンクラス
// 製作者　： ShionKubota
// 制作日時： 2014/02/28
// 更新日時： 2014/02/28
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
	public class TitleScene : BaseScene
	{
		private Sprite2D
			backSp,
			startSp,
			logoSp;
		private Texture2D
			backTex,
			startTex,
			logoTex;
		
		public static Music music;
		private MusicEffect musicEffect;
		
		private int frameCnt;
		private bool seFlg;
		
		private Particles particle;
		private SelectScene selectScene;
		
		// コンストラクタ
		public TitleScene ()
		{
			Init ();
		}
		
		// 初期化
		override public void Init()
		{
			// 画像
			logoTex = new Texture2D(@"/Application/assets/img/eleconnect_logo.png", false);
			logoSp = new Sprite2D(logoTex);
			logoSp.pos = new Vector3(AppMain.ScreenWidth / 2.0f, AppMain.ScreenHeight / 4.0f, 0.0f);
			logoSp.size = new Vector2(1.0f);
			
			backTex = new Texture2D(@"/Application/assets/img/eleconnect_titlebackground03.png", false);
			backSp = new Sprite2D(backTex);
			backSp.center = new Vector2(0.0f);
			backSp.size = new Vector2(1.0f);
			backSp.pos = new Vector3(0.0f);
			
			startTex = new Texture2D(@"/Application/assets/img/startlogo.png", false);
			startSp = new Sprite2D(startTex);
			startSp.size = new Vector2(1.0f,1.0f);
			startSp.pos = new Vector3(AppMain.ScreenWidth / 2.0f ,AppMain.ScreenHeight *3.0f /4.0f,0.0f);
			/*
			start_rTex = new Texture2D(@"/Application/assets/img/startlogo_r.png", false);
			start_rSp = new Sprite2D(start_rTex);
			start_rSp.size = new Vector2(1.0f,1.0f);
			start_rSp.pos = new Vector3(AppMain.ScreenWidth / 2.0f ,AppMain.ScreenHeight *3.0f /4.0f,0.0f);
			*/
			// 音関連
			music = new Music(@"/Application/assets/music/Title_Music.mp3");
			musicEffect = new MusicEffect(@"/Application/assets/se/Title_SE.wav");
			music.Set(true,0.6f,0.0d);
			
			this.fadeInTime = 120;
			this.fadeOutTime = 30;
			this.fadeInColor = new Vector3(1.0f);
			
			frameCnt = 0;
			seFlg = false;
			MenuManager.menuFlg = false;
			
			// パーティクルテスト
			/*
			particle = new Particles(200);
			particle.LoadTextureInfo(@"Application/assets/img/particle.png", false);
			particle.pos = new Vector2(AppMain.ScreenWidth / 2.0f, AppMain.ScreenHeight / 4.0f);
			particle.posVar = new Vector2(250.0f, 0.0f);
			particle.velocity = new Vector2(0.0f, 0.0f);
			particle.velocityVar = new Vector2(0.0f, 0.0f);
			particle.colorEnd = new Vector3(0.6f, 0.8f, 0.9f);
			particle.colorEndVar = new Vector3(1.0f);
			particle.scaleStartVar = 0.0f;
			particle.scaleEndVar = 0.0f;
			particle.lifeSpan = 0.1f;
			particle.gravity = new Vector2(0.0f, 0.0f);
			particle.fade = 0.1f;
			*/
			
			particle = new Particles(200);
			particle.LoadTextureInfo(@"Application/assets/img/particle.png", false);
			particle.pos = new Vector2(AppMain.ScreenWidth / 2.0f, AppMain.ScreenHeight / 4.0f);
			particle.posVar = new Vector2(250.0f, 10.0f);
			particle.velocity = new Vector2(0.0f, 0.0f);
			particle.velocityVar = new Vector2(3.0f, 3.0f);
			particle.colorEnd = new Vector3(0.6f, 0.8f, 0.9f);
			particle.colorEndVar = new Vector3(1.0f);
			particle.scaleStartVar = 3.0f;
			particle.scaleEndVar = 0.0f;
			particle.lifeSpan = 2.0f;
			particle.gravity = new Vector2(0.0f, -0.00f);
			particle.fade = 0.2f;
			
			selectScene = new SelectScene();
		}
		
		// 更新
		override public void Update()
		{
			music.Play();
			startSp.color.W = 0.3f + FMath.Sin(FMath.Radians(frameCnt * 2)) * 0.6f;			// Startボタンの点滅
			//clear.Update();
			/*回転
			panelTurn+=0.01f;
			startSp.size.X =FMath.Sin(panelTurn*2);
			if(startSp.size.X <= 0 && turnFlg == false)
			{
				start_rSp.size.X = 0.01f;
				panelTurn = 0.01f;
				turnFlg = true;
			}
			if(start_rSp.size.X <= 0 && turnFlg == true)
			{
				startSp.size.X = 0.01f;
				panelTurn = 0.01f;
				turnFlg = false;
			}
			if(turnFlg)
			{
				start_rSp.size.X = FMath.Sin(panelTurn*2);
			}*/
			
			// なにかボタンが押されたらフェードアウト開始
			Input input = Input.GetInstance();
			if((input.circle.isPushEnd || input.cross.isPushEnd || input.triangle.isPushEnd ||
			   input.square.isPushEnd || input.triggerR.isPushEnd || input.triggerL.isPushEnd ||
			   input.start.isPushEnd || input.select.isPushEnd) && seFlg == false)
			{
				
				seFlg = true;
				musicEffect.Set(0.6f,false);
				frameCnt = 0;
			}
			
			if(seFlg == true)
			{
				startSp.color.W = 0.3f + 
						 FMath.Sin(FMath.Radians(frameCnt * 50)) * 0.6f;
				
				logoSp.pos.Y += ((AppMain.ScreenHeight / 8.0f) - logoSp.pos.Y) * 0.05f;
				particle.pos.Y = logoSp.pos.Y;
			}
			
			if(frameCnt >= 90 && seFlg == true)
			{
				selectScene.Update();
			}
			particle.Update();
			frameCnt++;
		}
		
		// 描画
		override public void Draw()
		{
			backSp.Draw();
			particle.Draw ();
			logoSp.Draw ();
			if(frameCnt >= 90 && seFlg == true)
			{
				selectScene.Draw();
			}
			else
			{
				//if(turnFlg == false)
				startSp.Draw();
				//else
				//start_rSp.Draw();
				
			}
		}
		
		// 解放(musicはセレクトシーンでも使用しているのでセレクトシーンで解放)
		override public void Term()
		{
			//backTex.Dispose ();
			//logoTex.Dispose();
			//startTex.Dispose();
			//start_rTex.Dispose();
			backSp.Term();
			logoSp.Term();
			startSp.Term();
			musicEffect.Term();
			seFlg = false;
		}
	}
}

