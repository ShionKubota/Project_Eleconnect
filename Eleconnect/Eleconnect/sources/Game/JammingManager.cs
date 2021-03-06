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
		int vanishCnt;											// 消滅演出に使うフレームカウンタ
		bool visible;											// 表示するか否か
		
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
			panelSize = Panel.SIZE * Panel.SCALE;// + 5.0f;
			basePos = new Vector2(16.0f*14+panelSize/2,
			                      AppMain.ScreenCenter.Y - ((panelSize * GameScene.stage.height) / 2.0f) + (panelSize));
			jammingDataLength = new int[jammingSide,jammingLength];
			jammingDataSide = new int[jammingSide,jammingLength];
			jammingLocate = new Vector3(basePos.X + panelSize,
			                            basePos.Y + panelSize,
			                            0.0f);
			vanishCnt = 0;
			visible = true;
			JammingSet();
		}
		
		public void JammingSet()
		{
			if(PanelEditor.IS_RANDOM_MAP) return;
			
			// ジャミング配置場所の数値は1
			for(int i = 0;i < jammingSide;i++)
			{
				for(int j = 0;j < jammingLength;j++)
				{
					if(GameScene.stage.jamming == 0)
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
					if(GameScene.stage.jamming == 1)
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
					if(GameScene.stage.jamming == 2)
					{
						jammingDataLength[2,0] = 1;
						jammingDataLength[6,1] = 1;
						jammingDataLength[6,2] = 1;
						jammingDataLength[3,3] = 1;
						jammingDataLength[3,4] = 1;
						jammingDataSide[0,0] = 1;
						jammingDataSide[3,0] = 1;
						jammingDataSide[4,0] = 1;
						jammingDataSide[5,0] = 1;
						jammingDataSide[6,0] = 1;
						jammingDataSide[4,2] = 1;
						jammingDataSide[5,2] = 1;
						jammingDataSide[6,2] = 1;
					}
					
					if(GameScene.stage.jamming == 4)
					{
						jammingDataLength[3, 2] = 1;
						jammingDataLength[4, 2] = 1;
						jammingDataSide[4, 1] = 1;
						jammingDataSide[4, 2] = 1;
						/*
						jammingDataSide[0,3] = 1;
						jammingDataSide[1,3] = 1;
						jammingDataSide[2,3] = 1;
						jammingDataSide[3,3] = 1;
						*/
					}
				}
			}
		}
		
		// 更新
		public void Update()
		{
			jamming.Update();
			
			// 消滅演出
			if(!JammingSwitch.isJamming)
			{
				vanishCnt++;
				if(vanishCnt % 5 == 0)
					visible = (visible == true || vanishCnt > 60) ? false : true;
			}
			else
			{
				visible = true;
				vanishCnt = 0;
			}
			
			
		}
		
		// 描画
		public void Draw()
		{
			if(visible == false) return;	// 非表示
			
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
						jammingLocate = new Vector3(basePos.X- panelSize /2 + panelSize * i,
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

