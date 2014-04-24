using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class CursorOnPanels
	{
		// スプライト
		private Sprite2D sp;
		private Texture2D tex;
		
		// 音
		private MusicEffect musicEffect;
		
		// カーソル位置
		public int indexW{ private set; get; }
		public int indexH{ private set; get; }
		
		private int frameCnt;
		
		// 定数
		private const int FLASHING_SPEED = 5;	// 点滅の早さ
		private const float FLASHING_MEDIAN = 0.4f; // 点滅時の不透明度の基準値(中間の値)
		private const float FLASHING_AMPL = 0.3f;	// 点滅時の不透明度の変化のふり幅
		
		// コンストラクタ
		public CursorOnPanels (PanelManager panels)
		{
			Init (panels);
		}
		
		// 初期化
		private void Init(PanelManager panels)
		{
			// 初期化
			indexW = 0;
			indexH = 0;
			frameCnt = 0;
			
			tex = new Texture2D(@"/Application/assets/img/Cursor.png", false);
			sp = new Sprite2D(tex);
			sp.size = new Vector2(0.2f, 0.2f);
			sp.pos = panels[indexW, indexH].GetPos();
			
			musicEffect = new MusicEffect(@"/Application/assets/se/Move_SE.wav");
		}
		
		// 更新
		public void Update(PanelManager panels)
		{	
			Input input = Input.GetInstance();
			
			// カーソル移動
			if(input.left.isPushStart || input.right.isPushStart ||
			   input.up.isPushStart   || input.down.isPushStart)
			{
				musicEffect.Set(0.6f,false);
				indexW += (int)input.GetArrow().X;
				indexH += (int)input.GetArrow().Y;
				
				if(indexW < 0) indexW = GameScene.stageWidth - 1;
				if(indexH < 0) indexH = GameScene.stageHeight - 1;
				if(indexW >= GameScene.stageWidth) indexW = 0;
				if(indexH >= GameScene.stageHeight) indexH = 0;
				
				// 位置適用
				sp.pos = panels[indexW, indexH].GetPos();
			}
			
			// 点滅
			sp.color.W = FLASHING_MEDIAN + 
						 FMath.Sin(FMath.Radians(frameCnt * FLASHING_SPEED)) * FLASHING_AMPL;
			
			// 色が変わっていたら、通常に戻していく
			if(sp.color.X < 1.0f || sp.color.Y < 1.0f || sp.color.Z < 1.0f)
			{
				sp.color += new Vector4(0.01f, 0.01f, 0.01f, 0.0f);
			}
			
			frameCnt++;
		}
		
		// 一時的に色を変える
		public void ChangeColorTemporarily(Vector3 color)
		{
			sp.color = new Vector4(color.X, color.Y, color.Z, sp.color.W);
		}
		
		// 描画
		public void Draw()
		{
			sp.Draw ();
		}
		
		// 解放
		public void Term()
		{
			sp.Term();
			musicEffect.Term();
		}
	}//END OF CLASS
}

