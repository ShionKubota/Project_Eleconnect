using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class Electh
	{
		// 状態列挙
		public enum StateId
		{
			WAIT,
			FLOW,
			BURN,
			DEATH
		}
		
		private Sprite2D sp;
		private static Texture2D tex;
		public float speed{get; private set;}
		private int frameCnt;
		private int aniFrame;
		private Particles particle;
		public StateId state{get; private set;}
		public Panel target{get; private set;}
		
		public const float DEF_SPEED = 1.0f;
		
		public Electh (Panel panel, float speed)
		{
			if(tex == null)
				tex = new Texture2D(@"/Application/assets/img/electh.png", false);
			sp = new Sprite2D(tex);
			sp.pos = panel.GetPos();
			sp.textureUV = new Vector4(0.0f, 0.0f, 0.2f, 1.0f);
			sp.size = new Vector2(0.2f, 1.0f);
			target = panel;
			this.speed = speed;
			frameCnt = 0;
			aniFrame = 0;
			state = StateId.WAIT;
			
			// パーティクルの設定
			particle = new Particles(500);
			particle.LoadTextureInfo(@"Application/assets/img/particle.png", false);
			particle.pos = new Vector2(sp.pos.X, sp.pos.Y);
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
		
		public void Update()
		{
			particle.Update();
			frameCnt++;
			aniFrame += (frameCnt % 5) == 0 ? 1 : 0;
			sp.textureUV.X = (aniFrame % 4) * 0.2f;
			sp.textureUV.Z = sp.textureUV.X + 0.2f;
			
			switch(state)
			{
			// パネル上を流れる
			case StateId.FLOW:
				// 移動
				if( speed < Vector3.Length(target.GetPos() - sp.pos))
				{
					Vector3 move = Vector3.Normalize(target.GetPos() - sp.pos) * speed;
					sp.pos += move;
					
				}
				// 到着
				else
				{
					sp.pos = target.GetPos();
					state = StateId.WAIT;
					
					particle.pos = new Vector2(sp.pos.X, sp.pos.Y);
					particle.Generate(5);
					
					if(target.typeId == Panel.TypeId.JammSwitch)
					{
						JammingSwitch.isJamming = false;
						state = StateId.BURN;
					}
					// ゴールだったらどでかいパーティクル
					/*if(target.isGoal)
					{
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
					}*/
				}
				break;
			
			// 消滅
			case StateId.BURN:
				if(sp.color.W > 0)
					sp.color.W -= 0.01f;
				else
					state = StateId.DEATH;
				break;
			}
		}
		
		public void SetTarget(Panel panel)
		{
			state = StateId.FLOW;
			target = panel;
			this.speed += (this.speed < 5.0f) ? 0.3f : 0.0f;
		}
		
		public void Kill()
		{
			state = StateId.BURN;
		}
		
		public void Draw()
		{
			sp.Draw();
			particle.Draw ();
		}
	}
}

