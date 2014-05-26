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
		private Sprite2D[] selectBtnSp = new Sprite2D[10];		// スプライト
		private Texture2D[] selectBtnTex = new Texture2D[10];	// テクスチャ
		private Sprite2D[] prevGameSp = new Sprite2D[10];		// ステージサンプルスプライト
		private Texture2D[] prevGameTex = new Texture2D[10];		// ステージサンプルテクスチャ
		private Sprite2D[] cornerSp = new Sprite2D[4];
		private Texture2D[] cornerTex = new Texture2D[4];
		const int SELECT_BTN_SIZE = 128;						// 画像サイズ
		public const int STAGE_NUM = 9;							// 総ステージ数
		
		public int selectBtnNo;									// 選択されているステージNo.
		int selectPrevNum;
		bool selectMoveFlg;										// 選択されてるステージが変わったとき
		bool changePrevGame;									// ゲームプレビューの変更
		int changeDirection;									// 動く向き
		
		float btnMoveY;											// ボタンの動き
		float btnMoveX;											// ボタンの動き
		
		float cornerSize = 90.0f;								// 枠の大きさ
		
		// コンストラクタ
		public SelectButton ()
		{
			Init();
		}
		
		// 初期化
		private void Init()
		{
			selectBtnNo = 1;									// 選択No.
			selectPrevNum = 1;
			btnMoveX = 0;										// 動き
			btnMoveY = 0;										// 動き
			selectMoveFlg = false;								// 動くフラグ
			changeDirection = 2;								// 動く向き(左=0,右=1,停止=2)
			changePrevGame = false;								// ゲームプレビュー
			
			// 画像
			for(int i = 0;i <= STAGE_NUM;i++)
			{
				selectBtnTex[i] = new Texture2D(@"/Application/assets/img/number_"+ i +".png", false);
				selectBtnSp[i] = new Sprite2D(selectBtnTex[i]);
				
				prevGameTex[i] = new Texture2D(@"/Application/assets/img/test.png",false);
				prevGameSp[i] = new Sprite2D(prevGameTex[i]);
				prevGameSp[i].pos = new Vector3(AppMain.ScreenWidth/2,AppMain.ScreenHeight/2,0.0f);
				prevGameSp[i].size = new Vector2(0.8f,0.8f);
			}
			for(int i = 1;i<STAGE_NUM;i++)
			{
				selectBtnSp[i + 1].pos = new Vector3(AppMain.ScreenWidth/2.0f,
				                                 AppMain.ScreenHeight/2.0f,
				                                 0.0f);
				prevGameSp[i+1].color.W = 0.0f;
			}
			for(int i = 0; i < 4;i++)
			{
				cornerTex[i] = new Texture2D(@"/Application/assets/img/corner.png",false);
				cornerSp[i] = new Sprite2D(cornerTex[i]);
				cornerSp[i].size = new Vector2(1.0f);
				
				cornerSp[i].angle = 90 * i;
			}
			
			// 最初に選ばれているボタン
			selectBtnSp[1].size = new Vector2(1.48f,1.48f);
			selectBtnSp[1].pos = new Vector3(AppMain.ScreenWidth / 2.0f,
				                             AppMain.ScreenHeight / 2.0f,
				                             0.0f);
			prevGameSp[1].color.W = 0.6f;
		
			musicEffect = new MusicEffect(@"/Application/assets/se/Select_SE.wav");
		}
		
		// 更新
		public void Update()
		{
			// ボタン類更新
			Button_Move();
			
			// 座標更新
			cornerSp[0].pos = new Vector3(AppMain.ScreenWidth / 2.0f - cornerSize,
				                              AppMain.ScreenHeight / 2.0f - cornerSize,
				                              0.0f);
			cornerSp[1].pos = new Vector3(AppMain.ScreenWidth / 2.0f + cornerSize,
			                              AppMain.ScreenHeight / 2.0f - cornerSize,
			                              0.0f);
			cornerSp[2].pos = new Vector3(AppMain.ScreenWidth / 2.0f + cornerSize,
			                              AppMain.ScreenHeight / 2.0f + cornerSize,
			                              0.0f);
			cornerSp[3].pos = new Vector3(AppMain.ScreenWidth / 2.0f - cornerSize,
			                              AppMain.ScreenHeight / 2.0f + cornerSize,
			                              0.0f);
			
			// ボタン配置
			for(int i = 1;i<=STAGE_NUM;i++)
			{
				selectBtnSp[i].pos.X = btnMoveX + AppMain.ScreenWidth/2.0f + (SELECT_BTN_SIZE) * 3.0f * (i-1);
			}
			
			//Console.WriteLine(prevGameSp[selectPrevNum].color.W);
			//Console.WriteLine(selectBtnNo);
			//Console.WriteLine(changePrevGame);
		}
		
		// 描画
		public void Draw()
		{
			prevGameSp[selectPrevNum].Draw();
			for(int i = 1;i<=STAGE_NUM;i++)
			{
				selectBtnSp[i].Draw();
			}
			for(int i = 0; i < 4;i++)
			{
				cornerSp[i].Draw();
			}
		}
		
		// 開放
		public void Term()
		{
			for(int i = 0;i<10;i++)
			{
				prevGameTex[i].Dispose();
				selectBtnTex[i].Dispose();
			}
		}
		
		// ボタン類の動き
		public void Button_Move()
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
				
				// 後ろの画像のフェード
				if(changePrevGame == false)
				{
					prevGameSp[selectBtnNo+1].color.W -= 0.05f;
					cornerSize -= 5.0f;
				}
				
				if(prevGameSp[selectBtnNo+1].color.W <= 0 && changePrevGame == false)
				{
					selectPrevNum--;
					changePrevGame = true;
				}
				
				if(btnMoveX >= -(SELECT_BTN_SIZE * 3.0f * (selectBtnNo-1)))
				{
					selectMoveFlg = false;
					changePrevGame = false;
					prevGameSp[selectBtnNo].color.W = 0.6f;
					changeDirection = 2;
				}
			}

			// 画像は左に動くやで
			if(selectMoveFlg == true && changeDirection == 1)
			{
				btnMoveX -= 16;
				selectBtnSp[selectBtnNo].size += 0.02f;				// 選ばれてるボタンは表示が大きくなる
				selectBtnSp[selectBtnNo-1].size -= 0.02f;
				
				// 後ろの画像のフェード
				if(changePrevGame == false)
				{
					prevGameSp[selectBtnNo-1].color.W -= 0.05f;
					cornerSize -= 5.0f;
				}
				
				if(prevGameSp[selectBtnNo-1].color.W <= 0 && changePrevGame == false)
				{
					selectPrevNum++;
					changePrevGame = true;
				}
				
				if( btnMoveX <= -(SELECT_BTN_SIZE * 3.0f * (selectBtnNo-1)))
				{
					selectMoveFlg = false;
					changePrevGame = false;
					prevGameSp[selectBtnNo].color.W = 0.6f;
					changeDirection = 2;
				}
			}
			
			// 元に戻す
			if(changePrevGame == true)
			{
				prevGameSp[selectBtnNo].color.W += 0.05f;
				cornerSize += 5.0f;
			}
		}
	}
}

