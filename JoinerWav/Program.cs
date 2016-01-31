using Miktemk;
using Mp3SplitterCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JoinerWav
{
    public class JoinerWav
    {
        static void Main(string[] args)
		{
			var firstArg = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
			if (args.Length > 0)
				firstArg = args.FirstOrDefault();

			// a directory was gragged in
			if (Directory.Exists(firstArg)) {
				new JoinerWav(firstArg);
				return;
			}

			// otherwise a bunch of wavs were gragged in
			var outFile = Path.GetDirectoryName(firstArg)
				+ Path.DirectorySeparatorChar
				+ Path.GetFileNameWithoutExtension(firstArg)
				+ ".concat"
				+ Path.GetExtension(firstArg);
			new JoinerWav(outFile, args);
		}

		public JoinerWav(string dirpath)
		{
			var files = Directory.GetFiles(dirpath, "*.wav");
			DoStitch(dirpath + ".wav", files);
		}

		public JoinerWav(string outFile, string[] files) {
			DoStitch(outFile, files);
		}

		private void DoStitch(string outFile, string[] files)
		{
			SimpleLog.Reset();
			SimpleLog.LogIntro();
			SimpleLog.Log("Generating {0}", outFile);
			var result = new WavComposite(outFile);
			foreach (var fff in files) {
				SimpleLog.Log(fff);
				result.AppendAllOfFile(fff);
			}
			result.Close();
		}
    }
}
