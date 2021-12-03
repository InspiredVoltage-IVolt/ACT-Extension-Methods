using System.Security.AccessControl;

namespace ACT.Core.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static void AddSecurity(
          this DirectoryInfo dInfo,
          string Account,
          FileSystemRights Rights,
          AccessControlType ControlType)
        {
            DirectorySecurity accessControl = dInfo.GetAccessControl();
            accessControl.AddAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));
            dInfo.SetAccessControl(accessControl);
        }

        public static void RemoveSecurity(
          this DirectoryInfo dInfo,
          string Account,
          FileSystemRights Rights,
          AccessControlType ControlType)
        {
            DirectorySecurity accessControl = dInfo.GetAccessControl();
            accessControl.RemoveAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));
            dInfo.SetAccessControl(accessControl);
        }

        public static void CopyDirectory(this DirectoryInfo source, DirectoryInfo target) => DirectoryInfoExtensions.CopyFilesRecursively(source, target);

        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo directory in source.GetDirectories())
            {
                DirectoryInfoExtensions.CopyFilesRecursively(directory, target.CreateSubdirectory(directory.Name));
            }

            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name));
            }
        }

        public static void DeleteContents(this DirectoryInfo source)
        {
            foreach (FileSystemInfo file in source.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo directory in source.GetDirectories())
            {
                directory.Delete(true);
            }
        }
    }
}
