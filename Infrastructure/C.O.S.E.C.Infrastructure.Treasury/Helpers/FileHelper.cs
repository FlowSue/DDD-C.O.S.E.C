namespace C.O.S.E.C.Infrastructure.Treasury.Helpers
{
    using System;

    using Microsoft.Extensions.Logging;
    using System.IO;

    /// <summary>
    /// 文件操作
    /// </summary>
    public class FileHelper
    {
        private static ILogger<FileHelper> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public FileHelper(ILogger<FileHelper> logger)
        {
            FileHelper.logger = logger;
        }

        /// <summary>
        /// 保存为不带Bom的文件
        /// </summary>
        /// <param name="txtStr"></param>
        /// <param name="tempDir">格式:a/b.htm,相对根目录</param>
        public static void SaveFile(string txtStr, string tempDir)
        {
            SaveFile(txtStr, tempDir, true);
        }

        /// <summary>
        /// 保存文件内容,自动创建目录
        /// </summary>
        /// <param name="txtStr"></param>
        /// <param name="tempDir">格式:a/b.htm,相对根目录</param>
        /// <param name="noBom"></param>
        public static void SaveFile(string txtStr, string tempDir, bool noBom)
        {
            try
            {
                // CreateDir(GetFolderPath(true, tempDir));
                var sw = noBom ? new StreamWriter(tempDir, false, new System.Text.UTF8Encoding(false)) : new StreamWriter(tempDir, false, System.Text.Encoding.UTF8);
                sw.Write(txtStr);
                sw.Close();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, string.Empty);
            }
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="file">格式:a/b.htm,相对根目录</param>
        /// <returns></returns>
        public static bool FileExists(string file) => File.Exists(file);

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dir">物理路径</param>
        public static void CreateDir(string dir)
        {
            if (dir.Length != 0 && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        /// <summary>
        /// 创建目录路径
        /// </summary>
        /// <param name="folderPath">物理路径</param>
        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        /// <summary>
        /// 复制文件        
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <param name="overwrite">如果已经存在是否覆盖？</param>
        public static void CopyFile(string file1, string file2, bool overwrite)
        {
            if (!File.Exists(file1))
            {
                return;
            }

            if (overwrite)
            {
                File.Copy(file1, file2, true);
            }
            else
            {
                if (!File.Exists(file2))
                {
                    File.Copy(file1, file2);
                }
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件物理路径</param>
        public static void DelFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
