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
			jammingSide = GameScene.stage.width;
			jammingLength = GameScene.stage.height;
			panelSize = Panel.SIZE * Panel.SCALE + 5.0f;
			basePos = new Vector2(AppMain.ScreenCenter.X - ((panelSize * GameScene.stage.width) / 2.0f) + panelSize,
			                              AppMain.ScreenCenter.Y - ((panelSize * GameScene.stage.height) / 2.0f) + panelSize);
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
					if(PlayData.GetInstance().stageNo == 0)
					{
						jammingDataLength[0,1] = 1;
						jammingDataLength[0,2] = 1;
						jammingDataLength[0,3] = 1;
						jammingDataLength[3,1] = 1;
						jammingDataLength[3,2] = 1;
						jammingDataLength[3,3] = 1;
						jammingDataSide[1,0] = 1;
						jammingDataSide[2,0] = 1;
						jammingDataSide[3,0] = 1;
						jammingDataSide[1,3] = 1;
						jammingDataSide[2,3] = 1;
						jammingDataSide[3,3] = 1;
						jammingDataSide[4,3] = 1;
					}
					if(PlayData.GetInstance().stageNo == 1)
					{
						jammingDataLength[1,4] = 1;
						jammingDataLength[2,3] = 1;
						jammingDataLength[3,2] = 1;
						jammingDataLength[5,1] = 1;
						jammingDataSide[6,0] = 1;
						jammingDataSide[7,0] = 1;
						jammingDataSide[4,1] = 1;
						jammingDataSide[5,1] = 1;
						jammingDataSide[3,2] = 1;
						jammingDataSide[2,3] = 1;
					}
					if(PlayData.GetInstance().stageNo == 2)
					{
						jammingDataLength[0,1] = 1;
						jammingDataLength[0,2] = 1;
						jammingDataLength[0,3] = 1;
						jammingDataLength[0,4] = 1;
						jammingDataLength[3,1] = 1;
						jammingDataLength[3,2] = 1;
						jammingDataLength[3,3] = 1;
						jammingDataLength[3,4] = 1;
						jammingDataLength[4,1] = 1;
						jammingDataLength[4,2] = 1;
						jammingDataLength[4,3] = 1;
						jammingDataLength[4,4] = 1;
						jammingDataLength[7,1] = 1;
						jammingDataLength[7,2] = 1;
						jammingDataLength[7,3] = 1;
						jammingDataLength[7,4] = 1;
						jammingDataSide[1,0] = 1;
						jammingDataSide[2,0] = 1;
						jammingDataSide[3,0] = 1;
						jammingDataSide[5,0] = 1;
						jammingDataSide[6,0] = 1;
						jammingDataSide[7,0] = 1;
						jammingDataSide[1,4] = 1;
						jammingDataSide[2,4] = 1;
						jammingDataSide[3,4] = 1;
						jammingDataSide[5,4] = 1;
						jammingDataSide[6,4] = 1;
						jammingDataSide[7,4] = 1;
					}
					if(PlayData.GetInstance().stageNo == 4)
					{
						jammingDataSide[0,3] = 1;
						jammingDataSide[1,3] = 1;
						jammingDataSide[2,3] = 1;
						jammingDataSide[3,3] = 1;
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
		}
	}
}

