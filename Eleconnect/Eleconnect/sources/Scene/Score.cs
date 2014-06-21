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
		const int RAND_TIME = 140;		// ぐちゃぐちゃ表示時間
		public static float point;		// 得点
		int[] numeral;					// 桁ごとの得点
		int digit;						// 桁数
		int randCnt;
		public bool randEnd;
		
		public Number num;
		public TimeManager timeManager;
		
		// コンストラクタ
		public Score ()
		{
			Init();
		}
		
		// 初期化
		private void Init()
		{
			num = new Number();
			
			point = (int)((float)PlayData.GetInstance().connectNum / (float)(GameScene.stage.width * GameScene.stage.height) * 500.0f) * 100
				+ (int)TimeManager.timer * 100;		// 繋がってるパネルの数＋残り時間
			Console.WriteLine(point);
			Console.WriteLine(PlayData.GetInstance().connectNum);
			Console.WriteLine((int)TimeManager.timer);
			digit = point.ToString().Length;
			numeral = new int[digit];
			randCnt = 0;
			randEnd = false;
		}
		
		// 更新
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
			randCnt++;
		}
		
		// 描画
		public void Draw()
		{
			//	数字をぐちゃぐちゃ
			Random rand = new Random();
			
			//	frameが一定数でスコア表示
			for(int i = 0;i < digit;i++)
			{
				if(randCnt >= RAND_TIME)
				{
					num.numSp[numeral[i]].pos = new Vector3(AppMain.ScreenWidth/2.0f - (i * 64.0f)+ 320,AppMain.ScreenHeight/2.0f,0.0f);
					num.numSp[numeral[i]].Draw();
					randCnt = RAND_TIME;
					randEnd = true;
				}
				else
				{
					num.numSp[(randCnt+rand.Next(i)) % 10].pos = new Vector3(AppMain.ScreenWidth/2.0f - (i * 64.0f)+ 320,AppMain.ScreenHeight/2.0f,0.0f);
					num.numSp[(randCnt+rand.Next(i)) % 10].Draw();
				}
			}
		}
		
		// 解放
		public void Term()
		{
			num.Term();
		}
	}
}

