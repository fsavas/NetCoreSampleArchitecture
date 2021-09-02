using System.IO;
using static Sample.Core.Defaults.EnumClasses;

namespace Sample.Core.Helpers
{
    public static class FileHelper
    {
        #region Methods

        public static string GetFilePath(string fileName, FileExtensionTypes fileExtensionTypes)
        {
            var path = @"E:\Files\";//todo get from db
            MakeContentDirectory(path);

            return path += fileName + "." + fileExtensionTypes; ;
        }

        public static void MakeContentDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion Methods
    }
}