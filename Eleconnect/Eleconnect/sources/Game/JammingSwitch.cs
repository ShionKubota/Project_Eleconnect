using System;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class JammingSwitch : Panel
	{
		private Texture2D tex;
		private static Sprite2D effectSp;	// OFFになった時の演出用スプライト
		
		private static bool _isJamming;
		private int aniFrame;
		private int frameCnt;
		
		// コンストラクタ
		public JammingSwitch (Vector2 pos) : base(TypeId.JammSwitch, pos)
		{
			//Init();
		}
		
		// 初期化
		public override void Init(TypeId id, Vector2 pos)
		{
			tex = new Texture2D(@"/Application/assets\img\jamm_anime.png", false);
			const float SWITCH_SCALE = 0.25f;
			sp = new Sprite2D(tex);
			sp.pos = new Vector3(pos.X, pos.Y, 0.0f);
			sp.size = new Vector2(SWITCH_SCALE / 5.0f, SWITCH_SCALE);
			sp.textureUV = new Vector4(0.0f, 0.0f, 1.0f/5.0f, 1.0f);
			
			effectSp = new Sprite2D(tex);
			effectSp.pos = sp.pos;
			effectSp.size = sp.size;
			effectSp.textureUV = sp.textureUV;
			effectSp.color.W = 0.0f;
			
			route[DirId.RIGHT] = route[DirId.LEFT] = route[DirId.UP] = route[DirId.DOWN] = true;
			
			isJamming = true;
			aniFrame = 0;
			frameCnt = 0;
		}
		
		// 更新
		public override void Update()
		{
			frameCnt++;
			if(isJamming)
			{
				aniFrame += (frameCnt % 5) == 0 ? 1 : 0;
				sp.textureUV.X = (aniFrame % 4) * 1.0f/5.0f;
				sp.textureUV.Z = sp.textureUV.X + 1.0f/5.0f;
			}
			else
			{
				sp.textureUV.X = 4.0f/5.0f;
				sp.textureUV.Z = sp.textureUV.X + 1.0f/5.0f;
				
			}
			
			// スイッチOFF演出
			if(effectSp.color.W > 0.0f)
			{
				effectSp.color.W -= 0.05f;
				effectSp.size    += new Vector2(0.2f) * new Vector2(1.0f / 5.0f, 1.0f);
			}
			else
			{
				effectSp.size = sp.size;
			}
			
		}
		
		// 描画
		public override void Draw()
		{
			sp.Draw ();
			effectSp.Draw();
		}
		
		// 解放
		public void Term()
		{
			//tex.Dispose();
			sp.Term();
		}
		
		// ジャミングのON/OFF
		public static bool isJamming
		{
			get
			{
				return _isJamming;
			}
			set
			{
				// スイッチOFFエフェクトを表示
				if( _isJamming == true && value == false)
				{
					effectSp.color.W = 1.0f;
				}
				_isJamming = value;
				
				
			}
		}
		
		public override void ButtonEvent (bool pushR)
		{
		}
	}
}

