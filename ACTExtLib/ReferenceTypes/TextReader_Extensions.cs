using System.Text;

namespace ACT.Core.Extensions
{
    public static class SystemIOTextReaderExtensions
    {
        public static TextReader OpenFile(this string TextFilePath, Encoding FileEncoding) => new StreamReader(TextFilePath, FileEncoding);
    }
}
