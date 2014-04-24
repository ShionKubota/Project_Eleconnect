//--------------------------------------------------------------
// クラス名： PlayData.cs
// コメント： プレイデータ管理クラス
// 製作者　： MasayukiTada
// 制作日時： 2014/03/03
//--------------------------------------------------------------
using System;

namespace Eleconnect
{
	public class PlayData
	{
		private static PlayData instance;	// 唯一のインスタンス
		
		// ゲームプレイのデータ
		public int stageNo;
		public int connectNum;
		
		// コンストラクタ
		private PlayData ()
		{
			Init ();
		}
		
		// 初期化
		public int Init()
		{
			stageNo = 0;
			connectNum = 0;
			return 0;
		}
		
		// インスタンスを取得
		public static PlayData GetInstance()
		{
			if(instance == null) instance = new PlayData();
			return instance;
		}
	}
}

