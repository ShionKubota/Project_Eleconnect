using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class ElecthManager
	{
		private List<Electh> electh;
		private PanelManager panels;
		private JammingManager jammingMng;
		private Particles particle;
		
		public bool visibleElecth;
		public bool nowFlowing{ private set; get; }
		
		public bool clearFlg;
		
		public ElecthManager (PanelManager panels, JammingManager jammingMng)
		{
			electh = new List<Electh>();
			this.panels = panels;
			this.jammingMng = jammingMng;
			visibleElecth = false;
			nowFlowing = false;
			
			clearFlg = false;
			
			// パーティクルの設定
			particle = new Particles(500);
			particle.LoadTextureInfo(@"Application/assets/img/particle.png", false);
			particle.posVar = new Vector2(0.0f, 0.0f);
			particle.velocity = new Vector2(0.0f, -3.0f);
			particle.velocityVar = new Vector2(2.0f, 4.0f);
			particle.colorEnd = new Vector3(0.6f, 0.8f, 0.9f);
			particle.colorEndVar = new Vector3(0.2f);
			particle.scaleStart = 1.0f;
			particle.scaleStartVar = 0.5f;
			particle.scaleEndVar = 0.0f;
			particle.lifeSpan = 1.0f;
			particle.fade = 0.2f;
			particle.gravity = new Vector2(0.0f, 0.1f);
			particle.stopAutoGenerate = true;
		}
		
		// エレクスを流す
		public void FlowStart(int indexW, int indexH, bool visibleElecth)
		{
			if(nowFlowing) return;
			
			this.visibleElecth = visibleElecth;
			electh.Add (new Electh(panels[indexW, indexH], Electh.DEF_SPEED));
			nowFlowing = true;
			
			
			Panel.elecPowMax = 1;
			PanelManager.CheckConnectOfPanels(0, 0);
		}
		
		// 更新
		public void Update()
		{
			particle.Update();
			
			// 全てのエレクスが消滅したらゲーム、もしくはクリア状態へ遷移する
			if(electh.Count == 0)
			{
				//nowState = (Electh.arrivedGoal) ? StateId.CLEAR : StateId.GAME;
				nowFlowing = false;
				JammingSwitch.isJamming = true;
				Panel.elecPowMax = 99;
				PanelManager.CheckConnectOfPanels(0, 0);
			}	
			
			// エレクスの更新
			for(int i = electh.Count-1; i >= 0; i--)
			{
				if(electh[i].state == Electh.StateId.WAIT)
				{
					SetElecth (i);
				}
				
				electh[i].Update ();
				// パーティクル
				if(electh[i].state == Electh.StateId.WAIT)
				{
					if(electh[i].target.isGoal)
					{
						// どでかいパーティクル
						particle.velocity = new Vector2(0.0f, -7.0f);
						particle.velocityVar = new Vector2(10.0f, 7.0f);
						particle.colorStart = new Vector3(1.0f);
						particle.colorStartVar = new Vector3(1.0f);
						particle.colorEnd = particle.colorStart;
						particle.colorEndVar = new Vector3(0.5f);
						particle.scaleStart = 2.0f;
						particle.scaleStartVar = 2.0f;
						particle.lifeSpan = 1.0f;
						particle.lifeSpanVar = 0.7f;
						particle.fade = 0.2f;
						particle.Generate(100);
						electh[i].Kill();
					}
					else
					{
						particle.pos = new Vector2(electh[i].sp.pos.X, electh[i].sp.pos.Y);
						particle.Generate(5);
					}
				}
				
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
			panels.GetIdByPanel(oldTarget, ref id_w, ref id_h);
			
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
						newTarget = panels[checkIndexW, checkIndexH];
					}
					
					// 調査先に電流が既に流れていたら調査しない
					if(newTarget.elecPow > 0) continue;
					
					// ジャミングが張られていたら調査しない
					int oldIndexW = 0, oldIndexH = 0, jamIndexW, jamIndexH;
					panels.GetIdByPanel(oldTarget, ref oldIndexW, ref oldIndexH);
					if(JammingSwitch.isJamming)
					{
						if(checkIndexW != oldIndexW) // 横移動
						{
							jamIndexW = (checkIndexW < oldIndexW) ? checkIndexW : oldIndexW;
							jamIndexH = oldIndexH;
							if(jammingMng.jammingDataLength[jamIndexW, jamIndexH] == 1)
								continue;
						}
						if(checkIndexH != oldIndexH) // 縦移動
						{
							jamIndexW = oldIndexW;
							jamIndexH = (checkIndexH < oldIndexH) ? checkIndexH : oldIndexH;
							if(jammingMng.jammingDataSide[jamIndexW, jamIndexH] == 1)
								continue;
						}
					}
					
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
		
		public void Draw()
		{
			particle.Draw ();
			
			if(visibleElecth == false) return;
			
			foreach(Electh elc in electh)
			{
				elc.Draw();
			}
		}
	}
}

