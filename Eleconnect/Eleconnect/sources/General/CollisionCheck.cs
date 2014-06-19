//--------------------------------------------------------------
// クラス名： CollisionCheck.cs
// コメント： 衝突判定
// 製作者　： ShionKubota
// 制作日時： 2013/09/12
// 更新日時： 2013/09/12
//--------------------------------------------------------------
using System;
using Sce.PlayStation.Core ;

namespace Eleconnect
{
	public class CollisionCheck
	{
		// 3D空間における2点間の距離での当たり判定
		public static bool isHit(Vector3 pos1, Vector3 pos2, float distance)
		{
			Vector3 difVec = pos1 - pos2;
			float len = difVec.Length();
			
			if(len <= distance)
			{
				return true;
			}
			else
			{
				return false;
		
			}
		}
		
		// スプライトの矩形と、点の当たり判定
		public static bool isHit(Sprite2D sp, Vector2 point)
		{
			if(FMath.Abs(sp.pos.X - point.X) < sp.width / 2.0f &&
			   FMath.Abs(sp.pos.Y - point.Y) < sp.height / 2.0f)
				return true;
			
			return false;
		}
		
		/*
		public bool IsHit(TouchData touch, SpriteUV sprite)
		{
			Vector2 touchPos = GetTouchPos(touch);
			Vector2 spritePos = sprite.Position;
			float width = sprite.TextureInfo.Texture.Width;
			float height = sprite.TextureInfo.Texture.Height;
			return (
				touchPos.X > spritePos.X - width / 2 && 
				touchPos.X < spritePos.X + width / 2 && 
				touchPos.Y > spritePos.Y - height / 2 && 
				touchPos.Y < spritePos.Y + height / 2);
		}
		*/
	}
}

