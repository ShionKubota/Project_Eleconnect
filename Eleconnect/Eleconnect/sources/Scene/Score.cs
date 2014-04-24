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
	public class Score
	{
		const int RAND_TIME = 140;				// ぐちゃぐちゃ表示時間
		public static float point;		// 得点
		public int[] numeral;			// 桁ごとの得点
		public int digit;				// 桁数
		public int frameCnt;
		
		public Number num;
		public TimeManager timeManager;
		
		public Score ()
		{
			Init();
		}
		
		private void Init()
		{
			num = new Number();
			
			point = PlayData.GetInstance().connectNum * 1000+ (int)TimeManager.timer * 100;		// 繋がってるパネルの数＋残り時間
			digit = point.ToString().Length;;
			numeral = new int[digit];
		}
		
		public void Update()
		{
			// 桁数取得してポイント表示しよう
			for(int i = 0;i < digit;i++)
			{
				if(i == 0)
				{
					numeral[i] = (int)point % 10;
				}
				else if ( i > 0)
				{
					numeral[i] = (int)(point / (FMath.Pow(10,i))) % 10;
				}
			}
			
			frameCnt++;
		}
		
		public void Draw()
		{
			Random rand = new Random();
			for(int i = 0;i < digit;i++)
			{
				if(frameCnt >= RAND_TIME)
				{
					num.numSp[numeral[i]].pos = new Vector3(AppMain.ScreenWidth/2.0f - (i * 64.0f)+ 320,AppMain.ScreenHeight/2.0f,0.0f);
					num.numSp[numeral[i]].Draw();
					frameCnt = RAND_TIME;
				}
				else
				{
					num.numSp[(frameCnt+rand.Next(i)) % 10].pos = new Vector3(AppMain.ScreenWidth/2.0f - (i * 64.0f)+ 320,AppMain.ScreenHeight/2.0f,0.0f);
					num.numSp[(frameCnt+rand.Next(i)) % 10].Draw();
				}
			}
		}
		
		public void Term()
		{
			num.Term();
		}
	}
}

