using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_4_4
{
    /*
     * --ignore-fail-on-non-empty : Игнорирование всех ошибок
     * -p : Удаление всех вложенных папок
     * -v : говорит, что было удалено
     */

    class Program
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="root"></param>
        static void RecursiveDelete(string root)
        {
			// Выход из рекурсии
	        if (Directory.GetDirectories(root).Length == 0)
	        {
				if ( Directory.GetFiles(root).Length > 0)
					DeleteFiles(root);

		        Console.WriteLine("DIR:\t" + root + "\t{ DELETED }");
		        return;
	        }

			// Рекурсия восходящая
	        foreach (var dir in Directory.GetDirectories(root))
	        {
		        RecursiveDelete(dir);
	        }

			DeleteFiles(root);
			Console.WriteLine("DIR:\t" + root + " \t{DELETED }");
		}

		/// <summary>
		/// Удаление всех файлов в директории
		/// </summary>
		/// <param name="root"></param>
	    static void DeleteFiles(string root)
	    {
		    foreach (var file in Directory.GetFiles(root))
		    {
			    Console.WriteLine("FILE:\t" + file + "\t{ DELETED }");
		    }
	    }

        static void Main(string[] args)
        {
            while (true)
            {
                string[] inpCommand = Console.ReadLine().Trim().ToLower().Split(' ');
                
                //Если у нас просто удаление директории
                if (Directory.Exists(inpCommand[0]) && inpCommand.Length == 1)
                {
                    if (Directory.GetDirectories(inpCommand[0]).Length > 0)
                    {
                        Console.WriteLine("\nУдаление директории невозможно. Присутствуют поддиректории.\n");
                        continue;
                    }
                    if (Directory.GetFiles(inpCommand[0]).Length > 0)
                    {
                        Console.WriteLine("\nУдаление директории невозможно. Директория непуста.\n");
                        continue;
                    }

                    Console.WriteLine("\nДиректория удалена.\n");
                    continue;
                }
                // Модификаторы
                if (inpCommand.Length > 1)
                {
                    // Папка существует
                    if (Directory.Exists(inpCommand[1]))
                    {
                        if (inpCommand.Contains("--ignore-fail-on-non-empty"))
                        {
                            Console.WriteLine("\nДиректория удалена\n");
                            continue;
                        }

                        if (inpCommand.Contains("-p"))
                        {
                            // Присутствие файлов в корневом каталоге
                            if (Directory.GetFiles(inpCommand[1]).Length > 0)
                            {
                                Console.WriteLine("\nУдаление директории невозможно. Директория непуста.\n");
                                continue;
                            }

                            // Ищем файлы во всех подпапках
                            string[] directories = Directory.GetDirectories(inpCommand[1]);
                            bool filesExist = false;
                            foreach (var dir in directories)
                            {
                                if (Directory.GetFiles(dir).Length > 0)
                                {
                                    Console.WriteLine("\nУдаление директории невозможно. Директория непуста.\n");
                                    filesExist = true;
                                    break;
                                }
                            }

                            // Найдены файлы. Удаление невозможно
                            if (filesExist)
                            {
                                continue;
                            }

                            Console.WriteLine("Директория удалена");
                            continue;
                        }

                        if (inpCommand.Contains("-v"))
                        {
							Console.WriteLine("\n\nDeleting all directories & files...\n");

							RecursiveDelete(inpCommand[1]);

							Console.WriteLine("\nDeleting done.\n");
                            //string[] directories = Directory.GetDirectories(inpCommand[1]);

                            //StringBuilder sb = new StringBuilder("\n\nDeleting all directories & files...\n");

                            //foreach (var dir in directories)
                            //{
                            //    sb.Append("\n" + dir + " DELETING...");

                            //    string[] files = Directory.GetFiles(dir);
                            //    foreach (var file in files)
                            //    {
                            //        sb.Append("\n" + file + " << DELETED >>");
                            //    }
                            //}

                            //sb.Append("\n" + inpCommand[1] + " DELETING...");
                            //string[] rootFiles = Directory.GetFiles(inpCommand[1]);
                            //foreach (var file in rootFiles)
                            //{
                            //    sb.Append("\n" + file + " << DELETED >>");
                            //}

                            //sb.Append("\n\n<< DELETING DONE >>");

                            //Console.WriteLine(sb.ToString());
                            continue;
                        }
                    }
                    
                }
            }
        }
    }
}
