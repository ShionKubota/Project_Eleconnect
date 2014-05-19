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
	public class EditScene : GameScene
	{
		private PanelManager panelManager;
		
		public EditScene ()
		{
			//PlayData.GetInstance().stageNo = 0;
		}
		
		// 初期化
		override public void Init()
		{
			// 読み込むファイルを選択
			PlayData playData = PlayData.GetInstance();
			mapFileName = "mapData{stageNo}.dat".Replace("{stageNo}", playData.stageNo + "");		// マップデータ
			stageFileName = "stageData{stageNo}.dat".Replace("{stageNo}", playData.stageNo + "");	// ステージデータ
			
			// ステージのデータを読み込み
			if(!PanelEditor.IS_RANDOM_MAP)
			{
				LoadStageData();
			}
			
			stageWidth = (PanelEditor.IS_RANDOM_MAP) ? PanelEditor.RANDOM_MAP_W : stageData[0];
			stageHeight = (PanelEditor.IS_RANDOM_MAP) ? PanelEditor.RANDOM_MAP_H : stageData[1];
			
			CommonInit();
			
			gameUI = new GameUI();
			panelManager = new PanelEditor();
			cursor = new CursorOnPanels(panelManager);
			
			
			// デバッグ表示
			//if(IS_DEBUG_MODE) Console.WriteLine("IS_DEBUG_MODE...Xkey : Output the data of this panel.\tD/Wkey : Increment/Decrement the ELEC_POW_MAX.");
			Console.WriteLine("IS_EDIT_MODE...Zkey : Save the data of this map.");
		}
		
		
		
		// プレイヤー操作
		protected override void AcceptPlayerInput ()
		{
			Input input = Input.GetInstance();
			
			int indexW = cursor.indexW,
				indexH = cursor.indexH;
			
			// 選んでいるパネル
			Panel panel = panelManager[indexW, indexH];
			
			// パネルを回転
			if(input.triggerR.isPushStart) panel.ButtonEvent(true);
			if(input.triggerL.isPushStart) panel.ButtonEvent(false);
			
			// アイテムを使用
			// リピーター
			if(input.cross.isPushStart)
			{
				panel.isRepeater = true;
				PanelManager.CheckConnectOfPanels(0, 0);
			}
			
			// パネル変化
			if(input.square.isPushStart)
			{
				Panel.TypeId newId = (Panel.TypeId)((int)panel.typeId + 1);
				if(newId > Panel.TypeId.Cross) newId = Panel.TypeId.Straight;
				panel.ChangeType(newId);
				PanelManager.CheckConnectOfPanels(0, 0);
			}
			
			// デバッグ機能
			DebugProcess(input);
			
			// マップデータ保存
			if(input.select.isPushEnd) SaveMap ();
		}
		// マップデータセーブ
		private void SaveMap()
		{
			FileAccess fa = FileAccess.GetInstance();
			List<int> mapData = new List<int>();	// データ保存用リスト
			
			for(int i = 0; i < EditScene.stageWidth; i++)
			{
				for(int j = 0; j < EditScene.stageHeight; j++)	
				{
					mapData.Add((int)panelManager[i, j].typeId);	// パネルのタイプを保存
					
					int rotateCnt = panelManager[i, j].rotateCnt;
					if(rotateCnt < 0) rotateCnt = 4 - rotateCnt;
					if(rotateCnt > 9) rotateCnt = rotateCnt % 4;	// パネルの回転数(2桁の数や負の数であれば、0~3の数値に変換する)
					mapData.Add(rotateCnt);		// パネルの回転数を保存
				}
			}
			fa.SavaData(mapFileName, mapData.ToArray());
			
			// ステージデータ保存{横, 縦, リピーター数, チェンジアイテム数}
			int[] stageData = new int[]{EditScene.stageWidth, EditScene.stageHeight};
			fa.SavaData(stageFileName, stageData);
			Console.WriteLine ("SUCCESSFULL : Save the data of map.\n");
		}
	}// END OF CLASS
}