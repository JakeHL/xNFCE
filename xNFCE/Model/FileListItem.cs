using System;
using Android.Util;
using System.IO;

namespace xNFCE
{
	public class FileListItem
	{

		// Public accessible file properties
		public string name, directory, type, size;
		double wSize;

		public FileListItem(string f)
		{

			// Get the name of the directory
			directory = Path.GetDirectoryName(f);

			// Determine if the item is a file or directory
			if(Directory.Exists(f))
			{
				// Get type, name and size
				DirectoryInfo di = new DirectoryInfo(f);
				type = "directory";
				name = di.Name;
				size = type;
			}
			if(File.Exists(f))
			{
				FileInfo fi = new FileInfo(f);
				type = "file";
				name = fi.Name;
				wSize = fi.Length;
				// Determine bytes, kilobytes or megabytes
				if(wSize < 1000)
				{
					size = wSize.ToString("G3") + " B";
				}
				else if(wSize < 1000000)
				{
					size = (wSize / 1024f).ToString("G3") + " KB";
				}
				else
				{
					size = ((wSize / 1024f) / 1024f).ToString("G3") + " MB" ;
				}
			}
		}
	}
}

