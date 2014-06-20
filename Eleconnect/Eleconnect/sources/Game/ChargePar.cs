using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class ChargePar
	{
		private Number num;
		private Sprite2D chargeSp;
		private Texture2D chargeTex;
		private int digit;
		private int[] numeral;
		private int connectNum;
		private int chargePar;
		private Random rand;
		
		private const float CHARGE_NUM_X = 160.0f;
		private const float CHARGE_NUM_Y = 256.0f;
		
		public ChargePar ()
		{
			Init();
		}
		
		private void Init()
		{
			chargeTex = new Texture2D(@"/Application/assets/img/charge.png", false);
			chargeSp = new Sprite2D(chargeTex);
			chargeSp.pos = new Vector3(96.0f,196.0f,0.0f);
			chargeSp.size = new Vector2(0.35f);
			
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
			int trueChargePar = (int)((float)connectNum / (float)(GameScene.stage.width * GameScene.stage.height) * 500.0f);	// 本来のチャージ率
			int parGap = trueChargePar - chargePar;		// 正確なチャージ率と表示用のチャージ率の差
			float up = (parGap * 0.1f);				// 正確なチャージ率にすこしづつ近づける(1ケタずつ上がっていくのが見たいので)
			if((int)up == 0 && parGap != 0)
			{
				up = (up > 0.0f) ? 1.0f : -1.0f;
			}
			chargePar += (int)up;
			
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
				num.numSp[numeral[i]].pos = new Vector3(CHARGE_NUM_X - (32.0f+rand.Next(-2,2)*chargePar/100%10) * i - 32.0f * chargePar*0.002f*i,
				                                        CHARGE_NUM_Y + rand.Next(-2,2)*chargePar/100%10,
				                                        0.0f);
				num.numSp[numeral[i]].Draw();
			}
			num.numSp[numeral[0]].size = new Vector2((1.0f+chargePar*0.002f)/4.0f);
			num.numSp[numeral[0]].pos = new Vector3(CHARGE_NUM_X + rand.Next(-2,2)*chargePar/100%10,
			                                        CHARGE_NUM_Y + rand.Next(-2,2)*chargePar/100%10,
			                                        0.0f);
			num.numSp[numeral[0]].Draw();
			chargeSp.Draw();
		}
		
		public void Term()
		{
			//chargeTex.Dispose();
			chargeSp.Term ();
		}
	}
}

