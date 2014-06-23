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
		public static PanelManager panelManager{ protected set; get; }
		protected JammingManager jammingManager;
		protected CursorOnPanels cursor;
		protected MenuManager menuManager;
		protected ElecthManager electhManager;
		protected ResultScene result;
		protected ItemManager itemManager;
		protected GameUI gameUi;

		// 画像
		private Sprite2D backSp;
		private Texture2D backTex;
		//private Sprite2D guideSp;
		//private Texture2D guideTex;
		public Animation electhSp;
		private Texture2D electhTex;
		// 音楽
		protected MusicEffect musicEffect;
		protected MusicEffect resultEffect;
		protected static Music music;
		
		// ステージ情報
		public struct StageData
		{
			public int width, height,
				startX, startY,
				goalX, goalY,
				jamming;
			
			public int[] mapData;
		}
		public static StageData stage;								// ステージ情報格納用
		public static string mapFileName{ protected set; get; }		// 読み込むマップファイル名
		public static string stageFileName{ protected set; get; }		// 読み込むステージ情報ファイル名
		
		// プレイ情報
		private int frameCnt;
		private int aniFrame;
		private float eleRotate;
		private bool seFlg;
		
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
			
			panelManager = new PanelManager();
			
			CommonInit();
			
			
			// デバッグ表示
			//if(IS_DEBUG_MODE) Console.WriteLine("IS_DEBUG_MODE...Xkey : Output the data of this panel.\tD/Wkey : Increment/Decrement the elecPowMax.");
			//if(IS_EDIT_MODE) Console.WriteLine("IS_DEBUG_MODE...Zkey : Save the data of this map.");
		}
		
		// 子と共通の初期化処理
		protected void CommonInit()
		{
			// スプライト
			if(backTex == null)
			{
				backTex = new Texture2D(@"/Application/assets/img/eleconnect_background.png", false);
			}
			backSp = new Sprite2D(backTex);
			backSp.pos = AppMain.ScreenCenter;
			backSp.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
			backSp.color *= new Vector4(1.0f);
			/*
			if(guideTex == null)
			{
				guideTex = new Texture2D(@"/Application/assets/img/guid.png", false);
			}
			guideSp = new Sprite2D(guideTex);
			guideSp.pos = new Vector3(AppMain.ScreenWidth - 130.0f,
			                          AppMain.ScreenHeight / 2.0f + 50.0f,
			                          0.0f);
			guideSp.size = new Vector2(0.4f, 0.4f);
			*/
			if(electhTex == null)
			{
				electhTex = new Texture2D(@"/Application/assets/img/electh.png", false);
			}
			electhSp = new Animation(electhTex,new Vector2(1.0f/6.0f, 1.0f/2.0f),3,0,5,true,true,true);
			electhSp.pos = panelManager[stage.startX, stage.startY].GetPos();
			
			// インスタンス生成
			cursor = new CursorOnPanels(panelManager);
			menuManager = new MenuManager();
			jammingManager = new JammingManager();
			electhManager = new ElecthManager(panelManager, jammingManager);
			itemManager = new ItemManager(panelManager, cursor);
			gameUi = new GameUI();
			result = new ResultScene();
			
			// パラメータ初期化
			frameCnt = 0;
			aniFrame = 0;
			eleRotate = 0;
			PlayData.GetInstance().connectNum = 0;
			seFlg = false;
			nowState = StateId.GAME;
	
			// 音関連
			music = new Music(@"/Application/assets/music/Game_Music.mp3");
			musicEffect = new MusicEffect(@"/Application/assets/se/Rotate_SE.wav");
			resultEffect = new MusicEffect(@"/Application/assets/se/Result_SE.wav");
			music.Set(true,0.4f,0.0d);
		}
		
		// ステージのデータを読み込み
		protected void LoadStageData()
		{
			Stage_Base stageData;
			stageData = new StageDataList()[PlayData.GetInstance().stageNo];
			
			// マップ読み込み
			stage.mapData = stageData.mapData;
			
			// ステージ情報読み込み
			stage.width = stageData.stageData[0];
			stage.height = stageData.stageData[1];
			stage.startX = stageData.stageData[2];
			stage.startY = stageData.stageData[3];
			stage.goalX = stageData.stageData[4];
			stage.goalY = stageData.stageData[5];
			stage.jamming = stageData.stageData[6];
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
				backSp.color.W += (0.2f - backSp.color.W) * 0.1f;
				AfterClearingProcess();
				seFlg = true;
				break;
				
			case StateId.PAUSE:
				PausingProcess();
				break;
			}
			
			// パネルの更新
			panelManager.Update();
			jammingManager.Update();
			electhSp.Update();
			gameUi.Update(nowState);
			
			frameCnt++;
			eleRotate++;
			electhSp.angle=eleRotate;
			
		}
		
		// ゲーム中の更新プロセス
		protected void GamingProcess()
		{
			// カーソルの更新
			cursor.Update(panelManager);
			
			
			// アイテム更新
			itemManager.Update();
			
			// キー操作を受付け＆処理
			AcceptPlayerInput();
			
			// エレクス更新
			electhManager.Update();
		}
		
		// エレクスが流れるプロセス
		protected void FrowingProcess()
		{
			electhManager.Update();
			
			// エレクスが流れきったとき
			if(!electhManager.nowFlowing)
			{
				//nowState = (Electh.arrivedGoal) ? StateId.CLEAR : StateId.GAME;
				GC.Collect();	// ガベコレ発動
				if(Electh.arrivedGoal)
				{
					music.Stop();
					resultEffect.Set();
					nowState = StateId.CLEAR;
				}
				else
				{
					PlayData.GetInstance().connectNum = 0;
					nowState = StateId.GAME;	
				}
			}
		}
		
		// クリア後の更新プロセス
		protected void AfterClearingProcess()
		{
			for(int i = 0; i < stage.width; i++)
			{
				for(int j = 0; j < stage.height; j++)
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
			result.Update();
			
			// エレクス更新
			electhManager.Update();
		}
		
		// ポーズ中の更新プロセス
		protected void PausingProcess()
		{
			// ポーズメニューの更新
			menuManager.Update();
			
			// ポーズ終了をチェック
			if((menuManager.isEnd || Input.GetInstance().start.isPushStart)&&menuManager.selectFlg == false)
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
			if(input.triggerR.isPushStart)
			{
				panel.ButtonEvent(true);
				musicEffect.Set(1.0f,false);
			}
			if(input.triggerL.isPushStart)
			{
				panel.ButtonEvent(false);
				musicEffect.Set(1.0f,false);
			}
			
			// エレクスを流す
			if(input.circle.isPushStart)
			{
				frameCnt = 0;
				nowState = StateId.FLOW_ELECTH;
				electhManager.FlowStart(stage.startX, stage.startY, true);
			}
			
			/*
			// アイテムを使用
			// リピーター
			if(input.cross.isPushStart)
			{
				if(repeaterCnt < 1 && !(indexW == stage.width - 1 && indexH == stage.height - 1))
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
			//guideSp.Draw ();
			panelManager.Draw();
			if(nowState != StateId.CLEAR) jammingManager.Draw();
			if(nowState == StateId.GAME) cursor.Draw();
			if(nowState == StateId.PAUSE) menuManager.Draw();
			if(nowState == StateId.CLEAR && seFlg == true) result.Draw();
			else
			{
				itemManager.Draw ();
				gameUi.Draw();
			}
			electhManager.Draw();
			if(!electhManager.nowFlowing && nowState != StateId.CLEAR) electhSp.DrawAdd();
		}
		
		// 解放
		override public void Term()
		{
			//backTex.Dispose();
			backSp.Term();
			//electhTex.Dispose ();
			electhSp.Term();
			cursor.Term();
			panelManager.Term();
			jammingManager.Term();
			musicEffect.Term();
			music.Term();
			gameUi.Term();
			menuManager.Term();
			//result.Term();
			itemManager.Term();
		}
	}
}