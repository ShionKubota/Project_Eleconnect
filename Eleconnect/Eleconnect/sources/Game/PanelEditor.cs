using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class PanelEditor : PanelManager
	{
		// エディット情報
		public const bool IS_RANDOM_MAP = true;	// マップをランダムに配置
		public const int RANDOM_MAP_W = 5;
		public const int RANDOM_MAP_H = 5;
		
		
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
			for(int i = 0; i < GameScene.stageWidth; i++)
			{
				List<Panel> panelLine = new List<Panel>();
				for(int j = 0; j < GameScene.stageHeight; j++)
				{
					panelLine.Add (new Panel((Panel.TypeId)rand.Next(0, 4),
					               new Vector2(basePos.X + panelSize * i,
					            			   basePos.Y + panelSize * j)));
				}
				panels.Add(panelLine);
			}
		}
	}// END OF CLASS
}