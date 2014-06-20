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
	public class Number
	{
		// 画像設定
		public Sprite2D[] numSp = new Sprite2D[10];
		public Texture2D[] numTex = new Texture2D[10];
		
		// コンストラクタ
		public Number ()
		{
			Init();
		}
		
		// 初期化
		private void Init()
		{
			// 画像設定
			for(int i = 0;i < 10;i++)
			{
				numTex[i] = new Texture2D(@"/Application/assets/img/number_"+ i +".png", false);
				numSp[i] = new Sprite2D(numTex[i]);
				
				numSp[i].size = new Vector2(0.5f);
				numSp[i].pos = new Vector3(AppMain.ScreenWidth/4.0f + i * 48.0f,AppMain.ScreenHeight/2.0f,0.0f);
			}
		}
		
		// 更新
		public void Update()
		{
			
		}
		
		// 描画
		public void Draw()
		{
			for(int i = 0;i<10;i++)
			{
				numSp[i].Draw();
			}
		}
		
		// 解放
		public void Term()
		{
			for(int i = 0;i<10;i++)
			{
				//numTex[i].Dispose();
				numSp[i].Term();
			}
		}
	}
}

