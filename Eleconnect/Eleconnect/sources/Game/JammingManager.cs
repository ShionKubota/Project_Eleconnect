using System;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class JammingManager
	{
		// ジャミング
		public int[,] jammingDataLength;						// ジャミング配置データ(縦)
		public int[,] jammingDataSide;							// ジャミング配置データ(横)
		int jammingLength;										// ジャミング最大数(横)
		int jammingSide;										// ジャミング最大数(縦)
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
			jammingSide = GameScene.stageWidth;
			jammingLength = GameScene.stageHeight;
			panelSize = Panel.SIZE * Panel.SCALE + 5.0f;
			basePos = new Vector2(AppMain.ScreenCenter.X - ((panelSize * GameScene.stageWidth) / 2.0f) + panelSize,
			                              AppMain.ScreenCenter.Y - ((panelSize * GameScene.stageHeight) / 2.0f) + panelSize);
			jammingDataLength = new int[jammingSide,jammingLength];
			jammingDataSide = new int[jammingSide,jammingLength];
			jammingLocate = new Vector3(basePos.X + panelSize,
			                            basePos.Y + panelSize,
			                            0.0f);
			JammingSet();
		}
		
		public void JammingSet()
		{
			// ジャミング配置場所の数値は1
			for(int i = 0;i < jammingSide;i++)
			{
				for(int j = 0;j < jammingLength;j++)
				{
					jammingDataSide[i,j] = 1;
					jammingDataLength[i,j] = 1;
					
					/*if(i % 2 == 0)
					{
						jammingData[i,j] = 1;
					}
					*/
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
			
			// ジャミング縦
			for(int i = 0;i < jammingSide-1;i++)
			{
				for(int j = 0;j < jammingLength;j++)
				{
					if(jammingDataLength[i,j] == 1)
					{
						jammingLocate = new Vector3(basePos.X + panelSize * i,
				                            		basePos.Y-panelSize/2 + panelSize * j,
				                            		0.0f);
						jamming.Draw(jammingLocate,0.0f);
					}
				}
			}
			
			// ジャミング横
			for(int i = 0;i < jammingSide;i++)
			{
				for(int j = 0;j < jammingLength-1;j++)
				{
					if(jammingDataSide[i,j] == 1)
					{
						jammingLocate = new Vector3(basePos.X-panelSize/2 + panelSize * i,
				                            		basePos.Y + panelSize * j,
				                            		0.0f);
						jamming.Draw(jammingLocate,changeAngle);
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

