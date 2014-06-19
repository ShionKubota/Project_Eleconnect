//--------------------------------------------------------------
// クラス名： AppMain.cs
// コメント： ゲームメインクラス
// 製作者　： ShionKubota
// 制作日時： 2014/02/28
//--------------------------------------------------------------
using System ;
using System.Threading ;
using Sce.PlayStation.Core ;
using Sce.PlayStation.Core.Graphics ;
using Sce.PlayStation.Core.Environment ;
using Sce.PlayStation.HighLevel.UI;
using System.Collections.Generic;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Input;

namespace Eleconnect
{
	class AppMain
	{
		public static GraphicsContext graphics ;
		
		// 画面幅
		//public const int ScreenWidth = 960;
		//public const int ScreenHeight = 544;
		//public static readonly Vector3 ScreenCenter = new Vector3(ScreenWidth / 2.0f, ScreenHeight / 2.0f, 0.0f);
		public static int ScreenWidth;
		public static int ScreenHeight;
		public static Vector3 ScreenCenter;
		
		static ImageRect rectScreen;
		
		// シーンマネージャ
		public static SceneManager sceneManager;
		
		
		// Music
		public const float BGM_VOLUME = 0.25f;			// BGMの音量
		
		// エントリーポイント
		static void Main( string[] args )
		{
			Console.WriteLine("GAME_START");
			Init() ; // 初期化
			while ( true )
			{
				SystemEvents.CheckEvents() ;
				Update();
				Draw();
			}
			Term() ;	// 解放
		}
		
		static void Init()
		{
			graphics = new GraphicsContext(960, 544, PixelFormat.None, PixelFormat.None, MultiSampleMode.None) ;
			
			// 画面サイズ取得
			ScreenWidth = graphics.Screen.Width;
			ScreenHeight = graphics.Screen.Height;
			ScreenCenter = new Vector3(ScreenWidth / 2.0f, ScreenHeight / 2.0f, 0.0f);
			
			UISystem.Initialize(graphics);
			UISystem.EnabledFocus = false;
			
			rectScreen = graphics.Screen.Rectangle;
			sceneManager = SceneManager.GetInstance();
		}
		
		// 更新
		static void Update()
		{
			// 入力データ取得
			Input.GetInstance().Update();
			
			UISystem.Update(Input.touchDataList, ref Input.gamePadData);
			sceneManager.Update();
		}
		
		// 描画
		static void Draw()
		{
			graphics.SetClearColor( new Vector4(0.0f, 0.0f, 0.0f, 1.0f)) ;
			graphics.Clear() ;
			
			graphics.Enable( EnableMode.Blend ) ;
			graphics.SetBlendFunc( BlendFuncMode.Add, BlendFuncFactor.SrcAlpha, BlendFuncFactor.OneMinusSrcAlpha ) ;
			graphics.Enable( EnableMode.CullFace ) ;
			graphics.SetCullFace( CullFaceMode.Back, CullFaceDirection.Ccw ) ;
			graphics.Enable( EnableMode.DepthTest ) ;
			graphics.SetDepthFunc( DepthFuncMode.LEqual, true ) ; // zId値適用
			
			sceneManager.Draw();	// UISystemの描画はsceneManager内で行う
			
			// draw 2D
			graphics.Disable( EnableMode.CullFace ) ;
			graphics.Disable( EnableMode.DepthTest ) ;
			graphics.Disable( EnableMode.Blend ) ;
			graphics.SwapBuffers() ;
		}
		
		// 開放
		static void Term() {
			sceneManager.Term();
			graphics.Dispose() ;
			UISystem.Terminate();
		}
	}
}
