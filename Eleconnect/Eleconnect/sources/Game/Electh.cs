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
		
		public Animation sp;
		private static Texture2D tex;
		public float speed{get; private set;}
		private int frameCnt;
		private int aniFrame;
		private float eleRotate;
		public StateId state{get; private set;}
		public Panel target{get; private set;}
		public static bool arrivedGoal;		// ゴールに到着したらtrue
		public static bool arrivedSwitch;
		
		public const float DEF_SPEED = 2.0f;
		public const float MAX_SPEED = 10.0f;
		
		private MusicEffect switchSE;
		private MusicEffect goalSE;
		
		public Electh (Panel panel, float speed)
		{
			if(tex == null)
				tex = new Texture2D(@"/Application/assets/img/electh.png", false);
			sp = new Animation(tex, new Vector2(1.0f / 6.0f, 0.5f),3,1,5,true,false,true);
			sp.pos = panel.GetPos();
			//sp.textureUV = new Vector4(0.0f, 0.0f, 0.2f, 1.0f);
			//sp.size = new Vector2(0.2f, 1.0f);
			//sp.size *= new Vector2(0.75f, 0.75f);
			target = panel;
			this.speed = speed;
			frameCnt = 0;
			aniFrame = 0;
			eleRotate = 0;
			state = StateId.WAIT;
			
			if(switchSE == null)
				switchSE = new MusicEffect(@"/Application/assets/se/Switch_SE.wav");
			if(goalSE == null)
				goalSE = new MusicEffect(@"/Application/assets/se/Switch_SE.wav");
			
		}
		
		public void Update()
		{
			frameCnt++;
			eleRotate++;
			aniFrame += (frameCnt % 5) == 0 ? 1 : 0;
			//sp.textureUV.X = (aniFrame % 4) * 0.2f;
			//sp.textureUV.Z = sp.textureUV.X + 0.2f;
			sp.angle = eleRotate;
			sp.Update();
			
			switch(state)
			{
			// パネル上を流れる
			case StateId.FLOW:
				// 移動
				if( speed*2.0f < Vector3.Length(target.GetPos() - sp.pos))
				{
					// ○押している間は早送り
					float moveSpeed = (Input.GetInstance().circle.isPush) ? speed * 2.0f : speed;
					Vector3 move = Vector3.Normalize(target.GetPos() - sp.pos) * moveSpeed;
					sp.pos += move;
				}
				// 到着
				else
				{
					sp.pos = target.GetPos();
					state = StateId.WAIT;
					
					// 接続数カウント
					++PlayData.GetInstance().connectNum;
					
					// スイッチだったら
					if(target.typeId == Panel.TypeId.JammSwitch)
					{
						switchSE.Set(1.0f,false);
						arrivedSwitch = true;
						Kill ();
					}
					// ゴールだったら
					if(target.isGoal)
					{
						arrivedGoal = true;
						goalSE.Set();
					}
				}
				break;
			
			// 消滅
			case StateId.BURN:
				/*
				if(sp.color.W > 0)
					sp.color.W -= 0.02f;
				else
					state = StateId.DEATH;
					*/
				if(sp.FrameNo == 12)
					state = StateId.DEATH;
				break;
				
			}
			
		}
		
		public void SetTarget(Panel panel)
		{
			state = StateId.FLOW;
			target = panel;
			this.speed += (this.speed < MAX_SPEED) ? 0.5f : 0.0f;
		}
		
		public void Kill()
		{
			Vector3 tempPos = sp.pos;
			sp.speed = 3;
			sp.Set(6, 13);
			state = StateId.BURN;
		}
		
		public void Draw()
		{
			sp.DrawAdd();
		}
	}
}

