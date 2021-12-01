using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions._Logging
{
    public interface I_Logger
    {
        public void Log(string message, string additionalinfo, string[] stack, Exception ex);
    }

    public static class Log
    {
        static I_Logger? _ActiveLogger = null;
        static string? _LogPath = "";

        static List<string> _dlls = new List<string>();
        static string _baseDir = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat();
        public static string BaseDir { get { return _baseDir; } set { _baseDir = value; } }

        static Log()
        {
            _LogPath = BaseDir + "ApplicationLogs\\";
            List<string> _AllFilesInBasePath = (BaseDir + "Resources\\Plugins\\").GetAllFilesFromPath(true);
            foreach (string path in _AllFilesInBasePath.Where(x => x.ToLower().EndsWith(".dll"))) { _dlls.Add(path); }
        }

        static void SetActiveLogger()
        {

        }

        public static string? GetLogPath { get { return _LogPath; } }

        public static void LogError(string msg, string info, string[] stack, Exception ex)
        {
            if (_ActiveLogger == null) { SetActiveLogger(); }
            if (_ActiveLogger == null) { throw new Exception("Severe Error: Please Contact Administrator"); }
            _ActiveLogger.Log(msg, info, stack, ex);
        }

    }
}
