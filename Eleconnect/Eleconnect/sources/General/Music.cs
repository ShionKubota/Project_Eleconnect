//----------------------------------------
// 記入者：TakayukiNakanishi
// 制作日：13/09/26
// 更新日：13/10/01
// クラス：Music.cs
// コメント：サウンドクラス
// メモ---------------------------------
// BgmPlayerクラスは、１度の宣言でしか利用出来ない
// 重複すると、エラーを出したり、想定外の音が流れるので注意
// 配列化で同時再生は出来るかわからない(未検証)
// mp3
// Codec : MPEG Layer3
// Num Channels : 1 / 2
// Sampling Rate : 44100 / 48000 Hz
// Bitrate : 128-320 kbps
// supports VBR but there will be a restriction that Length / Seek cannot be used for VBR audio data. 
//----------------------------------------
using System;
using System.Collections.Generic;
using System.Threading;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;

namespace Eleconnect
{
	public class Music
	{	
		private Bgm bgm;				// BGMのデータ
		public static BgmPlayer player;		// BGMの再生クラス
		
		// コンストラクタ
		public Music(string bgmname)
		{
			bgm = new Bgm(bgmname);					// BGMのインスタンス
		}
		
		// サウンドBGMのセット
		public void Set(bool bgmloop, float volume, double time)
		{
			if(player != null) player.Dispose();
			player = bgm.CreatePlayer();			// BGM再生クラスにセット
			player.Loop = bgmloop;
			player.Volume = volume;
			player.Time = time;
			player.Play();
		}
		
		// 再生
		public void Play()
		{
			player.Play();						// BGMの再生
		}
		
		// サウンドBGMのストップ
		public void Stop()
		{
			player.Stop();
		}
		
		public void Pause()
		{
			player.Pause();
		}
		
		// サウンドBGM解放
		public void Term()
		{
			if(player != null)
			{
				player.Dispose();
			}
			bgm.Dispose();
		}
	}
}

