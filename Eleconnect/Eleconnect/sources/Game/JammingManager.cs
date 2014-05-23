using System;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class JammingManager
	{
		// ジャミング
		public int[,] jammingNum;							// ジャミング数の合計
		public int[,] jammingData;							// ジャミング配置データ
		int jammingX;										// ジャミング個数(横)
		int jammingY;										// ジャミング個数(縦)
		float panelSize;
		Vector2 basePos;
		public Vector3 jammingLocate;
		const float changeAngle =  90.0f;
		
		Jamming jamming;
		
		// コンストラクタ
		public JammingManager (int indexX,int indexY)
		{
			Init(indexX,indexY);
		}
		
		private void Init(int indexX,int indexY)
		{
			jamming = new Jamming();
			panelSize = Panel.SIZE * Panel.SCALE + 5.0f;
			basePos = new Vector2(AppMain.ScreenCenter.X - ((panelSize * GameScene.stageWidth) / 2.0f) + panelSize,
			                              AppMain.ScreenCenter.Y - ((panelSize * GameScene.stageHeight) / 2.0f) + panelSize);
			jammingNum = new int[indexX,indexY];
			jammingData = new int[indexX,indexY];
			jammingX = GameScene.stageWidth-1;
			jammingY = GameScene.stageHeight*2-1;
			jammingLocate = new Vector3(basePos.X + panelSize,
			                            basePos.Y + panelSize,
			                            0.0f);
			
			// ジャミング配置場所の配列を1に
			for(int i = 0;i < jammingX;i++)
			{
				for(int j = 0;j < jammingY;j++)
				{
					if(i % 2 == 0 && j % 2 == 1)
					{
						jammingData[i,j] = 1;
					}
				}
			}
			JammingSet(indexX,indexY);							// 縦横をセット
		}
		
		// 横向きなら1、縦向きなら0を入れる
		public void JammingSet(int indexX,int indexY)
		{
			for(int i = 0;i < jammingX;i++)
			{
				for(int j = 0;j < jammingY;j++)
				{
					if(jammingData[i,j] == 1)
					{
						jammingNum[i,j] = 1;
					}
					else
					{
						jammingNum[i,j] = 0;
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
			for(int i = 0;i < jammingX;i++)
			{
				for(int j = 0;j < jammingY;j++)
				{
					// 縦向き
					if(jammingNum[i,j] == 0)
					{
						jammingLocate = new Vector3(basePos.X + panelSize * i,
			                            		basePos.Y-panelSize/2 + panelSize * (j/2),
			                            		0.0f);
						jamming.Draw(jammingLocate,0.0f);
					}
					
					// 横向き
					if(jammingNum[i,j] == 1)
					{ 
						jammingLocate = new Vector3(basePos.X-panelSize/2 + panelSize * i,
			                            		basePos.Y + panelSize * (j/2),
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

