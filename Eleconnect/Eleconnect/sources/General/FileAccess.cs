//--------------------------------------------------------------
// クラス名： FileAccess.cs
// コメント： ファイルアクセスクラス
// 製作者　： MasayukiTada
// 制作日時： 2014/03/03
//--------------------------------------------------------------
using System;
using System.IO;

namespace Eleconnect
{
	public class FileAccess
	{		
		private byte[] data;
		private int size;
		private static FileAccess singleton;
		const string SAVE_FILE = "/Documents/";
		
		// コンストラクタ
		private FileAccess ()
		{
		}
		
		// データ書き込み
		// 書き込みたいデータをどんどん追加
		public void SavaData(string fileName, params int[] obj)
		{
			// 受けっとたデータを一つのstringに変換
			string outputData = "";
			/*
			foreach(int i in obj)
			{
				outputData += obj[i];
				if(i != obj.Length - 1) outputData += ",";
			}
			*/
			for(int i = 0; i < obj.Length; i++)
			{
				outputData += obj[i];
				if(i != obj.Length - 1) outputData += ",";
			}
			
			data = System.Text.Encoding.Unicode.GetBytes(outputData);	// 受け取ったデータをbyteに変換
			size = data.Length;											// 長さを取得
			
			// ファイルを開く
			FileStream output = File.Open(@SAVE_FILE + fileName, FileMode.OpenOrCreate);
			if(output != null)
			{
				output.SetLength((int)size);		
				output.Write(data,0,(int)size);	// 書き込み
				output.Close();					// ファイルを閉じる
			}
		}
		
		// データ読み込み
		public void LoadData(string fileName, ref string inputData)
		{
            FileStream input = File.Open(@SAVE_FILE + fileName, FileMode.OpenOrCreate);	// ファイルを開く
         	if (input != null)
			{
                long dataSize = input.Length;	// 呼び出した保存データの長さを取得
                data = new byte[dataSize];		// その長さ分の配列
                input.Read(data, 0, (int)dataSize);	// データロード
                size = (int)dataSize;				// ?
                inputData = System.Text.Encoding.Unicode.GetString(data);	// byteデータをstringに変換
                input.Close();		// ファイルを閉じる
			}
		}
		
		public static FileAccess GetInstance()
		{
			if(singleton == null) singleton = new FileAccess();
			return singleton;
		}	
	}
}

