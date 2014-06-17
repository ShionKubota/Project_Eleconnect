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
		
		public Sprite2D sp;
		private static Texture2D tex;
		public float speed{get; private set;}
		private int frameCnt;
		private int aniFrame;
		public StateId state{get; private set;}
		public Panel target{get; private set;}
		public static bool arrivedGoal;		// ゴールに到着したらtrue
		
		public const float DEF_SPEED = 1.0f;
		
		public Electh (Panel panel, float speed)
		{
			if(tex == null)
				tex = new Texture2D(@"/Application/assets/img/electh_test.png", false);
			sp = new Sprite2D(tex);
			sp.pos = panel.GetPos();
			sp.textureUV = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
			sp.size = new Vector2(1.0f, 1.0f);
			sp.size *= new Vector2(0.75f, 0.75f);
			target = panel;
			this.speed = speed;
			frameCnt = 0;
			aniFrame = 0;
			state = StateId.WAIT;
			arrivedGoal = false;
			
			
		}
		
		public void Update()
		{
			frameCnt++;
			aniFrame += (frameCnt % 5) == 0 ? 1 : 0;
			//sp.textureUV.X = (aniFrame % 4) * 0.2f;
			//sp.textureUV.Z = sp.textureUV.X + 0.2f;
			
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
					
					// 接続数カウント
					int connectNum = ++PlayData.GetInstance().connectNum;
					int chargePar = (int)((float)connectNum / (float)(GameScene.stage.width * GameScene.stage.height) * 500.0f);
					Console.WriteLine ("CONNECT = " + connectNum);
					Console.WriteLine ("CHARGE = " + chargePar + "%");
					
					if(target.typeId == Panel.TypeId.JammSwitch)
					{
						JammingSwitch.isJamming = false;
						state = StateId.BURN;
					}
					// ゴールだったら
					if(target.isGoal)
					{
						arrivedGoal = true;
					}
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
			sp.DrawAdd();
		}
	}
}

