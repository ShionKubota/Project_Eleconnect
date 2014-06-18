using System;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class ChargePar
	{
		private Number num;
		private int digit;
		private int[] numeral;
		private int connectNum;
		private int chargePar;
		private Random rand;
		public ChargePar ()
		{
			Init();
		}
		
		private void Init()
		{
			num = new Number();
			digit = 0;
			connectNum = 0;
			chargePar = 0;
			numeral = new int[3];
			rand = new Random();
		}
		
		public void Update()
		{
			connectNum = PlayData.GetInstance().connectNum;
			chargePar = (int)((float)connectNum / (float)(GameScene.stage.width * GameScene.stage.height) * 500.0f);
			digit = chargePar.ToString().Length;
			// 桁数取得してポイント表示しよう
			for(int i = 0;i < digit;i++)
			{
				if(i == 0)
				{
					numeral[i] = (int)chargePar % 10;
				}
				else if ( i > 0)
				{
					numeral[i] = (int)(chargePar / (FMath.Pow(10,i))) % 10;
				}
			}
		}
		
		public void Draw()
		{
			for(int i = 1;i<digit;i++)
			{
				num.numSp[numeral[i]].size = new Vector2((1.0f+chargePar*0.002f)/4.0f);
				num.numSp[numeral[i]].pos = new Vector3(160.0f - (32.0f+rand.Next(-2,2)*chargePar/100%10) * i - 32.0f * chargePar*0.002f*i,
				                                        320.0f+rand.Next(-2,2)*chargePar/100%10,
				                                        0.0f);
				num.numSp[numeral[i]].Draw();
			}
			num.numSp[numeral[0]].size = new Vector2((1.0f+chargePar*0.002f)/4.0f);
			num.numSp[numeral[0]].pos = new Vector3(160.0f + rand.Next(-2,2)*chargePar/100%10,
			                                        320.0f+rand.Next(-2,2)*chargePar/100%10,
			                                        0.0f);
			num.numSp[numeral[0]].Draw();
		}
		
		public void Term()
		{
			
		}
	}
}

