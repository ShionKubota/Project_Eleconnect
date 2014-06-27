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
		private Sprite2D[] selectBtnSp = new Sprite2D[11];		// スプライト
		private static Texture2D[] selectBtnTex = new Texture2D[11];	// テクスチャ
		private Sprite2D[] cornerSp = new Sprite2D[4];
		private static Texture2D[] cornerTex = new Texture2D[4];
		const int SELECT_BTN_SIZE = 128;						// 画像サイズ
		public const int STAGE_NUM = 9;							// 総ステージ数
		
		public int selectBtnNo;									// 選択されているステージNo.
		public bool selectMoveFlg;						// 選択されてるステージが変わったとき
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
			selectBtnNo = (PlayData.GetInstance().stageNo >= 0) ? PlayData.GetInstance().stageNo+1 : 1;	// 選択No.
			btnMoveX = 0;										// 動き
			btnMoveY = 0;										// 動き
			selectMoveFlg = false;								// 動くフラグ
			changeDirection = 2;								// 動く向き(左=0,右=1,停止=2)
			changePrevGame = false;								// ゲーム選択
			
			// 画像
			for(int i = 1;i <= STAGE_NUM;i++)
			{
				if(selectBtnTex[i] == null)
				{
					selectBtnTex[i] = new Texture2D(@"/Application/assets/img/number_"+ i +".png", false);
				}
				
				selectBtnSp[i] = new Sprite2D(selectBtnTex[i]);
				selectBtnSp[i].size = new Vector2(1.0f);
			}
			
			for(int i = 1;i<STAGE_NUM;i++)
			{
				selectBtnSp[i + 1].pos = new Vector3(AppMain.ScreenWidth/2.0f + (SELECT_BTN_SIZE) * 3.0f * i,
				                                 AppMain.ScreenHeight/2.0f + SELECT_BTN_SIZE/2,
				                                 0.0f);
			}
			
			for(int i = 0; i < 4;i++)
			{
				if(cornerTex[i] == null)
				{
					cornerTex[i] = new Texture2D(@"/Application/assets/img/corner.png",false);
				}
				cornerSp[i] = new Sprite2D(cornerTex[i]);
				cornerSp[i].size = new Vector2(1.0f);
				
				cornerSp[i].angle = 90 * i;
			}
			cornerSp[0].pos = new Vector3(AppMain.ScreenWidth / 2.0f - cornerSize,
			                              AppMain.ScreenHeight / 2.0f - cornerSize + SELECT_BTN_SIZE/2,
			                              0.0f);
			cornerSp[1].pos = new Vector3(AppMain.ScreenWidth / 2.0f + cornerSize,
			                              AppMain.ScreenHeight / 2.0f - cornerSize + SELECT_BTN_SIZE/2,
			                              0.0f);
			cornerSp[2].pos = new Vector3(AppMain.ScreenWidth / 2.0f + cornerSize,
			                              AppMain.ScreenHeight / 2.0f + cornerSize + SELECT_BTN_SIZE/2,
			                              0.0f);
			cornerSp[3].pos = new Vector3(AppMain.ScreenWidth / 2.0f - cornerSize,
			                              AppMain.ScreenHeight / 2.0f + cornerSize + SELECT_BTN_SIZE/2,
			                              0.0f);
			
			// 最初に選ばれているボタン
			selectBtnSp[selectBtnNo].size = new Vector2(1.48f,1.48f);
			selectBtnSp[selectBtnNo].pos = new Vector3(AppMain.ScreenWidth / 2.0f,
				                             AppMain.ScreenHeight / 2.0f + SELECT_BTN_SIZE/2,
				                             0.0f);
			
			musicEffect = new MusicEffect(@"/Application/assets/se/Select_SE.wav");
		}
		
		// 更新
		public void Update()
		{
			Console.WriteLine(selectBtnSp[selectBtnNo].pos.Y);
			if(!MenuManager.menuFlg)
			{
				// ボタン類更新
				Button_Move();
				
				// 枠座標更新
				cornerSp[0].pos = new Vector3(AppMain.ScreenWidth / 2.0f - cornerSize,
				                              AppMain.ScreenHeight / 2.0f - cornerSize + SELECT_BTN_SIZE/2,
				                              0.0f);
				cornerSp[1].pos = new Vector3(AppMain.ScreenWidth / 2.0f + cornerSize,
				                              AppMain.ScreenHeight / 2.0f - cornerSize + SELECT_BTN_SIZE/2,
				                              0.0f);
				cornerSp[2].pos = new Vector3(AppMain.ScreenWidth / 2.0f + cornerSize,
				                              AppMain.ScreenHeight / 2.0f + cornerSize + SELECT_BTN_SIZE/2,
				                              0.0f);
				cornerSp[3].pos = new Vector3(AppMain.ScreenWidth / 2.0f - cornerSize,
				                              AppMain.ScreenHeight / 2.0f + cornerSize + SELECT_BTN_SIZE/2,
				                              0.0f);
				
				// ボタン配置
				for(int i = 1;i<=STAGE_NUM;i++)
				{
					selectBtnSp[i].pos.X = btnMoveX + AppMain.ScreenWidth/2.0f + (SELECT_BTN_SIZE) * 3.0f * (i-1);
				}
			}
			else
			{
				menu_Button_Move();
				// ボタン配置
				for(int i = 1;i<=STAGE_NUM;i++)
				{
					selectBtnSp[i].pos = new Vector3(btnMoveX + AppMain.ScreenWidth/2.0f,AppMain.ScreenHeight / 2.0f + SELECT_BTN_SIZE/2,0.0f);
				}
			}
		}
		
		// 描画
		public void Draw()
		{
			if(MenuManager.menuFlg == false)
			{
				for(int i = 1;i<=STAGE_NUM;i++)
				{
					selectBtnSp[i].Draw();
				}
				for(int i = 0; i < 4;i++)
				{
					cornerSp[i].Draw();
				}
			}
			else
			{
				for(int i = 1;i<=STAGE_NUM;i++)
				{
					cornerSp[0].angle = -45.0f;
					cornerSp[1].angle = -225.0f;
					cornerSp[0].pos = new Vector3 (selectBtnSp[i].pos.X - SELECT_BTN_SIZE,selectBtnSp[i].pos.Y,0.0f);
					cornerSp[1].pos = new Vector3 (selectBtnSp[i].pos.X + SELECT_BTN_SIZE,selectBtnSp[i].pos.Y,0.0f);
					selectBtnSp[i].size = new Vector2 (1.48f);
				}
				
				selectBtnSp[selectBtnNo].Draw();
				if(selectBtnNo != 1)
				{
					cornerSp[0].Draw();
				}
				if(selectBtnNo != STAGE_NUM)
				{
					cornerSp[1].Draw();
				}
			}
		}
		
		// 開放
		public void Term()
		{
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
					cornerSize -= 5.0f;
				}
				
				if(btnMoveX % 192 == 0   && changePrevGame == false)
				{
					changePrevGame = true;
				}
				
				if(btnMoveX >= -(SELECT_BTN_SIZE * 3.0f * (selectBtnNo-1)))
				{
					selectMoveFlg = false;
					changePrevGame = false;
					changeDirection = 2;
				}
			}

			// 画像は左に動くやで
			if(selectMoveFlg == true && changeDirection == 1)
			{
				btnMoveX -= 16;
				selectBtnSp[selectBtnNo].size += 0.02f;				// 選ばれてるボタンは表示が大きくなる
				selectBtnSp[selectBtnNo-1].size -= 0.02f;
				
				// 枠の移動
				if(changePrevGame == false)
				{
					cornerSize -= 5.0f;
				}
				
				if(btnMoveX % 192 == 0 && changePrevGame == false)
				{
					changePrevGame = true;
				}
				
				if( btnMoveX <= -(SELECT_BTN_SIZE * 3.0f * (selectBtnNo-1)))
				{
					selectMoveFlg = false;
					changePrevGame = false;
					changeDirection = 2;
				}
			}
			
			// 枠を元の位置に
			if(changePrevGame == true)
			{
				cornerSize += 5.0f;
			}
		}
		
		public void menu_Button_Move()
		{
			// とりあえずキーを押したら
			// 右キー
			if(selectBtnNo != STAGE_NUM)
			{
				if(input.right.isPushEnd)
				{
					musicEffect.Set(0.6f,false);
					
					selectBtnNo++;
				}
			}
			
			// 左キー
			if(selectBtnNo != 1)
			{
				if(input.left.isPushEnd)
				{
					musicEffect.Set(0.6f,false);
					
					selectBtnNo--;
				}
			}
		}
	}
}

