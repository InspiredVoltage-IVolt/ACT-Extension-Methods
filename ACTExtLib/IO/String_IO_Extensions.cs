using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;

namespace ACT.Core.Extensions
{
    public static class String_FileIO
    {
        private static int RecursiveCopyCounter = 0;
        private static Dictionary<string, long> AsyncLongHolder = new Dictionary<string, long>();
        private static Dictionary<string, TimeSpan> AsyncTimeSpanHolder = new Dictionary<string, TimeSpan>();
        private static List<string> AsyncCleanupIDs = new List<string>();

        #region Fast Find Files

        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct WIN32_FIND_DATA
        {
            public int dwFileAttributes;
            public int ftCreationTime_dwLowDateTime;
            public int ftCreationTime_dwHighDateTime;
            public int ftLastAccessTime_dwLowDateTime;
            public int ftLastAccessTime_dwHighDateTime;
            public int ftLastWriteTime_dwLowDateTime;
            public int ftLastWriteTime_dwHighDateTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr FindFirstFile(string pFileName, ref WIN32_FIND_DATA pFindFileData);
        [DllImport("kernel32.dll")]
        private static extern bool FindNextFile(IntPtr hFindFile, ref WIN32_FIND_DATA lpFindFileData);
        [DllImport("kernel32.dll")]
        private static extern bool FindClose(IntPtr hFindFile);

        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private const int FILE_ATTRIBUTE_DIRECTORY = 16;

        public static int GetFileCount(this string dir, bool includeSubdirectories = false)
        {
            string searchPattern = Path.Combine(dir, "*");

            var findFileData = new WIN32_FIND_DATA();
            IntPtr hFindFile = FindFirstFile(searchPattern, ref findFileData);
            if (hFindFile == INVALID_HANDLE_VALUE)
            {
                throw new Exception("Directory not found: " + dir);
            }

            int fileCount = 0;
            do
            {
                if (findFileData.dwFileAttributes != FILE_ATTRIBUTE_DIRECTORY)
                {
                    fileCount++;
                    continue;
                }

                if (includeSubdirectories && findFileData.cFileName != "." && findFileData.cFileName != "..")
                {
                    string subDir = Path.Combine(dir, findFileData.cFileName);
                    fileCount += GetFileCount(subDir, true);
                }
            }
            while (FindNextFile(hFindFile, ref findFileData));

            FindClose(hFindFile);

            return fileCount;
        }

        #endregion

        /// <summary>Reads all the text from the File Path</summary>
        /// <param name="FilePath"></param>
        /// <returns>Null Means File Doesnt Exist, Empty is EmptyFile or Other Permission Errors, No Exceptions Thrown</returns>
        public static string ReadAllText(this string FilePath)
        {
            var _tmpReturn = String.Empty;
            if (!File.Exists(FilePath)) { return null; }

            try { _tmpReturn = File.ReadAllText(FilePath); }
            catch { return _tmpReturn; }

            return _tmpReturn;
        }

        /// <summary>
        /// Tries to copy a file from one location to another using a FileShare.ReadWrite operation.
        /// On error the length is 0 and the TimeSpan is set to MAX.  Errors are logged when VerboseDebugging = true;
        /// </summary>
        /// <param name="SourceFilePath">Source File Path</param>
        /// <param name="DestinationFilePath">Destination File Path</param>
        /// <param name="progress">Progress Info</param>
        /// <returns>(long, TimeSpan) or (TotalBytesWritten,Time To Do It)</returns>
        public static Task CopyFile_Passivly(
          this string SourceFilePath,
          string DestinationFilePath,
          IProgress<(long, TimeSpan)> progress)
        {
            return new Task(() =>
            {
                DateTime now = DateTime.Now;
                try
                {
                    using (BinaryReader binaryReader = new BinaryReader(new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(DestinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None)))
                        {
                            byte[] buffer = new byte[2048];
                            while (binaryReader.Read(buffer, 0, buffer.Length) > 0)
                            {
                                binaryWriter.Write(buffer);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //TODO LOG
                }
            });
        }

        /// <summary>Calculate the Directory Size Using a Parallel Loop</summary>
        /// <param name="SourcePath">Directory Path To Calculate</param>
        /// <returns>(int Count, long TotalSize)</returns>
        public static (int, long) CalculateDirectorySize(this string SourcePath)
        {
            FileInfo[] files = new DirectoryInfo(SourcePath).GetFiles("*", SearchOption.AllDirectories);
            long _tmpTotal = 0;
            Parallel.ForEach<FileInfo>(files, currentFile => Interlocked.Add(ref _tmpTotal, currentFile.Length));
            return (((IEnumerable<FileInfo>)files).Count<FileInfo>(), _tmpTotal);
        }

        /// <summary>Reads all the text from the File Path</summary>
        /// <param name="FilePath"></param>
        /// <param name="FileData"></param>
        /// <returns></returns>
        public static bool SaveAllText(this string FileData, string FilePath)
        {
            try
            {
                File.WriteAllText(FilePath, FileData);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Copys All The Files From The Source To The Destination -Mark
        /// </summary>
        /// <param name="SourceDir">Source Directory</param>
        /// <param name="DestinationDir">Destination Directory</param>
        /// <returns>Total Files Copied</returns>
        public static int CopyFilesRecursively(this string SourceDir, string DestinationDir)
        {
            String_FileIO.RecursiveCopyCounter = 0;
            String_FileIO.CopyFilesRecursively(new DirectoryInfo(SourceDir), new DirectoryInfo(DestinationDir));
            return String_FileIO.RecursiveCopyCounter;
        }

        /// <summary>Eventually Move This To ACT Foo</summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo directory in source.GetDirectories())
            {
                String_FileIO.CopyFilesRecursively(directory, target.CreateSubdirectory(directory.Name));
            }

            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                ++String_FileIO.RecursiveCopyCounter;
            }
        }

        /// <summary>
        /// Attempts To Delete A File.  Waits For It To Complete.  Throws Error On Lock or Other Issue.
        /// </summary>
        /// <remarks>   Mark Alicz, 12/17/2016. </remarks>
        /// <param name="FileToDelete"> Full File Path To Delete. </param>
        /// <param name="MaxWaitTime">  (Optional) the maximum wait time. </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
        public static bool DeleteFile(this string FileToDelete, int MaxWaitTime = 2000)
        {
            int num = 0;
            FileInfo fileInfo = new FileInfo(FileToDelete);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                fileInfo.Refresh();
                while (fileInfo.Exists)
                {
                    Thread.Sleep(100);
                    fileInfo.Refresh();
                    num += 100;
                    if (num > MaxWaitTime)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>Delete All Files From Directory Optionally Recursive</summary>
        /// <param name="Directory"></param>
        /// <returns></returns>
        public static Dictionary<string, bool> DeleteAllFilesFromDirectory(
          this string Directory,
          bool Recursive = false)
        {
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            if (Recursive)
            {
                foreach (string str in Directory.GetAllFilesFromPath(true))
                {
                    dictionary.Add(str, str.DeleteFile(2000));
                }
            }
            else
            {
                foreach (string str in Directory.GetAllFilesFromPath(false))
                {
                    dictionary.Add(str, str.DeleteFile(2000));
                }
            }
            return dictionary;
        }

        /// <summary>Internal get All Files From Path</summary>
        /// <param name="BaseDirectory"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static List<string> _getallfilesfrompath(string BaseDirectory, List<string> data, string RegexPattern = "")
        {
            Regex _Expression = null;

            if (!RegexPattern.NullOrEmpty()) { _Expression = new Regex(RegexPattern); }

            if (!Directory.Exists(BaseDirectory.EnsureDirectoryFormat())) { return data; }

            var files = Directory.GetFiles(BaseDirectory).Where(x => (_Expression.IsMatch(x) || _Expression == null));

            data.AddRange(files);

            foreach (string directory in Directory.GetDirectories(BaseDirectory))
            {
                data = String_FileIO._getallfilesfrompath(directory, data);
            }

            return data;
        }

        /// <summary>Get All Files From Base Directory</summary>
        /// <param name="BaseDirectory">Base Directory</param>
        /// <param name="Recursive">Get Files In Sub Folders</param>
        /// <returns>List{string} All Files Found</returns>
        public static List<string> GetAllFilesFromPath(this string BaseDirectory, bool Recursive) => Recursive ? String_FileIO.GetAllFilesFromPath(BaseDirectory, Recursive) : ((IEnumerable<string>)Directory.GetFiles(BaseDirectory)).ToList<string>();

        /// <summary>Get All Files From Base Directory</summary>
        /// <param name="BaseDirectory">Base Directory</param>
        /// <param name="FileNamePattern">This looks at the FileName only.  Use * as wildcards</param>
        /// <param name="Recursive">Get Files In Sub Folders</param>
        /// <returns>List{string} All Files Found</returns>
        public static List<string> GetAllFilesFromPath(this string BaseDirectory, string RegexPattern, bool Recursive) => Recursive ? String_FileIO.GetAllFilesFromPath(BaseDirectory, RegexPattern, Recursive) : ((IEnumerable<string>)Directory.GetFiles(BaseDirectory)).ToList<string>();

        /// <summary>
        /// Attempts To Delete A File.  Waits For It To Complete.  Throws Error On Lock or Other Issue.
        /// </summary>
        /// <remarks>   Mark Alicz, 12/17/2016. </remarks>
        /// <exception cref="T:System.Exception">    Thrown when an exception error condition occurs. </exception>
        /// <param name="FileToDelete"> Full File Path To Delete. </param>
        public static void DeleteFile(this string FileToDelete, short MaxTries = 50, bool throwError = false)
        {
            int num = 0;
            FileInfo fileInfo = new FileInfo(FileToDelete);
            if (!fileInfo.Exists)
            {
                return;
            }

            fileInfo.Delete();
            fileInfo.Refresh();
            while (fileInfo.Exists)
            {
                Thread.Sleep(100);
                fileInfo.Refresh();
                ++num;
                if (num > MaxTries)
                {
                    throw new Exception("Error Deleting File");
                }
            }
        }

        /// <summary>File Size</summary>
        /// <param name="FileName"></param>
        /// <returns>-1 File Doesnt Exist, -2 Other General Error</returns>
        public static long GetFileSize(this string FileName)
        {
            try
            {
                return !FileName.FileExists() ? 0L : new FileInfo(FileName).Length;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// FindFile and return the Path Optionally Searches Sub Folder
        /// </summary>
        /// <param name="path">Path to Start In</param>
        /// <param name="fileName">Name Of The File</param>
        /// <param name="searchSubFolders">Search Sub Folders Or Not</param>
        /// <returns></returns>
        public static string FindFileReturnPath(
          this string path,
          string fileName,
          bool searchSubFolders = true)
        {
            if (!Directory.Exists(path))
            {
                return "";
            }

            if (Directory.EnumerateFiles(path).Any<string>(x => x.GetFileNameFromFullPath().ToLower() == fileName.ToLower()))
            {
                return path;
            }

            if (!searchSubFolders)
            {
                return "";
            }

            foreach (string directory in Directory.GetDirectories(path))
            {
                string fileReturnPath = directory.FindFileReturnPath(fileName, searchSubFolders);
                if (fileReturnPath != "")
                {
                    return fileReturnPath;
                }
            }
            return "";
        }

        /// <summary>Searches for all references of the specified file</summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <param name="searchSubFolders"></param>
        /// <returns></returns>
        public static List<string> FindAllFileReferencesInPath(
          this string path,
          string filename,
          bool searchSubFolders = true)
        {
            return path.GetAllFilesFromPath(searchSubFolders).Where<string>(x => x.ToLower().GetFileNameFromFullPath() == filename.ToLower()).ToList<string>();
        }

        /// <summary>Get a Memory Stream From a File Path</summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static MemoryStream GetMemoryStream(this string Path)
        {
            using (FileStream fileStream = new FileStream(Path, FileMode.Open))
            {
                MemoryStream memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                return memoryStream;
            }
        }
    }

    public static class String_IO_Extensions
    {
        /// <summary>Converts a String to a MemoryStream.</summary>
        /// <param name="str">
        /// </param>
        /// <returns>
        /// </returns>
        public static Stream ToStream([NotNull] this string str) => new MemoryStream(Encoding.ASCII.GetBytes(str));

        /// <summary>Gets A File Name From A Full Path Name</summary>
        /// <param name="x">full Path</param>
        /// <param name="includeExtension">Include Or Exclude The Extension</param>
        /// <returns>string - File Name</returns>
        public static string GetFileName(this string x, bool includeExtension) => includeExtension ? x.GetFileNameFromFullPath() : x.GetFileNameWithoutExtension();

        /// <summary>Ensures the FileName passed in is Valid</summary>
        /// <param name="x">Either A Full Path To The FileName or The FileName ItSelf</param>
        /// <param name="ReplaceMentCharacter">Replacement Character to Replace</param>
        /// <returns></returns>
        public static string EnsureValidWindowsFileName(this string x, string ReplaceMentCharacter = "")
        {
            string str = x.GetFileName(true);
            string fromFileLocation = x.GetDirectoryFromFileLocation();
            foreach (char invalidFileNameChar in Path.GetInvalidFileNameChars())
            {
                str = str.Replace(invalidFileNameChar.ToString(), ReplaceMentCharacter);
            }

            return fromFileLocation.Length < 2 ? str : fromFileLocation + str;
        }

        /// <summary>Checks to see if the File Exists</summary>
        /// <param name="x">Full Path Name To File.  i.e. c:\test\test.txt</param>
        /// <returns>true / false</returns>
        public static bool FileExists(this string x) => File.Exists(x);

        /// <summary>Checks If The File Exists.</summary>
        /// <param name="DirectoryPath">Directory To The File</param>
        /// <param name="FileName">Actual File Name</param>
        /// <returns>true / false</returns>
        public static bool FileExists(this string DirectoryPath, string FileName) => (DirectoryPath.EnsureDirectoryFormat() + FileName).FileExists();

        /// <summary>Moves Up a Directory [Count] Directories.</summary>
        /// <param name="StartingDirectoryPath">Starting Directory</param>
        /// <param name="Count">Number Of Parents Up To Navigate To</param>
        /// <param name="Validate">Validate The Information - throwing and error</param>
        /// <returns>New Directory Path</returns>
        public static string NavigateUpDirectory(this string StartingDirectoryPath, int Count, bool Validate)
        {
            string x = !Validate || StartingDirectoryPath.DirectoryExists() ? StartingDirectoryPath.EnsureDirectoryFormat() : throw new DirectoryNotFoundException(StartingDirectoryPath);
            for (int index = 0; index <= Count; ++index)
            {
                x = x.Substring(0, x.LastIndexOf("\\"));
            }

            return x.EnsureDirectoryFormat();
        }

        /// <summary>
        /// Checks To See if the Directory Exists,  Optional Parameter To Create If It Doesnt
        /// </summary>
        /// <param name="DirectoryPath">Directory Path</param>
        /// <param name="ForceCreate">Create The Directory when It is not found</param>
        /// <returns>True On Success, False On Failure</returns>
        public static bool DirectoryExists(this string DirectoryPath, bool ForceCreate)
        {
            if (DirectoryPath.DirectoryExists())
            {
                return true;
            }

            if (!ForceCreate)
            {
                return false;
            }

            try
            {
                DirectoryPath.EnsureDirectoryFormat().CreateDirectoryStructure();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Checks If The Directory Exists</summary>
        /// <param name="x">Directory Path</param>
        /// <returns>true / false</returns>
        public static bool DirectoryExists(this string x)
        {
            if (!x.Contains("\\"))
            {
                x = x.EnsureDirectoryFormat();
            }

            x = x.EnsureDirectoryFormat();
            return Directory.Exists(x);
        }

        /// <summary>
        /// Create The Directory Structure Specified In The Parameter
        /// </summary>
        /// <param name="x">Directory Structure To Create</param>
        public static void CreateDirectoryStructure(this string x, DirectorySecurity d = null)
        {
            string[] strArray = x.Contains("\\") ? x.SplitString("\\", StringSplitOptions.RemoveEmptyEntries) : throw new Exception("Invalid Path Contains No Directory Format.");
            if (strArray.Length == 0)
            {
                throw new Exception("Invalid Path Info");
            }

            string x1 = strArray[0].DirectoryExists() ? strArray[0] : throw new Exception("Root Path Does Not Exist: (" + strArray[0] + ")");
            for (int index = 1; index < strArray.Length; ++index)
            {
                x1 = x1 + "\\" + strArray[index];
                if (!x1.DirectoryExists())
                {
                    try
                    {
                        if (d == null)
                        {
                            try
                            {
                                Directory.CreateDirectory(x1.EnsureDirectoryFormat());
                            }
                            catch (Exception ex)
                            {
                                //TODO
                                //ErrorLogger.LogError("ACT.Core.Extensions.CreateDirectoryStructure", "Error Creating Directory Without Security.", x, ex, ErrorLevel.Critical, 221);
                                //throw new Exception("Error Creating Directory. Please Check Log");
                            }
                        }
                        else
                        {
                            try
                            {
                                Directory.CreateDirectory(x1.EnsureDirectoryFormat());
                            }
                            catch (Exception ex)
                            {
                                // TODOa
                                //ErrorLogger.LogError("ACT.Core.Extensions.CreateDirectoryStructure", "Error Creating Directory WITH Security.", x + d.ToString(), ex, ErrorLevel.Critical, 233);
                                //throw new Exception("Error Creating Directory With Security. Please Check Log");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// Ensures The String Ends With \.  i.e. "c:\test\testdir" -&gt; "c:\test\testdir\"
        /// </summary>
        /// <param name="x">Directory Path</param>
        /// <returns>Formatted Directory Path</returns>
        public static string EnsureDirectoryFormat(this string x, bool WindowsFormat = true) => WindowsFormat ? (x.EndsWith("\\") || x.EndsWith("/") ? x.Replace("/", "\\") : x.Replace("/", "\\") + "\\") : (x.EndsWith("\\") || x.EndsWith("/") ? x.Replace("/", "\\") : x.Replace("/", "\\") + "\\");

        /// <summary>
        /// Returns the Directory Path From The Full FileName Path
        /// </summary>
        /// <param name="x">Directory With File Name</param>
        /// <returns>Directory Part Of The Full Path</returns>
        public static string GetDirectoryFromFileLocation(this string x)
        {
            string str = x;
            if (!str.Contains("\\"))
            {
                return "";
            }

            if (str.Contains("//"))
            {
                try
                {
                    str = str.Substring(0, str.LastIndexOf("//")).EnsureDirectoryFormat();
                }
                catch
                {
                }
            }
            if (str.Contains("\\"))
            {
                try
                {
                    str = str.Substring(0, str.LastIndexOf("\\")).EnsureDirectoryFormat();
                }
                catch
                {
                }
            }
            return str;
        }

        /// <summary>
        /// Gets the Last Directory Name From A Valid Directory Path
        /// </summary>
        /// <param name="x">Valid Directory Name</param>
        /// <returns></returns>
        public static string GetDirectoryName(this string x)
        {
            string x1 = x;
            if (!x.Contains("\\"))
            {
                return x1;
            }

            if (x.Contains("//"))
            {
                string str = x1.TrimEnd("//");
                x1 = str.Substring(str.LastIndexOf("//")).Trim("//").Trim();
            }
            if (x1.Contains("\\"))
            {
                string str = x1.TrimEnd("\\");
                x1 = str.Substring(str.LastIndexOf("\\")).Trim("\\").Trim();
            }
            return x1;
        }

        /// <summary>
        /// Returns the File Name Without The Extension From a Full File Path
        /// </summary>
        /// <param name="x"></param>
        /// <returns>File Name - With No Extensions</returns>
        public static string GetFileNameWithoutExtension(this string x)
        {
            string x1 = x;
            if (!x1.Contains("."))
            {
                return x1;
            }

            string nameFromFullPath = x1.GetFileNameFromFullPath();
            return nameFromFullPath.Substring(0, nameFromFullPath.LastIndexOf("."));
        }

        /// <summary>
        /// Returns the File Name From The Full File Path (Includes Network Paths)
        /// </summary>
        /// <param name="x">Full File Path</param>
        /// <returns>FileName With Extension</returns>
        public static string GetFileNameFromFullPath(this string x)
        {
            string x1 = x;
            if (!x1.Contains("\\"))
            {
                return x1;
            }

            if (x1.Contains("\\"))
            {
                x1 = x1.EnsureDirectoryFormat().Substring(x1.LastIndexOf("\\") + 1).TrimEnd("\\");
            }
            else if (x1.Contains("//"))
            {
                x1 = x1.EnsureDirectoryFormat().Substring(x1.LastIndexOf("//") + 1).TrimEnd("//");
            }

            return x1;
        }

        /// <summary>Returns the Extension From The FileName</summary>
        /// <param name="x">FileName</param>
        /// <returns>File Extension - Without the Period</returns>
        public static string GetExtensionFromFileName(this string x)
        {
            string x1 = x;
            if (!x1.Contains("."))
            {
                return x1;
            }

            try
            {
                x1 = ((IEnumerable<string>)x1.SplitString(".", StringSplitOptions.RemoveEmptyEntries)).Last<string>();
            }
            catch
            {
            }
            return x1;
        }

        /// <summary>
        /// Returns all Sub Directories
        /// </summary>
        /// <param name="x">Sub Directory To Search For</param>
        /// <returns></returns>
        public static List<string> GetAllSubDirectories(this string x)
        {
            var _tmpReturn = new List<string>();
            if (x.DirectoryExists()) { return _tmpReturn; }

            var _Returns = System.IO.Directory.GetDirectories(x, "*.dll", SearchOption.AllDirectories);

            return _tmpReturn;
        }

    }
}
