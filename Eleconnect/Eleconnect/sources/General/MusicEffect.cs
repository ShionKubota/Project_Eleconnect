//----------------------------------------
// 記入者：TakayukiNakanishi
// 制作日：13/09/27
// 更新日：13/10/01
// クラス：MusicEffect.cs
// コメント：サウンドエフェクトクラス
// メモ-----------------------------------
// SoundPlayerクラスは重複して使おうとすると、別の音が流れるので、
// 配列化して使う方がよいかも
// wav
// Num Channels : 1 / 2
// Sampling Rate : 22050 / 44100 Hz
// Sampling Bit Depth : 16bit only
// Linear PCM only
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
	public class MusicEffect
	{
		private Sound sound;				// 効果音のデータ
		private SoundPlayer soundPlayer; 	// 効果音の再生クラス
		
		// コンストラクタ
		public MusicEffect (string seName)
		{
			sound = new Sound(seName);				// サウンドエフェクトのインスタンス
			soundPlayer = sound.CreatePlayer();		// サウンドエフェクト再生クラスにセット
		}
		
		// サウンドエフェクトセット
		public void Set()
		{
			Set (1.0f, false);
		}
		public void Set(float seVolume, bool seLoopflg)
		{
			soundPlayer.Loop = seLoopflg;
			soundPlayer.Volume = seVolume;
			soundPlayer.Play();
		}
		
		// サウンドエフェクト停止
		public void Stop()
		{
			soundPlayer.Stop();
		}
		
		// 解放
		public void Term()
		{
			Stop ();
			sound.Dispose();
			soundPlayer.Dispose();
		}
	}
}

