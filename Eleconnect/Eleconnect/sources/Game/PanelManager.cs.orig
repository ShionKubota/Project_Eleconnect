using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class PanelManager
	{
		// パネル
		protected static List<List<Panel>> panels;	// パネルを格納する2重リスト
		public Panel this[int indexW, int indexH]
		{
			protected set
			{
				panels[indexW][indexH] = value;
			}
			
			get
			{
				return panels[indexW][indexH];
			}
		}
		
		public PanelManager ()
		{
			Init ();
		}
		
		// 初期化
		public void Init()
		{
			// 1つめのパネルの位置計算
			float panelSize = Panel.SIZE * Panel.SCALE + 5.0f;
<<<<<<< HEAD
			Vector2 basePos = new Vector2(16.0f*14,
			                              AppMain.ScreenCenter.Y - ((panelSize * GameScene.stage.height) / 2.0f) + (panelSize / 2.0f));
			//Vector2 basePos = new Vector2(AppMain.ScreenWidth / 8.0f /*AppMain.ScreenCenter.X - ((panelSize * GameScene.stage.width) / 2.0f) + (panelSize / 2.0f)*/,
			//                             AppMain.ScreenCenter.Y - ((panelSize * GameScene.stage.height) / 2.0f) + (panelSize / 2.0f));
=======
			Vector2 basePos = new Vector2(16.0f*14, AppMain.ScreenCenter.Y - ((panelSize * GameScene.stage.height) / 2.0f) + (panelSize/2));
>>>>>>> 352542059f67d66e58a0648ecf0a7119534a93e1
			//basePos.X += 100.0f;
			
			// パネルを並べる
			LineupPanels(panelSize, basePos);
			
			// ゴールを設置
			panels[GameScene.stage.goalX][GameScene.stage.goalY].isGoal = true;
			
			// 接続状況初期化
			CheckConnectOfPanels(GameScene.stage.startX, GameScene.stage.startY);
			
		}
		
		// パネルの配置
		protected virtual void LineupPanels(float panelSize, Vector2 basePos)
		{
			// パネルリストの初期化
			panels = new List<List<Panel>>();
			for(int i = 0; i < GameScene.stage.width; i++)
			{
				List<Panel> panelLine = new List<Panel>();
				for(int j = 0; j < GameScene.stage.height; j++)
				{
					// 読み込んだマップデータを基に配置
					int loadIndex = ((GameScene.stage.height * 2) * i) + (j * 2);
					Panel.TypeId type = (Panel.TypeId)GameScene.stage.mapData[loadIndex];
					// 通常パネル
					if(type <= Panel.TypeId.Cross)
					{
						panelLine.Add (new NormalPanel(type,
					    	                     new Vector2(basePos.X + panelSize * i,
					        	    			  			 basePos.Y + panelSize * j)));
					}
					// スイッチ
					if(type == Panel.TypeId.JammSwitch)
					{
						panelLine.Add (new JammingSwitch(new Vector2(basePos.X + panelSize * i,
					        	    			  			         basePos.Y + panelSize * j)));
					}
					panelLine[j].Rotate(GameScene.stage.mapData[loadIndex + 1]);
				}
				panels.Add(panelLine);
			}
			
			// グループパネルの設置
			if(PanelEditor.USE_GROUP_PANEL)
			{
				int indexW = PanelEditor.GROUP_ORIGIN_W, 
					indexH = PanelEditor.GROUP_ORIGIN_H;
				
				Panel child = panels[indexW][indexH];
				
				GroupPanel gPanel = new GroupPanel(new Vector2(child.GetPos().X, child.GetPos().Y),
				                                   PanelEditor.GROUP_LENGTH);
				for(int i = 0; i < FMath.Pow(gPanel.length, 2); i++)
				{
					// 新しい子パネル
					indexW = PanelEditor.GROUP_ORIGIN_W + (i) % gPanel.length;
					indexH += (indexW == PanelEditor.GROUP_ORIGIN_W && i != 0) ? 1 : 0;
					child = panels[indexW][indexH];
					
					Panel.TypeId id = panels[indexW][indexH].typeId;
					
					if(i > 1)
					{
						Console.WriteLine ();
					}
					// グループにパネルを追加
					gPanel.AddPanel(id, new Vector2(child.GetPos().X, child.GetPos().Y),
					                indexW, indexH, i);
					
					// 配列にグループパネルをセット
					panels[indexW][indexH] = gPanel;
				}
			}
		}
		
		// 更新
		public void Update()
		{
			bool groupUpdated = false;
			
			// パネルの更新
			foreach(List<Panel> line in panels)
			{
				foreach(Panel panel in line)
				{
					// グループパネルは1度だけ更新する
					if(panel is GroupPanel)
					{
						if(!groupUpdated)
						{
							panel.Update();
							groupUpdated = true;
						}
					}
					// それ以外は通常通り更新
					else
					{
						panel.Update();
					}
				}
			}
		}
		
		// 描画
		public void Draw()
		{
			bool groupDrawn = false;
			
			foreach(List<Panel> line in panels)
			{
				foreach(Panel panel in line)
				{
					// グループパネルは1度だけ描画する
					if(panel is GroupPanel)
					{
						if(!groupDrawn)
						{
							panel.Draw();
							groupDrawn = true;
						}
					}
					else
					{
						panel.Draw();
					}
				}
			}
		}
		
		// 解放
		public void Term()
		{
			/*
			foreach(List<Panel> line in panels)
			{
				foreach(Panel panel in line)
				{
					panel.Term();
				}
			}*/
			for(int i = GameScene.stage.width - 1; i >= 0; i--)
			{
				for(int j = GameScene.stage.height - 1; j >= 0; j--)
				{
					panels[i][j].Term();
					panels[i].RemoveAt(j);
				}
				panels.RemoveAt(i);
			}
		}
		
		// パネル同士の接続をチェック
		public static bool CheckConnectOfPanels(int startIndexW, int startIndexH)
		{	
			// 探査済みかどうかを保存しておくマップ
			for(int i = 0; i < GameScene.stage.width; i++)
			{
				for(int j = 0; j < GameScene.stage.height; j++)
				{
					panels[i][j].elecPow = 0;
				}
			}
			
			// 調査
			CheckConnect(startIndexW, startIndexH, Panel.elecPowMax);
		
			return false;
		}
		
		// 指定したパネルと、その上下左右のパネルとの接続を確認していく
		private static void CheckConnect(int indexW, int indexH, int elecPow)
		{	
			Panel panel = panels[indexW][indexH];	// 探査の基準とするパネル(現在地点)
			
			// pパネルに電力を適用する(リピーターアイテムが使われていれば、電力はMAXに回復)
			panel.elecPow = (panel.isRepeater) ? Panel.elecPowMax : elecPow;
			
			// 電力が0ならこれ以上の調査はしない
			if(panel.elecPow <= 0) return;
			
			// この地点がスイッチ、ゴールだったらこれ以上調査しない
			if(panel.typeId == Panel.TypeId.JammSwitch || panel.isGoal)
			{
				return;
			}
			
			// 調査-----------------------------------------------------------------------------
			// パネルの上下左右を確認、つながっていたらそのパネルもチェック
			int[] moveTblW = {0, 1, 0, -1};			// 現在のパネルから隣のパネル(上下左右)への移動量テーブル
			int[] moveTblH = {-1, 0, 1, 0};
			for(int i = 0; i < 4; i++)
			{
				if(panel.route[i] == false)	continue; // こちらから道が出ていない方向には調査を行わない
				
				
				// 調査先パネルの番号
				int checkPanelIndexW = indexW + moveTblW[i];
				int checkPanelIndexH = indexH + moveTblH[i];
				
				// 調査先が存在しない場合は調査しない
				if(checkPanelIndexW < 0 || checkPanelIndexW >= GameScene.stage.width ||
				   checkPanelIndexH < 0 || checkPanelIndexH >= GameScene.stage.height)
					continue;
				
				// 調査先に、自分より強い電流が既に流れていたら調査しない
				if(panel.elecPow <= panels[checkPanelIndexW][checkPanelIndexH].elecPow) continue;
				
				// 調査先パネルにて,調査するルート番号を設定(自分が0(↑)なら相手は2(↓)という具合)
				int checkRouteIndex = i + 2;
				if(checkRouteIndex > 3) checkRouteIndex -= 4;
				
				// 電力の設定
				int nextElecPow = panel.elecPow - 1;
				
				// 接続できていたらそのパネルをチェック(再帰)
				if(panels[checkPanelIndexW][checkPanelIndexH].route[checkRouteIndex] == true)
				{
					CheckConnect (checkPanelIndexW, checkPanelIndexH, nextElecPow);
				}
			}
			return;
		}
		
		// パネルが何行何列目の要素か教えてもらう
		public void GetIdByPanel(Panel panel, ref int id_w, ref int id_h)
		{
			for(int i = 0; i < panels.Count; i++)
			{
				int j = panels[i].IndexOf(panel);
				if(j >= 0)
				{
					id_w = i;
					id_h = j;
				}
			}
		}
		
		// 指定した番号のパネル位置を取得
		public Vector3 GetPos(int indexW, int indexH)
		{
			// 修正要綱：パネルの親クラスでGetPosをオーバーロードさせるか？
			if(panels[indexW][indexH] is GroupPanel)
			{
				return panels[indexW][indexH].GetPos(indexW, indexH);
			}
			else
			{
				return panels[indexW][indexH].GetPos();
			}
		}
		
		// 指定の番号の要素を新しいインスタンスに置き換え
		public void Replace(int indexW, int indexH, Panel newItem)
		{
			panels[indexW][indexH] = newItem;
		}
	}// END OF CLASS
}