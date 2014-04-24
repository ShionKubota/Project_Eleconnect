//--------------------------------------------------------------
// クラス名： Scene.cs
// コメント： シーン基本ラス
// 製作者　： ShionKubota
// 制作日時： 2014/02/28
// 更新日時： 2014/02/28
//--------------------------------------------------------------
using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public class BaseScene
	{
		public int fadeInTime{ get; protected set; }		// フェードイン・アウトにかける時間
		public int fadeOutTime{ get; protected set; }
		public Vector3 fadeInColor{ get; protected set; }	// フェードイン・アウト時の色
		public Vector3 fadeOutColor{ get; protected set; }
		
		public const float DEFAULT_FOG_START = 830.0f;
		public const float DEFAULT_FOG_END   = 1230.0f;
		
		public BaseScene ()
		{
			fadeInTime = fadeOutTime = 50;
			fadeInColor = new Vector3(0.0f, 0.0f, 0.0f);
			fadeOutColor = new Vector3(0.0f, 0.0f, 0.0f);
			
		}
		
		// 初期化
		public virtual void Init(){}
		
		// 更新
		public virtual void Update(){}
		
		// 解放
		public virtual void Term(){}
		
		// 描画
		public virtual void Draw(){}
	}
}

