using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Eleconnect
{
    public partial class EditUI : Scene
    {
		private PanelManager panels;	// 操作対象となるパネル
		private int indexW, indexH;
		
        public EditUI(PanelManager panelManager)
        {
            InitializeWidget();
			panels = panelManager;
			
			ChangePanelBuuton.ButtonAction += new EventHandler<TouchEventArgs>(ChangePanel);
			SetSwitchButton.ButtonAction   += new EventHandler<TouchEventArgs>(SetSwitch);
			SetStartButton.ButtonAction    += new EventHandler<TouchEventArgs>(SetStart);
			SetGoalButton.ButtonAction     += new EventHandler<TouchEventArgs>(SetGoal);
			SaveButton.ButtonAction		   += new EventHandler<TouchEventArgs>(SaveMap);
			LoadButton.ButtonAction		   += new EventHandler<TouchEventArgs>(LoadMap);
        }
		
		// パネル切り替え
		private void ChangePanel(object sender, TouchEventArgs e)
		{
			Panel panel = panels[indexW, indexH];
			if(panel.typeId == Panel.TypeId.JammSwitch) return;	// スイッチの場合はスキップ
				
			Panel.TypeId newId = (Panel.TypeId)((int)panel.typeId + 1);
			if(newId > Panel.TypeId.Cross) newId = Panel.TypeId.Straight;
			panel.ChangeType(newId);
			PanelManager.CheckConnectOfPanels(GameScene.stage.startX, GameScene.stage.startY);
		}
		
		// スイッチ設置
		private void SetSwitch(object sender, TouchEventArgs e)
		{
			Panel panel = panels[indexW, indexH];
			
			// 以前まであったスイッチをストレートの通常パネルに変更
			for(int i = 0; i < GameScene.stage.width; i++)
			{
				for(int j = 0; j < GameScene.stage.height; j++)
				{
					if(panels[i, j].typeId == Panel.TypeId.JammSwitch)
					{
						panels.Replace(i, j, 
				                     new NormalPanel(Panel.TypeId.Straight, new Vector2(panels[i,j].GetPos().X, panels[i,j].GetPos().Y)));
					}
				}
			}
			
			// 新しいスイッチを設置
			panels.Replace(indexW, indexH, 
				                     new JammingSwitch(new Vector2(panel.GetPos().X, panel.GetPos().Y)));
			PanelManager.CheckConnectOfPanels(GameScene.stage.startX, GameScene.stage.startY);
		}
		
		// スタート設置
		private void SetStart(object sender, TouchEventArgs e)
		{
			GameScene.stage.startX = indexW;
			GameScene.stage.startY = indexH;
			PanelManager.CheckConnectOfPanels(GameScene.stage.startX, GameScene.stage.startY);
		}
		
		// スタート設置
		private void SetGoal(object sender, TouchEventArgs e)
		{
			// 以前まであったゴールを通常のものに変更
			for(int i = 0; i < GameScene.stage.width; i++)
			{
				for(int j = 0; j < GameScene.stage.height; j++)
				{
					panels[i, j].isGoal = false;
				}
			}
			
			// 新しいゴールを設置
			panels[indexW, indexH].isGoal = true;
			GameScene.stage.goalX = indexW;
			GameScene.stage.goalY = indexH;
			
			PanelManager.CheckConnectOfPanels(GameScene.stage.startX, GameScene.stage.startY);
		}
		
		// マップ保存
		private void SaveMap(object sender, TouchEventArgs e)
		{
			FileAccess fa = FileAccess.GetInstance();
			List<int> mapData = new List<int>();	// データ保存用リスト
			
			for(int i = 0; i < EditScene.stage.width; i++)
			{
				for(int j = 0; j < EditScene.stage.height; j++)
				{
					mapData.Add((int)panels[i, j].typeId);	// パネルのタイプを保存
					
					int rotateCnt = panels[i, j].rotateCnt;
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
		// マップ読み込み
		private void LoadMap(object sender, TouchEventArgs e)
		{
			if(LoadNoList.SelectedIndex != 0)
			{
				PlayData.GetInstance().stageNo = LoadNoList.SelectedIndex - 1;
				SceneManager.GetInstance().Switch(SceneId.EDIT);
			}
		}
		// 操作対象のパネルをセット
		public void SetIndex(CursorOnPanels cursor)
		{
			indexW = cursor.indexW;
			indexH = cursor.indexH;
		}
    }
}
