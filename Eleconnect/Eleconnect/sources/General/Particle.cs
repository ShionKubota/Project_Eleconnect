//--------------------------------------------------------------
// クラス名： Particle.cs
// コメント： ２Dパーティクル(1粒)クラス
// 製作者　： ShionKubota
// 制作日時： 2014/06/03
//--------------------------------------------------------------
using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class Particle
	{
		// 変数
		private Sprite2D sp;
		private int frameCnt;
		
		public Vector2 velocity;
		public float lifeSpan;			// パーティクル出現から消滅までの時間(秒)
		public Vector3 colorChange;
		public float scaleChange;
		public float fade;		/* 0.0~1.0 */
		
		public bool isDead{ private set; get; }
		
		public Particle (Texture2D tex, Vector2 pos, Vector2 velocity, Vector3 colorStart, Vector3 colorEnd, float scaleStart, float scaleEnd, float lifeSpan, float fade)
		{
			sp = new Sprite2D(tex);
			sp.pos = new Vector3(pos.X, pos.Y, 0.0f);
			sp.color = new Vector4(colorStart.X, colorStart.Y, colorStart.Z, 0.0f);
			sp.size = new Vector2(scaleStart);
			colorChange = (colorEnd - colorStart) / (lifeSpan * 60);
			scaleChange = (scaleEnd - scaleStart) / (lifeSpan * 60);
			this.velocity = velocity; 
			this.lifeSpan = lifeSpan;
			this.fade = fade;
			isDead = false;
		}
		
		// 更新処理
		public void Update()
		{
			sp.pos += new Vector3(velocity.X, velocity.Y, 0.0f);
			sp.color += new Vector4(colorChange.X, colorChange.Y, colorChange.Z, 0.0f);
			sp.size += scaleChange;
			
			// フェードイン
			if((float)frameCnt / 60.0f < lifeSpan*fade)
			{
				sp.color.W += 1.0f / (lifeSpan*fade * 60);
			}
			
			// フェードアウト
			if((float)frameCnt / 60.0f > lifeSpan - lifeSpan*fade)
			{
				sp.color.W -= 1.0f / (lifeSpan*fade * 60);
			}
			
			// 寿命がきたら消滅フラグを立てる
			if((float)frameCnt / 60.0f >= lifeSpan)
			{
				isDead = true;
			}
			
			frameCnt++;
		}
		
		// 描画
		public void Draw()
		{
			sp.Draw ();
		}
	}
}

