using System;
using System.IO;
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
	public class SelectButton
	{
		Input input = Input.GetInstance();
		private MusicEffect musicEffect;
		public Sprite2D[] selectBtnSp = new Sprite2D[10];		// スプライト
		public Texture2D[] selectBtnTex = new Texture2D[10];	// テクスチャ
		const int SELECT_BTN_SIZE = 128;						// 画像サイズ
		public const int STAGE_NUM = 9;							// 総ステージ数
		
		public int selectBtnNo;									// 選択されているステージNo.
		public bool selectMoveFlg;								// 選択されてるステージが変わったとき
		public int changeDirection;								// 動く向き
		
		public float btnMoveY;									// ボタンの動き
		public float btnMoveX;									// ボタンの動き
		
		// コンストラクタ
		public SelectButton ()
		{
			Init();
		}
		
		// 初期化
		private void Init()
		{
			selectBtnNo = 1;									// 選択No.
			btnMoveX = 0;										// 動き
			btnMoveY = 0;										// 動き
			selectMoveFlg = false;								// 動くフラグ
			changeDirection = 2;								// 動く向き(左=0,右=1,停止=2)
			
			// 画像
			for(int i = 0;i <= STAGE_NUM;i++)
			{
				selectBtnTex[i] = new Texture2D(@"/Application/assets/img/number_"+ i +".png", false);
				selectBtnSp[i] = new Sprite2D(selectBtnTex[i]);
			}
			for(int i = 1;i<STAGE_NUM;i++)
			{
				selectBtnSp[i + 1].pos = new Vector3(AppMain.ScreenWidth/2.0f,
				                                 AppMain.ScreenHeight/2.0f,
				                                 0.0f);
			}
			
			// 最初に選ばれているボタン
			selectBtnSp[1].size = new Vector2(1.32f,1.32f);
			selectBtnSp[1].pos = new Vector3(AppMain.ScreenWidth / 2.0f,
				                             AppMain.ScreenHeight / 2.0f,
				                             0.0f);
			
			musicEffect = new MusicEffect(@"/Application/assets/se/Select_SE.wav");
		}
		
		// 更新
		public void Update()
		{
			
			// とりあえずキーを押したら
			// 右キー
			if(selectBtnNo != STAGE_NUM && selectMoveFlg == false)
			{
				if(input.right.isPush)
				{
					musicEffect.Set(0.6f,false);
					changeDirection = 1;
					selectMoveFlg = true;
					
					selectBtnNo++;
					
				}
			}
			
			// 左キー
			if(selectBtnNo != 1 && selectMoveFlg == false)
			{
				if(input.left.isPush)
				{
					musicEffect.Set(0.6f,false);
					changeDirection = 0;
					selectMoveFlg = true;
					
					selectBtnNo--;
					
				}
			}
			
			// ボタンの動き
			// 画像は右に動くやで
			if(selectMoveFlg == true && changeDirection == 0)
			{
				btnMoveX += 16;
				selectBtnSp[selectBtnNo].size += 0.02f;				// 選ばれてるボタンは表示が大きくなる
				selectBtnSp[selectBtnNo+1].size -= 0.02f;
				
				if(btnMoveX >= -(SELECT_BTN_SIZE * 3.0f * (selectBtnNo-1)))
				{
					selectMoveFlg = false;
					changeDirection = 2;
				}
			}

			// 画像は左に動くやで
			if(selectMoveFlg == true && changeDirection == 1)
			{
				btnMoveX -= 16;
				selectBtnSp[selectBtnNo].size += 0.02f;				// 選ばれてるボタンは表示が大きくなる
				selectBtnSp[selectBtnNo-1].size -= 0.02f;
				
				if( btnMoveX <= -(SELECT_BTN_SIZE * 3.0f * (selectBtnNo-1)))
				{
					selectMoveFlg = false;
					changeDirection = 2;
				}
			}
			
			// ボタン配置
			for(int i = 1;i<=STAGE_NUM;i++)
			{
				selectBtnSp[i].pos.X = btnMoveX + AppMain.ScreenWidth/2.0f + (SELECT_BTN_SIZE) * 3.0f * (i-1);
			}
			
			Console.WriteLine(selectBtnNo);
			
		}
		
		// 描画
		public void Draw()
		{
			for(int i = 1;i<=STAGE_NUM;i++)
			{
				selectBtnSp[i].Draw();
			}
		}
		
		// 開放
		public void Term()
		{
			for(int i = 0;i<10;i++)
			{
				selectBtnTex[i].Dispose();
			}
		}
	}
}

