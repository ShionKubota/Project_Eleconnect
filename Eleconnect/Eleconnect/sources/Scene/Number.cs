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
		
		// 表示するNo.変更
		public int minNumber = 0;
		public int maxNumber = 9;
		
		// コンストラクタ
		public Number (int minNum,int maxNum)
		{
			Init(minNum,maxNum);
		}
		
		// 初期化
		private void Init(int minNum,int maxNum)
		{
			minNumber = minNum;
			maxNumber = maxNum;
			// 画像設定
			for(int i =minNum;i <= maxNum;i++)
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
		public void Draw(int number)
		{
			for(int i = minNumber;i <=maxNumber;i++)
			{
				numSp[number].Draw();
			}
		}
		
		// 解放
		public void Term()
		{
			for(int i = minNumber;i<= maxNumber;i++)
			{
				//numTex[i].Dispose();
				numSp[i].Term();
			}
		}
		
		public void SetPos(Vector3 pos)
		{
			for(int i = minNumber;i<= maxNumber;i++)
			{
				numSp[i].pos = pos;
			}
		}
		
		public void SetSize(Vector2 size)
		{
			for(int i = minNumber;i<= maxNumber;i++)
			{
				numSp[i].size = size;
			}
		}
	}
}

