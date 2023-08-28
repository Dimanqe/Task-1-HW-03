using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1__HW_03_
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\\Users\\dsank\\Desktop\\111";

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Указанная папка не существует.");
                return;
            }

            try
            {
                CleanFolder(folderPath, TimeSpan.FromMinutes(30));
                Console.WriteLine("Очистка завершена.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Ошибка доступа: у вас нет прав для удаления файлов из этой папки.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        static void CleanFolder(string folderPath, TimeSpan maxAge)
        {
            var directoryInfo = new DirectoryInfo(folderPath);

            foreach (var file in directoryInfo.GetFiles())
            {
                if (DateTime.Now - file.LastAccessTime > maxAge)
                {
                    file.Delete();
                    Console.WriteLine($"Удален файл: {file.FullName}");
                }
            }

            foreach (var subfolder in directoryInfo.GetDirectories())
            {
                CleanFolder(subfolder.FullName, maxAge);
                if (subfolder.GetFiles().Length == 0 && subfolder.GetDirectories().Length == 0)
                {
                    subfolder.Delete();
                    Console.WriteLine($"Удалена пустая папка: {subfolder.FullName}");
                }
            }
        }
    }
}
