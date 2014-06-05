//--------------------------------------------------------------
// クラス名： Particles.cs
// コメント： ２Dパーティクル管理クラス(PSM標準装備のパーティクルが上手く動かないので作成)
// 製作者　： ShionKubota
// 制作日時： 2014/06/02
//--------------------------------------------------------------
using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class Particles
	{
		private List<Particle> particle;
		private Texture2D tex;
		private int frameCnt;
		
		public int particleMax;
		public Vector2 velocity;
		public Vector2 velocityVar;
		public float waitTime;			// パーティクル出現の間隔（秒）
		public float waitTimeRelVar;
		private float nextWaitTime;
		public float lifeSpan;			// パーティクル出現から消滅までの時間(秒)
		public float lifeSpanVar;
		public Vector2 pos;
		public Vector2 posVar;
		public Vector3 colorStart;
		public Vector3 colorStartVar;
		public Vector3 colorEnd;
		public Vector3 colorEndVar;
		public float scaleStart;
		public float scaleStartVar;
		public float scaleEnd;
		public float scaleEndVar;
		public float fade;		/* 0.0~1.0 */
		public float angle;
		public Vector2 gravity;
		public bool stopAutoGenerate;
		
		private Random rand;
		
		public Particles (int particleMax)
		{
			Init (particleMax);
		}
		
		// 全てのパラメータを初期値に戻す。
		public void Init(int particleMax)
		{
			particle = new List<Particle>();
			rand = new Random();
			
			this.particleMax 	= particleMax;
			velocity 			= new Vector2(0.0f, 0.0f);
			velocityVar 		= new Vector2(4.0f, 4.0f);
			waitTime 			= 0.5f;
			waitTimeRelVar 		= 0.0f;
			nextWaitTime		= GetRand() * waitTimeRelVar * waitTime;
			lifeSpan 			= 2.0f;
			lifeSpanVar 		= 0.0f;
			pos 				= Vector2.Zero;
			posVar 				= Vector2.Zero;
			colorStart 			= Vector3.One;
			colorStartVar 		= Vector3.Zero;
			colorEnd 			= Vector3.One;
			colorEndVar 		= Vector3.Zero;
			scaleStart 			= 1.0f;
			scaleStartVar 		= 0.0f;
			scaleEnd 			= 1.0f;
			scaleEndVar 		= 0.0f;
			fade 				= 0.1f;
			angle = 0.0f;
			gravity 			= Vector2.Zero;
			stopAutoGenerate 		= false;
			frameCnt = 0;
		}
		
		// 画像の読み込み
		public void LoadTextureInfo(string fileName, bool mipmap)
		{
			if(tex != null)
				tex.Dispose();
			
			tex = new Texture2D(fileName, mipmap);
		}
		
		// 更新
		public void Update()
		{
			//angle += FMath.Radians(0.1f);
			
			// パーティクルの発生
			if(frameCnt / 60.0f >= nextWaitTime && particle.Count < particleMax && !stopAutoGenerate)
			{
				Generate (1);
				frameCnt = 0;
				nextWaitTime = GetRand() * waitTimeRelVar * waitTime;
			}
			
			// パーティクルの更新
			for(int i = particle.Count - 1; i >= 0; i--)
			{
				particle[i].Update();
				particle[i].velocity += gravity;	// 重力を適用
				
				// 役目を終えたパーティクルをリストからはずす
				if(particle[i].isDead)
				{
					particle.RemoveAt(i);
				}
			}
			
			frameCnt++;
		}
		
		// 描画
		public void Draw()
		{
			foreach(Particle pa in particle)
			{
				pa.Draw ();
			}
		}
		
		// 解放
		public void Term()
		{
			tex.Dispose();
			for(int i = particle.Count - 1; i >= 0; i--)
				particle.RemoveAt(i);
		}
		
		// パーティクルを生成
		public void Generate(int num)
		{
			// パラメータを角度に合わせて回転(未実装)
			Vector2 newPosVar;
			newPosVar.X = posVar.X * FMath.Cos(angle) - posVar.Y * FMath.Sin(angle);
			newPosVar.Y = posVar.X * FMath.Sin(angle) + posVar.Y * FMath.Cos(angle);
			//Console.WriteLine ("x = " + newPosVar.X + " : y = " + newPosVar.Y);
			
			for(int i = 0; i < num; i++)
			{
				// パーティクルのセット
				particle.Add(new Particle(
					tex,
					pos 		+ posVar 		* GetRandVec2(),
					velocity 	+ velocityVar	* GetRandVec2(),
					colorStart 	+ colorStartVar * GetRandVec3(),
					colorEnd 	+ colorEndVar 	* GetRandVec3(),
					scaleStart 	+ scaleStartVar * GetRand(),
					scaleEnd 	+ scaleEndVar 	* GetRand(),
					lifeSpan 	+ lifeSpanVar 	* GetRand(),
					fade));
			}
		}
		
		// -1.0f ~ 1.0f までの数値をランダムに取得
		private float GetRand()
		{
			float f = (float)rand.NextDouble() * 2.0f - 1.0f;
			return f;
		
		}
		private Vector2 GetRandVec2(){ return new Vector2(GetRand(), GetRand ()); }
		private Vector3 GetRandVec3(){ return new Vector3(GetRand(), GetRand (), GetRand ()); }
	}
}

