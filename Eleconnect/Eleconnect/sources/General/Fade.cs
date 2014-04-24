//--------------------------------------------------------------
// クラス名： Fade.cs
// コメント： フェードクラス
// 製作者　： ShionKubota
// 制作日時： 2014/02/28
// 更新日時： 2014/02/28
//--------------------------------------------------------------


using System ;
using Sce.PlayStation.Core ;
using Sce.PlayStation.Core.Graphics ;

namespace Eleconnect
{
	public class Fade
	{
		public enum StateId
		{
			NONE,
			IN,
			OUT,
			OUT_END
		}
		
		private Sprite2D fadeSprite;
		private Texture2D fadeTex;
		private float inSpeed, outSpeed;
		public StateId state{ get; private set;}
		
		public Fade ()
		{
			Init();
		}
		
		public void Init()
		{
			fadeTex = new Texture2D(@"/Application/assets/img/White.png", false);
			fadeSprite = new Sprite2D(fadeTex);	// スプライト作成
			fadeSprite.pos = AppMain.ScreenCenter;
			fadeSprite.size = new Vector2(AppMain.ScreenWidth, AppMain.ScreenHeight);
			fadeSprite.color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
			
			this.In ();
		}
		
		public void Update()
		{
			switch(state)
			{
			case StateId.NONE :
				break;
			case StateId.IN :
				if(fadeSprite.color.W > 0.0f) fadeSprite.color.W -= inSpeed;
				else state = StateId.NONE;
				break;
			case StateId.OUT :
				if(fadeSprite.color.W < 1.0f) fadeSprite.color.W += outSpeed;
				else state = StateId.OUT_END;
				break;
			case StateId.OUT_END :
				state = StateId.NONE;
				break;
			}
		}
		
		public void Term()
		{
			//fadeSprite.Term();
			fadeTex.Dispose();	// テクスチャ解放
		}
		
		public void Draw()
		{
			if(fadeSprite.color.W > 0.0f) fadeSprite.Draw();
		}
		
		public void In()
		{
			this.In(new Vector3(0.0f, 0.0f, 0.0f), 60);
		}
		public void In(Vector3 rgb, int time)
		{
			fadeSprite.color.X = rgb.X;
			fadeSprite.color.Y = rgb.Y;
			fadeSprite.color.Z = rgb.Z;
			fadeSprite.color.W = 1.0f;
			inSpeed = 1.0f / (float)time;
			state = StateId.IN;
		}
		
		public void Out()
		{
			this.Out(new Vector3(0.0f, 0.0f, 0.0f), 60);
		}
		public void Out(Vector3 rgb, int time)
		{
			fadeSprite.color = new Vector4(rgb.X, rgb.Y, rgb.Z, 0.0f);
			outSpeed = 1.0f / (float)time;
			state = StateId.OUT;
		}
	}
}

/*
namespace Eleconnect
{
	public class Fade
	{
		public Sprite2D fadeSprite;
		public Texture2D fadeTex;
		
		public enum State
		{
			NONE = 0, 
			FADE_IN = 1,
			FADE_OUT = 2
		}
		public int state;		// 移行するフェードの状態
		public int nowState;	// 現在のフェードの状態
		
		public Fade ()
		{
			Init();
		}
		
		public void Init()
		{
			fadeTex = new Texture2D(@"/Application/resources/img/White.png", false);
			fadeSprite = new Sprite2D(fadeTex);	// スプライト作成
			fadeSprite.pos = new Vector3(GearDrive.ScreenWidth/2.0f, GearDrive.ScreenHeight/2.0f, 0.0f);
			fadeSprite.size = new Vector2(GearDrive.ScreenWidth, GearDrive.ScreenHeight);
			fadeSprite.color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
			state = (int)State.NONE;
		}
		
		public void Update()
		{
			switch(state)
			{
			case (int)State.NONE :
				break;
			case (int)State.FADE_IN :
				nowState = (int)State.FADE_IN;
				if(fadeSprite.color.W > 0.0f) fadeSprite.color.W -= 0.02f;
				else state = (int)State.NONE;
				break;
			case (int)State.FADE_OUT :
				nowState = (int)State.FADE_OUT;
				if(fadeSprite.color.W < 1.0f) fadeSprite.color.W += 0.02f;
				else state = (int)State.NONE;
				break;
			}
		}
		
		public void Term()
		{
			//fadeSprite.Term();
			fadeTex.Dispose();	// テクスチャ解放
		}
		
		public void Draw()
		{
			fadeSprite.Draw();
		}
		
		public void FadeIn()
		{
			fadeSprite.color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
			state = (int)State.FADE_IN;
		}
		
		// 色変更
		public void FadeIn(Vector3 rgb)
		{
			fadeSprite.color.X = rgb.X;
			fadeSprite.color.Y = rgb.Y;
			fadeSprite.color.Z = rgb.Z;
			fadeSprite.color.W = 1.0f;
			state = (int)State.FADE_IN;
		}
		
		public void FadeOut()
		{
			fadeSprite.color = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
			state = (int)State.FADE_OUT;
		}
		
		public void FadeOut(Vector3 rgb)
		{
			fadeSprite.color.X = rgb.X;
			fadeSprite.color.Y = rgb.Y;
			fadeSprite.color.Z = rgb.Z;
			fadeSprite.color.W = 0.0f;
			state = (int)State.FADE_OUT;
		}
	}
}
*/

