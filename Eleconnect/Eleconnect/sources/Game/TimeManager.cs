using System;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class TimeManager
	{
		public static float timer;
		public int digit;
		public int[] numeral;
		public Sprite2D timeSp;
		public Texture2D timeTex;
		
		public Number num;
		public Score score;
		
		public TimeManager ()
		{
			Init();
		}
		
		private void Init()
		{
			timer =120.0f;
			digit = timer.ToString().Length;
			numeral = new int[digit];
			
			timeTex= new Texture2D(@"/Application/assets/img/timelogo.png", false);
			timeSp = new Sprite2D(timeTex);
			timeSp.size = new Vector2(0.5f);
			timeSp.pos = new Vector3(AppMain.ScreenWidth-160.0f,32.0f,0.0f);
			
			num = new Number();
		}
		
		public void Update()
		{
			timer -= 1.0f / 60.0f;
			
			// 桁数取得してポイント表示しよう
			for(int i = 0;i < digit;i++)
			{
				if(i == 0)
				{
					numeral[i] = (int)timer % 10;
				}
				else if ( i > 0)
				{
					numeral[i] = (int)(timer / (FMath.Pow(10,i))) % 10;
				}
			}
			
			if(timer <= 0.0f)
			{
				AppMain.sceneManager.Switch(SceneId.RESULT);
			}
		}
		
		public void Draw()
		{
			for(int i = 0;i<digit;i++)
			{
				num.numSp[numeral[i]].size = new Vector2(1.0f/4.0f);
				num.numSp[numeral[i]].pos = new Vector3(AppMain.ScreenWidth - 32.0f * i - 64.0f,80.0f,0.0f);
				num.numSp[numeral[i]].Draw();
			}
			timeSp.Draw();
			
			
		}
		
		public void Term()
		{
			timeTex.Dispose();
		}
	}
}

