using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ACT.Core.Extensions
{
    public static class INIFile_Reader
    {
        static INIFile_Reader()
        {
            if (_first) { LoadCache(); _first = false; }
        }
        private static Dictionary<string, Dictionary<string, string>> _Cache = null;
        static bool _first = false;
        static void LoadCache()
        {
            _Cache = new Dictionary<string, Dictionary<string, string>>();

        }
        public static string ReadIniFileValue(this string FilePath, string Key, string SingleSection = null, bool ClearCache = false)
        {
            if (SingleSection == null) { SingleSection = "MARKALICZWASHERE2021"; }
            SingleSection += FilePath.Replace("\\", "").Replace("/", "").Replace("c:", "", StringComparison.InvariantCultureIgnoreCase);

            #region Cache
            if (ClearCache) { _Cache.Clear(); }
            else
            {
                if (_Cache.ContainsKey(SingleSection) == true)
                {
                    if (_Cache[SingleSection].ContainsKey(Key))
                    {
                        return _Cache[SingleSection][Key];
                    }
                }
            }
            #endregion

            IniFile iniFile = new IniFile(FilePath);
            if (iniFile.KeyExists(Key, SingleSection))
            {
                var _tmpVal = iniFile.Read(Key, SingleSection);
                if (_Cache == null) { _Cache = new Dictionary<string, Dictionary<string, string>>(); }
                _Cache.Add(SingleSection, new Dictionary<string, string>());
                _Cache[SingleSection].Add(Key, _tmpVal);
            }
            else
            {
                return null;
            }

            return _Cache[SingleSection][Key];
        }
        public static bool WriteCacheToINIFile(this string DestinationFile)
        {
            if (DestinationFile.FileExists() == false) { return false; }

            IniFile iniFile = new IniFile(DestinationFile);
            foreach (string Section in _Cache.Keys)
            {
                foreach (string Keys in _Cache[Section].Keys)
                {
                    iniFile.Write(Keys, _Cache[Section][Keys], Section);
                }
            }

            return true;
        }
        public static bool DeleteINIKey(bool FromCache, string Key, string FilePath, string SingleSection = null)
        {
            if (SingleSection == null) { SingleSection = "MARKALICZWASHERE2021"; }
            if (FilePath == null) { throw new Exception("File Must Exist First"); }

            SingleSection += FilePath.Replace("\\", "").Replace("/", "").Replace("c:", "", StringComparison.InvariantCultureIgnoreCase);


            if (FromCache)
            {
                if (_Cache.ContainsKey(SingleSection) == true)
                {
                    if (_Cache[SingleSection].ContainsKey(Key))
                    {
                        IniFile iniFilea = new IniFile(FilePath);
                        iniFilea.DeleteKey(Key, SingleSection);
                    }
                }
            }
            else
            {
                IniFile iniFile = new IniFile(FilePath);
                iniFile.DeleteKey(Key, SingleSection);
            }

            return true;
        }
        public static bool DeleteINISection(bool FromCache, string FilePath, string SingleSection = null)
        {
            if (SingleSection == null) { SingleSection = "MARKALICZWASHERE2021"; }
            if (FilePath == null) { throw new Exception("File Must Exist First"); }

            SingleSection += FilePath.Replace("\\", "").Replace("/", "").Replace("c:", "", StringComparison.InvariantCultureIgnoreCase);


            if (FromCache)
            {
                if (_Cache.ContainsKey(SingleSection) == true)
                {
                    IniFile iniFilea = new IniFile(FilePath);
                    iniFilea.DeleteSection(SingleSection);
                }
            }
            else
            {
                IniFile iniFile = new IniFile(FilePath);
                iniFile.DeleteSection(SingleSection);
            }
            return true;
        }

        private class IniFile   // revision 11
        {
            string Path;
            string EXE = Assembly.GetExecutingAssembly().GetName().Name;

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

            public IniFile(string IniPath = null)
            {
                Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
            }

            public string Read(string Key, string Section = null)
            {
                var RetVal = new StringBuilder(255);
                GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
                return RetVal.ToString();
            }

            public void Write(string Key, string Value, string Section = null)
            {
                WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
            }

            public void DeleteKey(string Key, string Section = null)
            {
                Write(Key, null, Section ?? EXE);
            }

            public void DeleteSection(string Section = null)
            {
                Write(null, null, Section ?? EXE);
            }

            public bool KeyExists(string Key, string Section = null)
            {
                return Read(Key, Section).Length > 0;
            }
        }
    }
}
