using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class MenuManager
	{
		// 画像
		private Texture2D
			menubaseTex,
			backmojiTex,
			exitmojiTex,
			pausemojiTex,
			retrymojiTex,
			selectmojiTex,
			titlemojiTex;
		private Sprite2D[] menuSp = new Sprite2D[5];		// ボタン[数]
		private Sprite2D menubaseSp;
		
		public static bool menuFlg;							// メニューを表示
		public int menuNum;								// 選んでいるメニュー番号
		
		private float frameCnt;
		
		Input input = Input.GetInstance();
		
		// コンストラクタ
		public MenuManager ()
		{
			Init();
		}
		
		// 初期化
		private void Init()
		{
			// 画像
			menubaseTex = new Texture2D(@"/Application/assets/img/menuimg/menubase.png", false);
			backmojiTex = new Texture2D(@"/Application/assets/img/menuimg/backmoji.png", false);
			pausemojiTex = new Texture2D(@"/Application/assets/img/menuimg/pausemoji.png", false);
			retrymojiTex = new Texture2D(@"/Application/assets/img/menuimg/retrymoji.png", false);
			selectmojiTex = new Texture2D(@"/Application/assets/img/menuimg/selectmoji.png", false);
			titlemojiTex = new Texture2D(@"/Application/assets/img/menuimg/titlemoji.png", false);
			menubaseSp = new Sprite2D(menubaseTex);
			menuSp[0] = new Sprite2D(pausemojiTex);
			menuSp[1] = new Sprite2D(backmojiTex);
			menuSp[2] = new Sprite2D(retrymojiTex);
			menuSp[3] = new Sprite2D(selectmojiTex);
			menuSp[4] = new Sprite2D(titlemojiTex);
			for(int i = 0;i<5;i++)
			{
				menuSp[i].size = new Vector2(1.0f);
				menuSp[i].pos = new Vector3(AppMain.ScreenWidth/2.0f,AppMain.ScreenHeight/2.0f + 65.0f * i - 128.0f,0.0f);
			}
			menubaseSp.size = new Vector2(1.0f);
			menubaseSp.pos = new Vector3(AppMain.ScreenWidth / 2.0f,AppMain.ScreenHeight / 2.0f,0.0f);
			menubaseSp.color = new Vector4(1.0f,1.0f,1.0f,0.5f);
			
			menuFlg = false;
			menuNum = 1;
			
			frameCnt = 0;
		}
		
		// 更新
		public void Update()
		{
			frameCnt++;
			
			// 上下キーで選択
			if(input.up.isPushStart && menuNum != 1)
			{
				menuNum--;
			}
			else if(input.down.isPushStart && menuNum != 5)
			{
				menuNum++;
			}
			
			// 選ばれているボタンは点滅
			menuSp[menuNum].color.W = 0.3f + 
						 FMath.Sin(FMath.Radians( frameCnt* 10)) * 0.6f;
			
			// 選ばれてなかったら戻るよ
			for(int i = 1;i < 5;i++)
			{
				if(i != menuNum)
				menuSp[i].color.W = 1.0f;
			}
			
			// ボタン押したとき
			MenuFunction();
		}
		
		// 描画
		public void Draw()
		{
			// Startが押されたらメニューON/OFF
			if(input.start.isPushEnd && menuFlg == false)
			{
				menuFlg = true;
			}
			else if(input.start.isPushEnd && menuFlg == true)
			{
				menuFlg = false;
			}
			
			// メニュー表示
			if(menuFlg == true)
			{
				menubaseSp.Draw();
				for(int i = 0;i<5;i++)
				{
					menuSp[i].Draw();
				}
			}
		}
		
		// 解放
		public void Term()
		{
			menubaseTex.Dispose();
			pausemojiTex.Dispose();
			backmojiTex.Dispose();
			retrymojiTex.Dispose();
			selectmojiTex.Dispose();
			titlemojiTex.Dispose();
		}
		
		// ボタン押したときの処理
		void MenuFunction()
		{
			if(input.circle.isPushStart)
			{
				switch(menuNum)
				{
					case 1:			// backボタン
					{
						menuFlg = false;
						break;
					}
					case 2:			// retryボタン
					{
						AppMain.sceneManager.Switch(SceneId.GAME);
						break;
					}
					case 3:			// selectボタン
					{
						AppMain.sceneManager.Switch(SceneId.SELECT);
						break;
					}
					case 4:			// titleボタン
					{
						AppMain.sceneManager.Switch(SceneId.TITLE);
					break;
					}
				}
			}
		}	
	}
}