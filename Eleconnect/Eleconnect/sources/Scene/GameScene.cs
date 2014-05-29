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
		public static PanelManager panelManager{protected set; get;}
		protected JammingManager jammingManager;
		protected CursorOnPanels cursor;
		protected MenuManager menuManager;
		protected List<Electh> electh;
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
		public static List<int> mapData{ protected set; get; }		// マップデータ
		public static List<int> stageData{ protected set; get; }		// ステージデータ
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
			PlayData playData = PlayData.GetInstance();
			mapFileName = "mapData{stageNo}.dat".Replace("{stageNo}", playData.stageNo + "");		// マップデータ
			stageFileName = "stageData{stageNo}.dat".Replace("{stageNo}", playData.stageNo + "");	// ステージデータ
			LoadStageData();
			
			stageWidth = stageData[0];
			stageHeight = stageData[1];
			
			CommonInit();
			
			panelManager = new PanelManager();
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
			electh = new List<Electh>();
			jammingManager = new JammingManager();
			
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
			// マップ読み込み
			mapData = new List<int>();
			string mapDataStr = "";
			FileAccess.GetInstance().LoadData(mapFileName,
			                                  ref mapDataStr);
			
			for(int i = 0; i < mapDataStr.Length; i++)
			{
				Console.WriteLine ("mapdata[{0}] = {1}", i, mapDataStr[i]);
				if(mapDataStr[i] == ',') continue;
				mapData.Add(int.Parse(mapDataStr[i]+""));
			}
			
			// ステージ情報読み込み
			stageData = new List<int>();
			string stageDataStr = "";
			FileAccess.GetInstance().LoadData(stageFileName,
			                                  ref stageDataStr);
			
			for(int i = 0; i < stageDataStr.Length; i++)
			{
				if(stageDataStr[i] == ',') continue;
				stageData.Add(int.Parse(stageDataStr[i]+""));
			}
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
			
			// 仮に、右下までつながったらクリアーとする
			if(panelManager[stageWidth - 1, stageHeight - 1].elecPow > 0)
			{
				/*
				frameCnt = 0;
				nowState = StateId.FLOW_ELECTH;
				electh.Add (new Electh(panelManager[0,0], Electh.DEF_SPEED));
				Panel.elecPowMax = 1;
				PanelManager.CheckConnectOfPanels(0, 0);
				*/
			}
			
		}
		
		// エレクス（電気の球）がステージ上を流れる際の更新プロセス
		protected void FrowingProcess()
		{
			// 全てのエレクスが消滅したらゲームへ戻る
			if(electh.Count == 0)
			{
				nowState = StateId.GAME;
				JammingSwitch.isJamming = true;
				Panel.elecPowMax = 99;
			}	
			
			// エレクスの更新
			for(int i = electh.Count-1; i >= 0; i--)
			{
				if(electh[i].state == Electh.StateId.WAIT)
				{
					SetElecth (i);
				}
				
				electh[i].Update ();
				
				// 役目を果たしたエレクスをリストからはずす
				if(electh[i].state == Electh.StateId.DEATH) electh.RemoveAt(i);
			}
		}
		
		// エレクスを次のパネルに発進させる(分岐していたら新しいエレクスを作る)
		private void SetElecth(int id)
		{
			bool needNewElecth = false;
			
			Panel oldTarget = electh[id].target,	// このエレクスが現在到着しているパネル
				  newTarget;						// 次のターゲットなるパネル(次のターゲットは無いかもしれないので初期化はここではしない)
			
			float oldSpeed = electh[id].speed;
			
			int id_w = 0, id_h = 0;					// 現在のパネルの、配列における要素番号
			panelManager.GetIdByPanel(oldTarget, ref id_w, ref id_h);
			
			int[] moveTblW = {0, 1, 0, -1};			// 現在のパネルから隣のパネル(上下左右)への移動量テーブル
			int[] moveTblH = {-1, 0, 1, 0};
			
			for(int j = 0; j < 4; j++)	// 4方向チェック
			{
				if(oldTarget.route[j])
				{
					// 調査先番号
					int checkIndexW = id_w + moveTblW[j],
						checkIndexH = id_h + moveTblH[j];
					
					// 調査先が存在しない場合は調査しない
					if(checkIndexW < 0 || checkIndexW >= GameScene.stageWidth ||
					   checkIndexH < 0 || checkIndexH >= GameScene.stageHeight)
						continue;
					
					// 存在していたら、調査先を確定
					else
					{
						newTarget = panelManager[checkIndexW, checkIndexH];
					}
					
					// 調査先に電流が既に流れていたら調査しない
					if(newTarget.elecPow > 0) continue;
					/*
					// ジャミングが張られていたら調査しない
					int oldIndexW = 0, oldIndexH = 0, jamIndexW, jamIndexH;
					panelManager.GetIdByPanel(oldTarget, ref oldIndexW, ref oldIndexH);
					jamIndexW = (checkIndexW > oldIndexW) ? checkIndexW : oldIndexW;
					jamIndexH = (checkIndexH > oldIndexH) ? checkIndexH : oldIndexH;
					if(jammingManager.jammingData[jamIndexW, jamIndexH] == 1)
					{
						continue;
					}
					*/
					// 調査先パネルにて,調査するルート番号を設定(自分が0(↑)なら相手は2(↓)という具合)
					int checkRouteIndex = j + 2;
					if(checkRouteIndex > 3) checkRouteIndex -= 4;
					
					// 接続できていたらそのパネルにエレクスを向かわせる
					if(newTarget.route[checkRouteIndex] == true)
					{
						// 新しいエレクスが必要なら生成して再帰
						if(needNewElecth == true)
						{
							electh.Add(new Electh(oldTarget, oldSpeed));
							SetElecth (electh.Count-1);
						}
						// そうでなければ現在のエレクスをそのまま次に向かわせる
						else
						{
							needNewElecth = true;
							electh[id].SetTarget(newTarget);
							newTarget.elecPow = 1;
						}
					}
				}
			}
			
			// どの方向にもエレクスが進まなかったら消滅させる
			if(electh[id].state == Electh.StateId.WAIT) electh[id].Kill();
		}
		
		// クリア後の更新プロセス
		protected void AfterClearingProcess()
		{
			// リザルトへ
			if(Panel.elecPowMax == 10)
			{
				this.fadeOutTime = 100;
				this.fadeOutColor = new Vector3(1.0f, 1.0f, 1.0f);
				SceneManager.GetInstance().Switch(SceneId.RESULT);
			}
			
			// 消える演出
			if(frameCnt == 30)
			{
				PanelManager.CheckConnectOfPanels(0, 0);
			}
			
			// 光る演出
			if(frameCnt % 5 == 0 && frameCnt > 70 && Panel.elecPowMax < 10)
			{
				Panel.elecPowMax++;
				PanelManager.CheckConnectOfPanels(0, 0);
			}
			
			//Panel.elecPowMax += (int)(frameCnt * 0.01f);
			
			//Panel.elecPowMax = (int)(5.0f + 4 * FMath.Sin(FMath.Radians(frameCnt * 10)));
			//PanelManager.CheckConnectOfPanels(0, 0);
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
				electh.Add (new Electh(panelManager[0,0], Electh.DEF_SPEED));
				Panel.elecPowMax = 1;
				PanelManager.CheckConnectOfPanels(0, 0);
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
			foreach(Electh elec in electh)
			{
				elec.Draw();
			}
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