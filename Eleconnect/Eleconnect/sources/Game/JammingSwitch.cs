using System;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class JammingSwitch
	{
		private Texture2D[] switchTex = new Texture2D[2];
		private Sprite2D[] switchSp = new Sprite2D[2];
		
		public static bool isJamming{set; get;} 
		
		// コンストラクタ
		public JammingSwitch ()
		{
			Init();
		}
		
		// 初期化
		private void Init()
		{
			switchTex[0] = new Texture2D(@"/Application/assets/img/switch_on.png", false);
			switchTex[1] = new Texture2D(@"/Application/assets/img/switch_off.png", false);
			for(int i = 0;i<2;i++)
			{
				switchSp[i] = new Sprite2D(switchTex[i]);
				switchSp[i].pos = new Vector3(AppMain.ScreenWidth/2,AppMain.ScreenWidth/2,0.0f);
				switchSp[i].size = new Vector2(0.6f);
			}
			isJamming = true;
		}
		
		// 更新
		public void Update()
		{
			
		}
		
		// 描画
		public void Draw()
		{
			if(isJamming == true)
			{
				switchSp[0].Draw();
			}
			else
			{
				switchSp[1].Draw();
				Console.WriteLine("OFF");
			}
		}
		
		// 解放
		public void Term()
		{
			switchTex[0].Dispose();
			switchTex[0].Dispose();
		}
	}
}

