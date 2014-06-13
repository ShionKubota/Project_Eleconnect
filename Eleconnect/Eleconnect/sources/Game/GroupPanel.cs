using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class GroupPanel : Panel
	{
		// 変数
		private List<List<ChildPanel>> panels;
		private float angle;			// グループ全体の回転量
		private float rotateTo;			// 回転の目標値
		private Vector2 centerPos;
		public int length{ private set; get; }
		
		// 定数
		private const float ROTATE_SPEED = 0.2f;		// パネルの回転スピード
		private const float ROTATE_STOP_ANGLE = 1.0f;	// 目標の角度と現在の角度の差が、この数値以下になったら回転をストップ
		
		// コンストラクタ
		public GroupPanel(Vector2 pos, int length) : base(TypeId.Group, pos)
		{
			this.length = length;
			panels = new List<List<ChildPanel>>();
			
			angle = 0.0f;
			rotateTo = 0.0f;
			
			centerPos = pos + (((Panel.SIZE * Panel.SCALE + 5.0f) * length - 1) / 2.0f);
			centerPos -= (Panel.SIZE * Panel.SCALE + 5.0f) / 2.0f;
		}
		
		// ボタン押された際のイベント
		public override void ButtonEvent(bool pushR)
		{
			Console.WriteLine ("GroupRotate");
			Rotate (pushR);
		}
		
		// グループにパネル追加
		public override void AddPanel(TypeId id, Vector2 pos, int indexW, int indexH, int localIndexW)
		{
			if(panels.Count >= localIndexW)
				panels.Add (new List<ChildPanel>());
			panels[localIndexW].Add(new ChildPanel(id, pos, centerPos, indexW, indexH));
		}
		
		public override void Update ()
		{
			// 回転
			angle += (rotateTo - angle) * ROTATE_SPEED;
			if(FMath.Abs(rotateTo - angle) < ROTATE_STOP_ANGLE && rotateTo - angle != 0.0f)
			{
				// 回転終了
				angle = rotateTo;
				
				// 接続状況チェック
				PanelManager.CheckConnectOfPanels(GameScene.stageData[2], GameScene.stageData[3]);
				
				Console.WriteLine ("GroupUpdate");
			}
			
			// パネル個々の更新
			for(int i = 0; i < panels.Count; i++)
			{
				for(int j = 0; j < panels[i].Count; j++)
				{
					panels[i][j].Update();
					
					float thisAngle = FMath.Radians(angle) + panels[i][j].childData.initAngle;
					float newX = centerPos.X + FMath.Cos(thisAngle) * Vector3.Length(panels[i][j].GetPos() - new Vector3(centerPos.X, centerPos.Y, 0.0f));
					float newY = centerPos.Y + FMath.Sin(thisAngle) * Vector3.Length(panels[i][j].GetPos() - new Vector3(centerPos.X, centerPos.Y, 0.0f));
					panels[i][j].SetPos(new Vector3(newX, newY, 0.0f));
					//panels[i][j].SetPos(new Vector3(centerPos.X, centerPos.Y, 0.0f));
				}
			}
		}
		
		public override void Draw ()
		{
			foreach(List<ChildPanel> line in panels)
			{
				foreach(ChildPanel panel in line)
				{
					panel.Draw();
				}
			}
		}
		
		public override Vector3 GetPos()
		{
			return panels[0][0].GetPos();
		}
		public override Vector3 GetPos(int indexW, int indexH)
		{
			foreach(List<ChildPanel> line in panels)
			{
				foreach(ChildPanel panel in line)
				{
					if(panel.childData.indexW == indexW &&
					   panel.childData.indexH == indexH)
					{
						return panel.GetPos();
					}
				}
			}
			
			return GetPos();
		}
		
		// 回転させる
		protected override void Rotate(bool isClockwise)
		{
			if(angle % 90.0f != 0.0f) return;	// 非回転時のみ実行
			rotateTo = angle + (isClockwise ? 90.0f : -90.0f);
			foreach(List<ChildPanel> line in panels)
			{
				foreach(ChildPanel panel in line)
				{
					panel.ButtonEvent(isClockwise);
				}
			}
			
		}
	}// END OF CLASS
}

