using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class PanelEditor : PanelManager
	{
		// エディット情報(マップデータ)
		public static bool IS_RANDOM_MAP = false;	// マップを新しくランダムに生成
		public static int RANDOM_MAP_W = 5;
		public const int RANDOM_MAP_H = 5;
		public const int START_X = 0;
		public const int START_Y = 0;
		public const int GOAL_X  = 4;
		public const int GOAL_Y  = 4;
		public static GameScene.StageData randomStage;	// ランダムマップ生成時に使う、ステージデータ構造体
		// (グループパネルについてのデータ)
		public const bool USE_GROUP_PANEL = false;
		public const int GROUP_LENGTH = 2;			// グループパネルの1辺の要素数
		public const int GROUP_ORIGIN_W = 2;
		public const int GROUP_ORIGIN_H = 2;
		
		public PanelEditor ()
		{
			Console.WriteLine("Instanced PanelEditor");
		}
		
		// パネルの配置
		protected override void LineupPanels (float panelSize, Vector2 basePos)
		{
			// 通常通り並べる-----------------------
			if(IS_RANDOM_MAP == false)
			{
				base.LineupPanels(panelSize, basePos);
				return;
			}
			
			// ランダムに並べる----------------------
			Random rand = new Random();
			// パネルリストの初期化
			panels = new List<List<Panel>>();
			for(int i = 0; i < GameScene.stage.width; i++)
			{
				List<Panel> panelLine = new List<Panel>();
				for(int j = 0; j < GameScene.stage.height; j++)
				{
					panelLine.Add (new NormalPanel((Panel.TypeId)rand.Next(0, 4),
					               new Vector2(basePos.X + panelSize * i,
					            			   basePos.Y + panelSize * j)));
				}
				panels.Add(panelLine);
			}
		}
	}// END OF CLASS
}