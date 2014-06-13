//--------------------------------------------------------------
// クラス名： GameScene.cs
// コメント： ゲームシーンクラス
// 製作者　： ShionKubota
// 制作日時： 2014/02/28
// 更新日時： 2014/02/28
//--------------------------------------------------------------
using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.UI;

namespace Eleconnect
{
	public class GameScene : BaseScene
	{
		// 主要オブジェクト
		public static GameUI gameUI;
		public static PanelManager panelManager{ protected set; get; }
		protected JammingManager jammingManager;
		protected CursorOnPanels cursor;
		protected MenuManager menuManager;
		protected ElecthManager electhManager;
		// 画像
		private Sprite2D backSp;
		private Texture2D backTex;
		private Sprite2D guideSp;
		private Texture2D guideTex;
		// 音楽
		protected static MusicEffect musicEffect;
		protected static Music music;
		protected static TimeManager timeManager;
		
		// ステージ情報
		public static int stageWidth{ protected set; get; }			// 現ステージのパネルの枚数　横
		public static int stageHeight{ protected set; get; }			//      〃                縦
		public static int[] mapData{ protected set; get; }		// マップデータ
		public static int[] stageData{ protected set; get; }		// ステージデータ
		public static string mapFileName{ protected set; get; }		// 読み込むマップファイル名
		public static string stageFileName{ protected set; get; }		// 読み込むステージ情報ファイル名
		
		// プレイ情報
		private int repeaterCnt;	// アイテム使用カウント
		private int changeCnt;
		private int frameCnt;
		
		// ゲームの状態ID列挙
		public enum StateId
		{
			GAME,
			PAUSE,
			FLOW_ELECTH,
			CLEAR
		}
		public StateId nowState{ private set; get; }
		
		// デバッグ用
		public const bool IS_DEBUG_MODE = true;		// デバッグモード...パネル情報の開示、変更
		
		// コンストラクタ
		public GameScene ()
		{
			Init ();
		}
		
		// 初期化
		override public void Init()
		{
			// 読み込むファイルを選択
			LoadStageData();
			
			stageWidth = stageData[0];
			stageHeight = stageData[1];
			
			panelManager = new PanelManager();
			
			CommonInit();
			
			cursor = new CursorOnPanels(panelManager);
			
			// デバッグ表示
			//if(IS_DEBUG_MODE) Console.WriteLine("IS_DEBUG_MODE...Xkey : Output the data of this panel.\tD/Wkey : Increment/Decrement the elecPowMax.");
			//if(IS_EDIT_MODE) Console.WriteLine("IS_DEBUG_MODE...Zkey : Save the data of this map.");
		}
		
		// 子と共通の初期化処理
		protected void CommonInit()
		{
			// スプライト
			backTex = new Texture2D(@"/Application/assets/img/eleconnect_background01.png", false);
			backSp = new Sprite2D(backTex);
			backSp.pos = AppMain.ScreenCenter;
			backSp.color = new Vector4(0.6f, 0.6f, 0.6f, 1.0f);
			
			guideTex = new Texture2D(@"/Application/assets/img/guid.png", false);
			guideSp = new Sprite2D(guideTex);
			guideSp.pos = new Vector3(AppMain.ScreenWidth - 130.0f,
			                          AppMain.ScreenHeight / 2.0f + 50.0f,
			                          0.0f);
			guideSp.size = new Vector2(0.4f, 0.4f);
			
			// インスタンス生成
			gameUI = new GameUI();
			timeManager = new TimeManager();
			menuManager = new MenuManager();
			jammingManager = new JammingManager();
			electhManager = new ElecthManager(panelManager, jammingManager);
			
			// パラメータ初期化
			repeaterCnt = 0;
			changeCnt = 0;
			frameCnt = 0;
			nowState = StateId.GAME;
			
			// 音関連
			music = new Music(@"/Application/assets/music/Game_Music.mp3");
			musicEffect = new MusicEffect(@"/Application/assets/se/Rotate_SE.wav");
			music.Set(true,0.4f,0.0d);
		}
		
		// ステージのデータを読み込み
		protected void LoadStageData()
		{
			Stage_Base stage;
			stage = new StageDataList()[PlayData.GetInstance().stageNo];
			
			// マップ読み込み
			mapData = stage.mapData;
			
			// ステージ情報読み込み
			stageData = stage.stageData;
		}
		
		// 更新
		override public void Update()
		{
			try{panelManager.ToString();}
			catch(NullReferenceException e)
			{
				panelManager = new PanelEditor();
			}
			
			switch(nowState)
			{
			case StateId.GAME:
				GamingProcess();
				break;
				
			case StateId.FLOW_ELECTH:
				FrowingProcess();
				break;
				
			case StateId.CLEAR:
				AfterClearingProcess();
				break;
				
			case StateId.PAUSE:
				PausingProcess();
				break;
			}
			
			// パネルの更新
			panelManager.Update();
			jammingManager.Update();
			
			frameCnt++;
		}
		
		// ゲーム中の更新プロセス
		protected void GamingProcess()
		{
			// カーソルの更新
			cursor.Update(panelManager);
			
			// タイムの更新
			timeManager.Update();
			
			// 操作を受付け＆処理
			AcceptPlayerInput();
			
			// エレクス更新
			electhManager.Update();
			if(frameCnt % 300 == 0)
			{
				//ecthManager.FlowStart(0, 0, false);
			}
		}
		
		// エレクスが流れるプロセス
		protected void FrowingProcess()
		{
			electhManager.Update();
			
			if(!electhManager.nowFlowing)
			{
				nowState = (Electh.arrivedGoal) ? StateId.CLEAR : StateId.GAME;
			}
		}
		
		// クリア後の更新プロセス
		protected void AfterClearingProcess()
		{
			nowState = StateId.GAME;
			return;
			for(int i = 0; i < stageWidth; i++)
			{
				for(int j = 0; j < stageHeight; j++)
				{
					if(panelManager[i, j].sp.size.X > 0.0f)
					{
						//panelManager[i, j].sp.size -= (Panel.SCALE+1.0f - panelManager[i, j].sp.size) * 0.1f;
						//panelManager[i, j].sp.size -= new Vector2(0.01f);
						//Console.WriteLine (panelManager[i, j].sp.size.X);
						panelManager[i, j].sp.pos += (panelManager[i, j].sp.pos - AppMain.ScreenCenter + new Vector3(-1.0f, -1.0f, 0.0f)) * 0.1f;
					}
					else
					{
						panelManager[i, j].sp.size = new Vector2(0.0f);
					}
				}
			}
			// リザルトへ
			//SceneManager.GetInstance().Switch(SceneId.RESULT);
		}
		
		// ポーズ中の更新プロセス
		protected void PausingProcess()
		{
			// ポーズメニューの更新
			menuManager.Update();
			
			// ポーズ終了をチェック
			if(menuManager.isEnd || Input.GetInstance().start.isPushStart)
			{
				nowState = StateId.GAME;
			}
			
		}
		
		// プレイヤーの操作
		protected virtual void AcceptPlayerInput()
		{
			Input input = Input.GetInstance();
			
			int indexW = cursor.indexW,
				indexH = cursor.indexH;
			
			// 選んでいるパネル
			Panel panel = panelManager[indexW, indexH];
			
			// パネルを回転
			if(input.triggerR.isPushStart && panel.isGoal == false)
			{
				panel.ButtonEvent(true);
				musicEffect.Set(1.0f,false);
			}
			if(input.triggerL.isPushStart && panel.isGoal == false)
			{
				panel.ButtonEvent(false);
				musicEffect.Set(1.0f,false);
			}
			
			// エレクスを流す
			if(input.circle.isPushStart)
			{
				frameCnt = 0;
				nowState = StateId.FLOW_ELECTH;
				electhManager.FlowStart(stageData[2], stageData[3], true);
			}
			
			/*
			// アイテムを使用
			// リピーター
			if(input.cross.isPushStart)
			{
				if(repeaterCnt < 1 && !(indexW == stageWidth - 1 && indexH == stageHeight - 1))
				{
					panel.isRepeater = true;
					PanelManager.CheckConnectOfPanels(0, 0);
					repeaterCnt++;
				}
				else	// 使えないことを教えるためにカーソルを赤く(?)する
				{
					cursor.ChangeColorTemporarily(new Vector3(1.0f, 0.5f, 0.5f));
				}
			}
			
			// パネル変化
			if(input.square.isPushStart && panel.typeId != Panel.TypeId.Cross)
			{
				if(changeCnt < 2)
				{
					Panel.TypeId newId = (Panel.TypeId)((int)panel.typeId + 1);
					panel.ChangeType(newId);
					PanelManager.CheckConnectOfPanels(0, 0);
					changeCnt++;
				}
				else
				{
					cursor.ChangeColorTemporarily(new Vector3(1.0f, 0.0f, 0.0f));
				}
			}
			*/
			// ポーズメニューを開く
			if(input.start.isPushStart) nowState = StateId.PAUSE;
			
			// デバッグ機能
			if(GameScene.IS_DEBUG_MODE) DebugProcess(input);
		}
		
		// デバッグ機能
		protected void DebugProcess(Input input)
		{
			int indexW = cursor.indexW,
				indexH = cursor.indexH;
			
			// 選んでいるパネル
			Panel panel = panelManager[indexW, indexH];
			
			// パネルの情報を表示(デバッグ機能)
			if(input.start.isPushStart)
			{
				Console.WriteLine ("TypeId = {0} \nrotateCnt = {1}\n", 
				                   panel.typeId,
				                   panel.rotateCnt);
			}
		}
		
		// 描画
		override public void Draw()
		{
			backSp.Draw();
			guideSp.Draw ();
			timeManager.Draw();
			panelManager.Draw();
			jammingManager.Draw();
			if(nowState == StateId.GAME) cursor.Draw();
			if(nowState == StateId.PAUSE) menuManager.Draw();
			electhManager.Draw();
		}
		
		// 解放
		override public void Term()
		{
			backTex.Dispose();
			guideTex.Dispose();
			cursor.Term();
			panelManager.Term();
			jammingManager.Term();
			gameUI.Term ();
			musicEffect.Term();
			music.Term();
			timeManager.Term();
		}
	}
}