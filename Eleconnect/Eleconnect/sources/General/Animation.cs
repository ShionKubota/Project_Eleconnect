//--------------------------------------------------------------
// クラス名： Animation.cs
// コメント： アニメーションクラス
// 製作者　： MasayukiTada
// 制作日時： 2014/03/10
//--------------------------------------------------------------
using System;
using System.Collections.Generic;
using Sce.PlayStation.Core ;
using Sce.PlayStation.Core.Graphics ;

namespace Eleconnect
{
	// アニメーションクラス
	public class Animation : Sprite2D
	{
		private enum ANIMATION_STATE{STAY, ONES, LOOP};	// アニメーションの状態
		private int speed;								// アニメーションスピード
		private int frameCnt;							// フレームカウンター
		private int frameNo;							// アニメーションナンバー
		private int frameFirst;							// 最初のフレーム
		private int frameLast;							// フレームの最大値
		private ANIMATION_STATE animationState;			// アニメーションの状態
		private bool isLoop;							// ループするかどうか
		private bool isFirst;							// 最初の画像に戻すかどうか
		private bool isVisibleStay;						// 停止中に表示するかどうか
		private List<Vector4> texUVList;				// 切り取り矩形を保存するリスト
		
		// 現在のアニメーションフレーム番号
		public int FrameNo
		{
			get{return frameNo;}
			set{
				frameNo = value;
				this.textureUV = texUVList[value];
			}
		}
		
		// コンストラクタ
		public Animation (Texture2D tex, Vector2 size, int speed, int frameFirst, int frameMax, bool isLoop, bool isFirst, bool isVisibleStay) : base(tex)
		{
			this.size 			= size;
			this.speed 			= speed;
			frameCnt			= 0;	
			this.frameNo		= 0;	
			this.frameFirst		= frameFirst;
			this.frameLast		= frameMax;
			this.isLoop			= isLoop;
			this.isFirst		= isFirst;
			this.isVisibleStay	= isVisibleStay;
			animationState		= (isLoop) ? ANIMATION_STATE.LOOP : ANIMATION_STATE.STAY;
			texUVList 			= new List<Vector4>();
			SetTextureUVList(width, height, tex.Width, tex.Height);			
		}
		
		// 切り取り矩形をListにセット
		private void SetTextureUVList(float x, float y, float width, float height)
		{
			// 切り取る数を計算
			int widthCnt = (int)(width / x);
			int heightCnt = (int)(height / y);
			
			// 切り取りながらリストに追加していく
			for(int j = 0;j < heightCnt;j++)
			{
				for(int i = 0;i < widthCnt;i++)
				{
					Vector4 texcoad = new Vector4();
					texcoad.X = (x * i) / width;
					texcoad.Y = (y * j) / height;
					texcoad.Z = texcoad.X + (x / width);
					texcoad.W = texcoad.Y + (y / height);
					texUVList.Add(texcoad);
				}
			}
			
			textureUV = texUVList[frameFirst];	// 最初のフレームを設定
		}
		
		// 更新
		public void Update()
		{
			frameCnt++;
			
			// アニメーションのコマを進める
			if(frameCnt % speed == 0)
			{
				// アニメーションの状態によって管理
				switch(animationState)
				{
				case ANIMATION_STATE.STAY:
						// 何もしない
					break;
				case ANIMATION_STATE.ONES:
					if (frameNo < frameLast)
	                {	
						textureUV = texUVList[FrameNo];						
					}
	                else
					{
						animationState = ANIMATION_STATE.STAY;
						if(isFirst)
						{
							// 最初のフレームに戻す
							textureUV = texUVList[frameFirst];
						}
	                }
	                frameNo++;	// アニメーションを進める
					break;
				case ANIMATION_STATE.LOOP:
					frameNo = (frameNo + 1) % (frameLast);
					textureUV = texUVList[FrameNo];
					break;
				}
			}
		}
		
		// 描画
		public void AnimationDraw()
		{
			if(animationState == ANIMATION_STATE.STAY && !isVisibleStay ){/*描画しない*/}
			else Draw();
		}
		
		// アニメーション再生
		public void Set()
		{
			Set (pos, 0, frameLast);
		}
		public void Set(int frameStart, int frameLast)
		{
			Set (pos, frameStart, frameLast);
		}
		public void Set(Vector3 pos, int frameStart, int frameLast)
		{
			this.pos = pos;
			frameCnt = 0;
			frameNo = frameStart;
			this.frameLast = frameLast;
			animationState = ANIMATION_STATE.ONES;
		}
		
		public override void Term()
		{
			vertexBuffer.Dispose();
			texture = null;
			graphics = null;
			int cnt  = texUVList.Count - 1;
			for(int i = cnt;i >= 0;i--)
			{
				texUVList.RemoveAt(i);
			}
		}
	}
}

