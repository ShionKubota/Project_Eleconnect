//--------------------------------------------------------------
// クラス名： Input.cs
// コメント： 入力情報管理クラス
// 製作者　： ShionKubota
// 制作日時： 2014/02/10
// 更新日時： 2014/02/10
//--------------------------------------------------------------
using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;

namespace Eleconnect
{
	public sealed class Input
	{
		// ボタン情報
		public class KeyState
		{
			public bool isPush;
			public bool isPushStart;
			public bool isPushEnd;
			public bool prevPush;
			public float holdingSeconds;
		}
		
		// タッチ情報
		public class TouchState
		{
			public bool isTouch;	// タッチしているか否か
			public bool down;		// タッチした瞬間true
			public bool up;			// 話した瞬間true
			public bool move;		// 動いている間true
			public Vector2 pos;		// タッチ位置
		}
		
		// 各種ボタン情・スティック情報
		public KeyState circle  { get; private set; }  // ○
		public KeyState cross   { get; private set; }  // ×
		public KeyState square  { get; private set; }  // □
		public KeyState triangle{ get; private set; }  // △
		public KeyState triggerR{ get; private set; }  // Rトリガー
		public KeyState triggerL{ get; private set; }  // Lトリガー
		public KeyState start  { get; private set; }   // START
		public KeyState select { get; private set; }   // SELECT
		public KeyState right{ get; private set; }     // 十字キー右
		public KeyState left { get; private set; }     // 十字キー左
		public KeyState up{ get; private set; }  	   // 十字キー上
		public KeyState down{ get; private set; }      // 十字キー下
		
		private Vector2  arrow;		// Vector2による十字キー
		private Vector2  analogR;	// アナログパッド右
		private Vector2  analogL;	// アナログパッド左
		public Vector2 GetArrow()  { return new Vector2(arrow.X, arrow.Y); }
		public Vector2 GetAnalogR(){ return new Vector2(analogR.X, analogR.Y); }
		public Vector2 GetAnalogL(){ return new Vector2(analogL.X, analogL.Y); }
		
		// タッチ情報
		public const int TOUCH_MAX = 5;
		public TouchState[] touch = new TouchState[TOUCH_MAX];
		
		public static GamePadData gamePadData;
		public static List<TouchData> touchDataList = Touch.GetData (0);
		
		private Input ()
		{
			circle = new KeyState();	// ○
			cross = new KeyState();		// ×
			square = new KeyState();	// □
			triangle = new KeyState();	// △
			triggerR = new KeyState();	// Rトリガー
			triggerL = new KeyState();	// Lトリガー
			start = new KeyState();		// START
			select = new KeyState();	// SELECT
			right = new KeyState();		// 十字キー右
			left = new KeyState();		// 左
			up = new KeyState();		// 上
			down = new KeyState();		// 下
			arrow = new Vector2();
			analogR = new Vector2();
			analogL = new Vector2();
			for(int i = 0; i < TOUCH_MAX; i++)
			{
				touch[i] = new TouchState();
			}
		}
		
		// 更新
		public void Update()
		{
			gamePadData = GamePad.GetData(0);
			touchDataList = Touch.GetData(0);
			
			// ボタン情報取得
			SetKeyState (circle, GamePadButtons.Circle);
			SetKeyState (cross, GamePadButtons.Cross);
			SetKeyState (square, GamePadButtons.Square);
			SetKeyState (triangle, GamePadButtons.Triangle);
			SetKeyState (triggerR, GamePadButtons.R);
			SetKeyState (triggerL, GamePadButtons.L);
			SetKeyState (start, GamePadButtons.Start);
			SetKeyState (select, GamePadButtons.Select);
			SetKeyState (right, GamePadButtons.Right);
			SetKeyState (left, GamePadButtons.Left);
			SetKeyState (up, GamePadButtons.Up);
			SetKeyState (down, GamePadButtons.Down);
			
			// 十字キー左右情報取得
			if     (right.isPush) arrow.X = 1;
			else if(left.isPush)  arrow.X = -1;
			else 				  arrow.X = 0;
			// 十字キー上下情報取得
			if     (down.isPush)  arrow.Y = 1;
			else if(up.isPush)    arrow.Y = -1;
			else 				  arrow.Y = 0;
			
			// アナログパッド情報取得
			analogR = new Vector2(gamePadData.AnalogRightX, 
				                  gamePadData.AnalogRightY); // 右アナログパッド
			analogL = new Vector2(gamePadData.AnalogLeftX, 
				                  gamePadData.AnalogLeftY);	 // 左アナログパッド
			
			// タッチ情報
			for(int i = 0; i < TOUCH_MAX; i++)
			{
				// タッチされていないとき
				if(touchDataList.Count <= i)
				{
					touch[i].isTouch = false;
					break;
				}
				
				// タッチされているとき
				touch[i].isTouch = true;
				touch[i].down = (touchDataList[i].Status == TouchStatus.Down);
				touch[i].up   = (touchDataList[i].Status == TouchStatus.Up);
				touch[i].move  = (touchDataList[i].Status == TouchStatus.Move);
				touch[i].pos = new Vector2(AppMain.ScreenWidth  / 2.0f + (AppMain.ScreenWidth  * touchDataList[i].X),
				                             AppMain.ScreenHeight / 2.0f + (AppMain.ScreenHeight * touchDataList[i].Y));
			}
		}
		
		// ボタン情報セット
		private void SetKeyState(KeyState keyState, GamePadButtons button)
		{
			// 前フレーム・現フレームのプッシュの可否をチェック
			keyState.prevPush = keyState.isPush;
			keyState.isPush = ((gamePadData.Buttons & button) != 0) ? true : false;
			
			keyState.isPushStart = (keyState.isPush && !keyState.prevPush) ? true : false; // 現フレームが押し始めた瞬間か否か
			keyState.isPushEnd = (!keyState.isPush && keyState.prevPush) ? true : false;   // 〃　押し終わった瞬間か否か
			
			// ボタンを押している長さを計測
			if(keyState.isPush)
			{
				keyState.holdingSeconds += 1.0f / 60.0f;	// 1/60秒足す
			}
			else
			{
				keyState.holdingSeconds = 0.0f;
			}
		}
		
		// 唯一のインタンス取得
		private static Input instance;
		public static Input GetInstance()
		{
			if(instance == null)
			{
				instance = new Input();
			}
			return instance;
		}
	}
}

