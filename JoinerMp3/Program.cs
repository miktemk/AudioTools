using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Mp3SplitterCommon;
using Miktemk;

namespace Mp3Joiner
{
	class JoinerMp3
	{
		static void Main(string[] args)
		{
			var firstArg = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
			if (args.Length > 0)
				firstArg = args.FirstOrDefault();

			// a directory was gragged in
			if (Directory.Exists(firstArg)) {
				new JoinerMp3(firstArg);
				return;
			}

			// otherwise a bunch of mp3s were gragged in
			var outFile = Path.GetDirectoryName(firstArg)
				+ Path.DirectorySeparatorChar
				+ Path.GetFileNameWithoutExtension(firstArg)
				+ ".concat"
				+ Path.GetExtension(firstArg);
			new JoinerMp3(outFile, args);
		}

		public JoinerMp3(string dirpath)
		{
			var files = Directory.GetFiles(dirpath, "*.mp3");
			DoStitch(dirpath + ".mp3", files);
		}

		public JoinerMp3(string outFile, string[] files) {
			DoStitch(outFile, files);
		}

		private void DoStitch(string outFile, string[] files)
		{
			SimpleLog.Reset();
			SimpleLog.LogIntro();
			SimpleLog.Log("Generating {0}", outFile);
			var result = new Mp3Composite(outFile);
			foreach (var fff in files) {
				SimpleLog.Log(fff);
				result.AppendAllOfFile(fff);
			}
			result.Close();
		}

		
	}
}
