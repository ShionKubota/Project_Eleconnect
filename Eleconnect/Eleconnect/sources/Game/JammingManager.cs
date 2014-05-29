using System;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class JammingManager
	{
		// ジャミング
		public int[,] jammingData;							// ジャミング配置データ
		int jammingX;										// ジャミング最大数(横)
		int jammingY;										// ジャミング最大数(縦)
		float panelSize;
		Vector2 basePos;
		public Vector3 jammingLocate;
		const float changeAngle =  90.0f;
		
		Jamming jamming;
		JammingSwitch jammSwitch;
		
		// コンストラクタ
		public JammingManager ()
		{
			Init();
		}
		
		private void Init()
		{
			jamming = new Jamming();
			jammingX = GameScene.stageWidth-1;
			jammingY = GameScene.stageHeight*2-1;
			panelSize = Panel.SIZE * Panel.SCALE + 5.0f;
			basePos = new Vector2(AppMain.ScreenCenter.X - ((panelSize * GameScene.stageWidth) / 2.0f) + panelSize,
			                              AppMain.ScreenCenter.Y - ((panelSize * GameScene.stageHeight) / 2.0f) + panelSize);
			jammingData = new int[jammingX,jammingY];
			jammingLocate = new Vector3(basePos.X + panelSize,
			                            basePos.Y + panelSize,
			                            0.0f);
			JammingSet();
		}
		
		public void JammingSet()
		{
			// ジャミング配置場所の数値は1
			for(int i = 0;i < jammingX;i++)
			{
				for(int j = 0;j < jammingY;j++)
				{
					//jammingData[i,j] = 1;
					
					if(i == 0 && j == 0)
					{
						jammingData[i,j] = 1;
					}
					
				}
			}
		}
		
		// 更新
		public void Update()
		{
		}
		
		// 描画
		public void Draw()
		{
			if(!JammingSwitch.isJamming) return;
			
			for(int i = 0;i < jammingX;i++)
			{
				for(int j = 0;j < jammingY;j++)
				{
					if(jammingData[i,j] == 1)
					{
						// 縦向き
						if(j%2 == 0)
						{
							jammingLocate = new Vector3(basePos.X + panelSize * i,
				                            		basePos.Y-panelSize/2 + panelSize * (j/2),
				                            		0.0f);
							jamming.Draw(jammingLocate,0.0f);
						}
						
						// 横向き
						if(j%2 == 1)
						{ 
							jammingLocate = new Vector3(basePos.X-panelSize/2 + panelSize * i,
				                            		basePos.Y + panelSize * (j/2),
				                            		0.0f);
							jamming.Draw(jammingLocate,changeAngle);
						}
					}
				}
			}
			}
		
		// 解放
		public void Term()
		{
			jamming.Term();
			jammSwitch.Term();
		}
	}
}

