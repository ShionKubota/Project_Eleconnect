using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class ItemManager
	{
		public struct Item
		{
			public NormalPanel panel;
			public Vector3 basePos;
			public bool isGripped;	// ドラッグ中はtrue
			public bool isVisible;
		}
		private Item[] items;
		public const int ITEM_NUM = 4;
		
		private Sprite2D logoSp;
		private Texture2D logoTex;
		private PanelManager panels;
		private CursorOnPanels cursor;
		
		public static int itemUsed;	// アイテム使用時にスコアを減らすため
		
		// 初期化
		public ItemManager (PanelManager paMng, CursorOnPanels cOP)
		{
			items = new Item[ITEM_NUM];
			for(int i = 0; i < 4; i++)
			{
				// 2 * 2で正方形の形に並ぶように配置する
				float offset = 70.0f;	// 隙間
				items[i].basePos = new Vector3((i % 2.0f) * offset + 64.0f,
				                               (int)(i / 2.0f) * offset + 400.0f,
				                               0.0f);
				items[i].panel = new NormalPanel((Panel.TypeId)i, 										// パネル種類
				                                 new Vector2(items[i].basePos.X, items[i].basePos.Y));	// 位置
				items[i].panel.elecPow = 1;
				items[i].isVisible = true;
			}
			
			this.panels = paMng;
			this.cursor = cOP;
			
			logoTex = new Texture2D(@"/Application/assets/img/itemlogo.png", false);
			logoSp  = new Sprite2D(logoTex);
			logoSp.pos.X = (items[0].panel.GetPos().X + items[1].panel.GetPos().X) / 2.0f;
			logoSp.pos.Y = items[0].panel.GetPos().Y - 64.0f;
			logoSp.size = new Vector2(0.35f);
			
			itemUsed = 0;
		}
		
		// 更新
		public void Update()
		{
			for(int i = 0; i < ITEM_NUM; i++)
			{
				UpdateItem(ref items[i]);
			}
		}
		
		// アイテム更新
		private void UpdateItem(ref Item item)
		{
			if(item.isVisible == false) return;
			
			Input.TouchState touch = Input.GetInstance().touch[0];
			// 入力があるとき
			if(touch.isTouch)
			{
				
				// このアイテムにタッチされたら、掴まれた状態として扱う(2重キャッチ回避のため、他のアイテムのフラグはfalseに)
				if(touch.down)
				{
					if(CollisionCheck.isHit(item.panel.sp, new Vector2(touch.pos.X, touch.pos.Y)))	
					{
						for(int i = 0; i < ITEM_NUM; i++) items[i].isGripped = false;
						item.isGripped = true;
					}
				}
				
				// 掴まれている場合、その指に追従
				if(item.isGripped)
				{
					item.panel.moveSpeed = 0.3f;
					item.panel.moveTo = new Vector3(touch.pos.X, touch.pos.Y + CursorOnPanels.TOUCH_Y_OFFSET, 0.0f);
					//item.panel.sp.pos.X += (touch.pos.X 								  - item.panel.sp.pos.X) * 0.3f;
					//item.panel.sp.pos.Y += ((touch.pos.Y + CursorOnPanels.TOUCH_Y_OFFSET) - item.panel.sp.pos.Y) * 0.3f;
				}
			}
			
			// ドラッグしていたアイテムをドロップしたとき指が離れたとき
			if(touch.up && item.isGripped)
			{
				// どのパネルも選択していなければ（もしくはスイッチを選択してたら）、アイテムの掴み状態を解除
				if(cursor.notSelected || panels[cursor.indexW, cursor.indexH].typeId == Panel.TypeId.JammSwitch)
				{
					item.isGripped = false;
					item.panel.moveSpeed = 0.15f;
				}
				
				// パネルが選択されていたら、そこにパネルを当てはめる
				else
				{
					item.isVisible = false;
					item.panel.moveTo = panels[cursor.indexW, cursor.indexH].sp.pos;	// 位置を正す
					panels.Replace(cursor.indexW, cursor.indexH, item.panel);			// パネル置き換え
					PanelManager.CheckConnectOfPanels(GameScene.stage.startX, GameScene.stage.startY);
					
					itemUsed++;
					return;
				}
			}
			
			// 掴まれていないアイテムを初期値まで移動させる
			if(item.isGripped == false)
			{
				//Vector3 move = (item.basePos - item.panel.GetPos()) * 0.15f;
				//item.panel.sp.pos += move;
				item.panel.moveTo = item.basePos;
			}
			
			// パネルの更新
			item.panel.Update();
		}
		
		// 描画
		public void Draw()
		{
			for(int i = 0; i < ITEM_NUM; i++)
			{
				if(items[i].isVisible)
					items[i].panel.Draw();
			}
			logoSp.Draw();
		}
		
		// 解放
		public void Term()
		{
			for(int i = 0; i < ITEM_NUM; i ++)
			{
				items[i].panel.Term();
			}
			//logoTex.Dispose();
			logoSp.Term();
		}
	}
}

