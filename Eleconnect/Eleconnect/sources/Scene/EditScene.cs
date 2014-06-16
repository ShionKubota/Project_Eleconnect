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
		private EditUI editUI;
		private Sprite2D dummySp;
		private Texture2D dummyTex;
		
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
			
			stage.width = (PanelEditor.IS_RANDOM_MAP) ? PanelEditor.RANDOM_MAP_W : GameScene.stage.width;
			stage.height = (PanelEditor.IS_RANDOM_MAP) ? PanelEditor.RANDOM_MAP_H : GameScene.stage.height;
			
			panelManager = new PanelEditor();
			
			CommonInit();
			
			editUI = new EditUI(panelManager);
			UISystem.SetScene(editUI);
			
			// バグ修正用のテクスチャ
			dummyTex = new Texture2D(@"Application/assets/img/particle.png", false);
			
			// デバッグ表示
			//if(IS_DEBUG_MODE) Console.WriteLine("IS_DEBUG_MODE...Xkey : Output the data of this panel.\tD/Wkey : Increment/Decrement the elecPowMax.");
			Console.WriteLine("IS_EDIT_MODE...Zkey : Save the data of this map.");
		}
		
		
		
		// プレイヤー操作
		protected override void AcceptPlayerInput ()
		{
			base.AcceptPlayerInput();
			
			Input input = Input.GetInstance();
			
			int indexW = cursor.indexW,
				indexH = cursor.indexH;
			
			// 選んでいるパネル
			Panel panel = panelManager[indexW, indexH];
			editUI.SetIndex(cursor);
			
			// パネル変化
			/*
			if(input.cross.isPush == false && input.square.isPushStart)
			{
				Panel.TypeId newId = (Panel.TypeId)((int)panel.typeId + 1);
				if(newId > Panel.TypeId.Cross) newId = Panel.TypeId.Straight;
				panel.ChangeType(newId);
				PanelManager.CheckConnectOfPanels(0, 0);
			}
			*/
			/*
			// スイッチ設置
			if(input.cross.isPush && input.square.isPushStart)
			{
				panelManager.Replace(indexW, indexH, 
				                     new JammingSwitch(new Vector2(panel.GetPos().X, panel.GetPos().Y)));
				PanelManager.CheckConnectOfPanels(GameScene.stage.startX, GameScene.stage.startY);
			}
			*/
			// マップデータ保存
			//if(input.select.isPushEnd) SaveMap ();
			
			dummySp = new Sprite2D(dummyTex);
		}
		// マップデータセーブ
		private void SaveMap()
		{
			FileAccess fa = FileAccess.GetInstance();
			List<int> mapData = new List<int>();	// データ保存用リスト
			
			for(int i = 0; i < EditScene.stage.width; i++)
			{
				for(int j = 0; j < EditScene.stage.height; j++)
				{
					mapData.Add((int)panelManager[i, j].typeId);	// パネルのタイプを保存
					
					int rotateCnt = panelManager[i, j].rotateCnt;
					if(rotateCnt < 0) rotateCnt = 4 - rotateCnt;
					if(rotateCnt > 9) rotateCnt = rotateCnt % 4;	// パネルの回転数(2桁の数や負の数であれば、0~3の数値に変換する)
					mapData.Add(rotateCnt);		// パネルの回転数を保存
				}
			}
			fa.SavaData("mapData.dat", mapData.ToArray());
			
			// ステージデータ保存{横, 縦, }
			int[] stageData = new int[]{
										 EditScene.stage.width, EditScene.stage.height,
										 PanelEditor.START_X, PanelEditor.START_Y,
										 PanelEditor.GOAL_X, PanelEditor.GOAL_Y
										};
			fa.SavaData("stageData.dat", stageData);
			Console.WriteLine ("SUCCESSFULL : Save the data of map.\n");
		}
	}// END OF CLASS
}