using System;
using System.IO;
using System.Reflection;

using Sce.PlayStation.Core;

namespace Eleconnect
{
	public class Utility
	{
		static public Byte[] ReadEmbeddedFile(string embeddedFileName)
		{
			Assembly resourceAssembly = Assembly.GetExecutingAssembly();
			if (resourceAssembly.GetManifestResourceInfo(embeddedFileName) == null)
			{
				throw new FileNotFoundException("File not found.", embeddedFileName);
			}
	
			Stream fileStream = resourceAssembly.GetManifestResourceStream(embeddedFileName);
			Byte[] dataBuffer = new Byte[fileStream.Length];
			
			fileStream.Read(dataBuffer, 0, dataBuffer.Length);
			
			return dataBuffer;
		}
	}
}

