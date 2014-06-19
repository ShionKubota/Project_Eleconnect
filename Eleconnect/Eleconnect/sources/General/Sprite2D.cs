//--------------------------------------------------------------
// クラス名： Sprite2D.cs
// コメント： ２Dスプライトクラス
// 製作者　： MasayukiTada
// 制作日時： 2013/09/25
// 更新日時： 2013/09/25
//--------------------------------------------------------------
using System;
using Sce.PlayStation.Core ;
using Sce.PlayStation.Core.Graphics ;

namespace Eleconnect
{
	public class Sprite2D
	{		
		protected static ShaderProgram shaderProgram;
		protected GraphicsContext graphics;
		protected ushort[] indices = {0, 1, 2, 3}; // インデックス
		protected Matrix4 screenMatrix;
		protected float[] vertices = new float[12];// 頂点座標
		
		// テクスチャ座標
		protected float[] texcoords = 
		{
			0.0f, 0.0f,	// left top
			0.0f, 1.0f,	// left bottom
			1.0f, 0.0f,	// right top
			1.0f, 1.0f,	// right bottom
		};
		
		// 頂点色
		protected float[] colors = 
		{
			1.0f,	1.0f,	1.0f,	1.0f,	// left top
			1.0f,	1.0f,	1.0f,	1.0f,	// left bottom
			1.0f,	1.0f,	1.0f,	1.0f,	// right top
			1.0f,	1.0f,	1.0f,	1.0f,	// right bottom
		};
		
		protected VertexBuffer vertexBuffer;					// 頂点バッファ
		protected Texture2D texture;							// 表示用テクスチャ
		
		public Vector3 pos;									// 表示位置
		public Vector4 color;								// カラー変更用
		public Vector4 textureUV;							// テクスチャUV値
		public Vector2 size;								// 画像のサイズ(割合)
		public Vector2 center;								// スプライトの中心座標を指定。0.0f～1.0fの範囲で指定してください
		public float angle;									// 表示角度
		protected float Width,Height;
		public float width{get { return Width * size.X;}} 	// スプライトの幅
		public float height{get { return Height * size.Y;}} // スプライトの高さ
		
		// コンストラクタ
		public Sprite2D(Texture2D texture)
		{
			if(texture == null) throw new Exception("ERROR: texture is null.");
			if(shaderProgram == null) shaderProgram = CreateSpriteShader();
			
			graphics = AppMain.graphics;
			graphics.SetShaderProgram(shaderProgram);
			
			this.texture = texture;		
			Width = texture.Width;
			Height = texture.Height;
			
			pos 		= Vector3.Zero;
			size 		= Vector2.One;
			color		= Vector4.One;
			textureUV 	= new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
			center		= new Vector2(0.5f, 0.5f);
			angle 		= 0.0f;
			
			//                                    頂点座標,             テクスチャ座標,        頂点色
			vertexBuffer = new VertexBuffer(4, 4, VertexFormat.Float3, VertexFormat.Float2, VertexFormat.Float4);
		}

		// 描画
		public void Draw()
		{
			SetMatrix();
			screenMatrix *= Matrix4.Translation(pos.X, pos.Y, pos.Z);
			screenMatrix *= Matrix4.RotationZ(FMath.Radians(angle));
			
			SetVertics();
			SetTextureUV();
			SetColor();
			
			
			AppMain.graphics.SetBlendFunc( BlendFuncMode.Add, BlendFuncFactor.SrcAlpha, BlendFuncFactor.OneMinusSrcAlpha ) ;
			vertexBuffer.SetVertices(0, vertices);
			vertexBuffer.SetVertices(1, texcoords);
			vertexBuffer.SetVertices(2, colors);
			vertexBuffer.SetIndices(indices);
			
			graphics.SetVertexBuffer(0, vertexBuffer);
			graphics.SetTexture(0, texture);
			
			shaderProgram.SetUniformValue(0, ref screenMatrix);
			graphics.DrawArrays(DrawMode.TriangleStrip, 0, 4);
		}
		
		public virtual void Term()
		{
			vertexBuffer.Dispose();
			texture = null;
			graphics = null;
		}
		
		// マトリックス初期化
		protected void SetMatrix()
		{
			screenMatrix = new Matrix4(
					 2.0f/graphics.Screen.Rectangle.Width,	0.0f,		0.0f, 0.0f,
					 0.0f,	 -2.0f/graphics.Screen.Rectangle.Height,	0.0f, 0.0f,
					 0.0f,	 0.0f, 1.0f, 0.0f,
					-1.0f, 1.0f, 0.0f, 1.0f
				);
		}
		
		// 頂点色の設定
		protected void SetColor()
		{
			for (int i = 0; i < colors.Length; i+=4)
			{
				colors[i] 	= color.R;
				colors[i+1] = color.G;
				colors[i+2] = color.B;
				colors[i+3] = color.A;
			}
		}
		
		protected void SetVertics()
		{
			vertices[0]  = -width * center.X;	// x0
			vertices[1]  = -height * center.Y;	// y0
			vertices[2]  = 0.0f;				// z0
			vertices[3]	 = -width * center.X;			// x1
			vertices[4]  = height * (1.0f - center.Y);	// y1
			vertices[5]  = 0.0f;						// z1
			vertices[6]  = width * (1.0f - center.X);		// x2
			vertices[7]  = -height * center.Y;				// y2
			vertices[8]  = 0.0f;							// z2
			vertices[9]  = width * (1.0f  -center.X);			// x3
			vertices[10] = height * (1.0f - center.Y);			// y3
			vertices[11] = 0.0f;								// z3
		}
		
		// UV(0.0f～1.0f)でテクスチャ座標を指定
		protected void SetTextureUV()
		{
			texcoords[0] = textureUV.X;	// left top u
			texcoords[1] = textureUV.Y;	// left top v
			texcoords[2] = textureUV.X;	// left bottom u
			texcoords[3] = textureUV.W;	// left bottom v
			texcoords[4] = textureUV.Z;	// right top u
			texcoords[5] = textureUV.Y;	// right top v
			texcoords[6] = textureUV.Z;	// right bottom u
			texcoords[7] = textureUV.W;	// right bottom v
		}	
		
		// シェーダープログラムの初期化
		protected static ShaderProgram CreateSpriteShader()
		{
			string resourceName = "Eleconnect.shaders.SimpleSprite.cgx";
			
			Byte[] dataBuffer = Utility.ReadEmbeddedFile(resourceName);
				
			ShaderProgram shaderProgram = new ShaderProgram(dataBuffer);
			shaderProgram.SetUniformBinding(0, "u_WorldMatrix");
	
			return shaderProgram;
		}
		
		// 加算合成描画
		public void DrawAdd()
		{				
			SetMatrix();
			screenMatrix *= Matrix4.Translation(pos.X, pos.Y, pos.Z);
			screenMatrix *= Matrix4.RotationZ(FMath.Radians(angle));
			
			SetVertics();
			SetTextureUV();
			SetColor();
			
			vertexBuffer.SetVertices(0, vertices);
			vertexBuffer.SetVertices(1, texcoords);
			vertexBuffer.SetVertices(2, colors);
			vertexBuffer.SetIndices(indices);
			
		
			// 加算合成
			AppMain.graphics.SetBlendFunc( BlendFuncMode.Add, BlendFuncFactor.SrcAlpha, BlendFuncFactor.One ) ;
		
			graphics.SetVertexBuffer(0, vertexBuffer);
			graphics.SetTexture(0, texture);
			shaderProgram.SetUniformValue(0, ref screenMatrix);
			graphics.DrawArrays(DrawMode.TriangleStrip, 0, 4);
		}
	}
}

