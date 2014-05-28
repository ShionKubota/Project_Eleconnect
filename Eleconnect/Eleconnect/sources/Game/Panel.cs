using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Eleconnect
{
	public abstract class Panel
	{
		// 各種定数
		public const float SIZE = 128.0f;	// テクスチャサイズ
		public const float SCALE = 0.4f;	// 倍率
		
		private const float ROTATE_SPEED = 0.4f;		// パネルの回転スピード
		private const float ROTATE_STOP_ANGLE = 1.0f;	// 目標の角度と現在の角度の差が、この数値以下になったら回転をストップ
		
		private const float BRIGHT_MINI = 0.0f;			// 光のパネルの不透明度の最低値
		private const float BRIGHT_CHANGE_SPEED = 0.1f;	// 明度変更時の変化スピード
		public static int elecPowMax = 81;	// パネルに流れる電力の最大値
		
		// 変数
		private Sprite2D sp;
		private Sprite2D lightSp;
		private static Texture2D[] tex = new Texture2D[5];
		private Sprite2D repeaterSp;
		private static Texture2D repeaterTex;
		
		private float rotateTo;
		public int rotateCnt{ private set; get; }	// 回転させた回数
		public int elecPow;			// このパネルに流れる電力
		public bool isRepeater;		// 電力が回復する特殊パネル
		public bool isGoal;			// ゴールとなるパネル
		public TypeId typeId{private set; get;}	// 現在のパネルのタイプ
		
		// パネルの種類列挙
		public enum TypeId
		{
			Straight,		// 直線
			RightAngle,		// 直角
			T,				// T字
			Cross,			// 十字
			JammSwitch		// ジャミングスイッチ
		}
		
		// パネルのルート情報
		public bool[] route = new bool[4];
		public struct DirId
		{
			public const int UP = 0;
			public const int RIGHT = 1;
			public const int DOWN = 2;
			public const int LEFT = 3;
		}
		
		// コンストラクタ
		public Panel (TypeId id, Vector2 pos)
		{
			Init (id, pos);
		}
		
		// 初期化
		public virtual void Init(TypeId id, Vector2 pos)
		{
			for(int i = 0; i < 4; i++)
			{
				if(tex[i] == null)
					tex[i] = new Texture2D(@"/Application/assets/img/paneru" + i + ".png", false);
			}
			
			switch(id)
			{
			case TypeId.Straight:
				//sp.textureUV = new Vector4(0.0f, 0.0f, 0.5f, 0.5f);
				route[DirId.UP] = route[DirId.DOWN] = true;
				route[DirId.LEFT] = route[DirId.RIGHT] = false;
				break;
				
			case TypeId.RightAngle:
				route[DirId.RIGHT] = route[DirId.DOWN] = true;
				route[DirId.UP] = route[DirId.LEFT] =  false;
				break;
				
			case TypeId.T:
				route[DirId.RIGHT] = route[DirId.LEFT] = route[DirId.DOWN] = true;
				route[DirId.UP] = false;
				break;
				
			case TypeId.Cross:
				route[DirId.RIGHT] = route[DirId.LEFT] = route[DirId.UP] = route[DirId.DOWN] = true;
				break;
			}
			
			sp =  new Sprite2D(tex[(int)id]);
			sp.pos = new Vector3(pos.X, pos.Y, 0.0f);
			sp.textureUV = new Vector4(0.0f, 0.0f, 0.5f, 1.0f);
			sp.size = new Vector2(SCALE / 2.0f, SCALE);
			sp.color = new Vector4(0.3f, 0.3f, 0.3f, 1.0f);
			
			lightSp = new Sprite2D(tex[(int)id]);
			lightSp.pos = sp.pos;
			lightSp.textureUV = new Vector4(0.5f, 0.0f, 1.0f, 1.0f);
			lightSp.size = sp.size;
			
			repeaterTex = new Texture2D(@"/Application/assets/img/ianzuma.png", false);
			repeaterSp =  new Sprite2D(repeaterTex);
			repeaterSp.pos = sp.pos;
			repeaterSp.size = new Vector2(0.2f, 0.2f);
			repeaterSp.color.W = 0.0f;
			
			isRepeater = false;
			isGoal = false;
			elecPow = 0;
			rotateCnt = 0;
			rotateTo = 0;
			typeId = id;
		}
		
		// 更新
		public virtual void Update()
		{
			// 回転
			sp.angle += (rotateTo - sp.angle) * ROTATE_SPEED;
			if(FMath.Abs(rotateTo - sp.angle) < ROTATE_STOP_ANGLE && rotateTo - sp.angle != 0.0f)
			{
				// 回転終了
				sp.angle = rotateTo;
				
				// 接続状況チェック
				PanelManager.CheckConnectOfPanels(0, 0);
			}
			
			// 光る
			Bright ();
			
			
			/*
			float targetBright = BRIGHT_MINI + (elecPow / (float)elecPowMax) * (1.0f - BRIGHT_MINI);	// 目標の明度
			float nowBright = sp.color.X;																// 現在の明度
			float newBright = nowBright + (targetBright - nowBright) * BRIGHT_CHANGE_SPEED;				// 新しい明度
			if(FMath.Abs (targetBright - newBright) < 0.01f) newBright = targetBright;
			sp.color = new Vector4(newBright, newBright, newBright, 1.0f);
			*/
			/*
			// リピーターのパネルは緑(光は黄色)にする
			if(isRepeater && repeaterSp.color.W < 1.0f)
			{
				repeaterSp.color.W += 0.1f;
			}
			*/
			// ゴールは赤く光る
			if(isGoal)
			{
				lightSp.color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
			}
		}
		
		// 光る
		protected void Bright()
		{
			// 現在の電力に合わせてボンヤリと光らせる(光スプライトの透明度を下げる)
			//float targetBright = BRIGHT_MINI + (elecPow / (float)elecPowMax) * (1.0f - BRIGHT_MINI);	// 目標の透明度
			float targetBright = (this.elecPow > 0) ? 1.0f : 0.0f;
			float nowBright = lightSp.color.W;															// 現在の透明度
			float newBright = nowBright + (targetBright - nowBright) * BRIGHT_CHANGE_SPEED;				// 新しい透明度
			if(FMath.Abs (targetBright - newBright) < 0.01f) newBright = targetBright;
			lightSp.color.W = newBright;
			
			// 光スプライトの同期
			lightSp.angle = sp.angle;
		}
		
		// 描画
		public void Draw()
		{
			sp.Draw();
			lightSp.Draw();
			repeaterSp.Draw();
		}
		
		// 解放
		public void Term()
		{
			for(int i = 0; i < 4; i++)
			{
				//tex[i].Dispose();
			}
		}
		
		// L,Rボタンを押した際のイベント
		public abstract void ButtonEvent(bool pushR);
		
		// グループにパネルを追加
		public virtual void AddPanel(TypeId id, Vector2 pos, int indexW, int indexH){}
		
		// 位置取得
		public Vector3 GetPos()
		{
			return sp.pos;
		}
		
		// 指定した方向に90°回転開始
		protected void Rotate(bool isClockwise)	// 時計回りならtrue
		{
			if(sp.angle % 90.0f != 0.0f) return;	// 非回転時のみ実行
			rotateTo = sp.angle + (isClockwise ? 90.0f : -90.0f);
			RotateRoute((isClockwise) ? 1 : -1);
			rotateCnt += (isClockwise) ? 1 : -1;	// 回転した回数の集計
		}
		// 90*num° 回転
		public void Rotate(int num)	// 時計回りならtrue
		{
			if(sp.angle % 90.0f != 0.0f) return;	// 非回転時のみ実行
			rotateTo = sp.angle + (num >= 0 ? 90.0f : -90.0f) * num;
			RotateRoute(num);
		}
		
		// 通路データだけを回転(+で時計回りに回転)
		private void RotateRoute(int rotation)
		{
			int rotateDir = (rotation > 0) ? -1 : 1;	// 時計回りなら-1がはいる
			int ROTATION = Math.Abs(rotation);			// 回転回数
			int startIndex = (rotateDir == -1) ? 3 : 0;	// ループの開始位置
			
			for(int i = 0; i < ROTATION; i++)
			{
				bool save = route[startIndex];
				for(int j = startIndex; ; j += rotateDir)
				{
					int next = j + rotateDir;
					if(next < 0 || next > 3)
					{
						route[j] = save;
						break;
					}
					else
					{
						route[j] = route[next];
					}
				}
			}
		}
		
		// 種類変更
		public void ChangeType(TypeId id)
		{
			Init (id, new Vector2(sp.pos.X, sp.pos.Y));
			PanelManager.CheckConnectOfPanels(0, 0);
		}
		
		// 色設定
		public void SetColor(Vector3 color)
		{
			sp.color = new Vector4(color.X, color.Y, color.Z, sp.color.W);
		}
		
		// 色を乗算
		public void MultiplyColor(Vector3 color)
		{
			sp.color *= new Vector4(color.X, color.Y, color.Z, 1.0f);
		}
	}
}

