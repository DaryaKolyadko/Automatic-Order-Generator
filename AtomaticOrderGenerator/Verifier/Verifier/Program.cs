using System;
using System.Text;
using System.IO;

namespace Verifier
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose files 1:");
                String[] files_1 = Console.ReadLine().Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("Choose files 2:");
                String[] files_2 = Console.ReadLine().Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                int length = Math.Min(files_1.Length, files_2.Length);

                for (int i = 0; i < length; i++)
                {
                    StreamReader reader_1 = new StreamReader(files_1[i], Encoding.Default);
                    StreamReader reader_2 = new StreamReader(files_2[i], Encoding.Default);

                    String[] content_1 = reader_1.ReadToEnd().Split(new String[] { " ", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    String[] content_2 = reader_2.ReadToEnd().Split(new String[] { " ", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    if (CompareArrays(content_1, content_2))
                        Console.WriteLine("OK");
                }
                Console.ReadKey();
            }
        }

        static bool CompareArrays(String[] arr_1, String[] arr_2)
        {
            if (arr_1.Length != arr_2.Length)
            {
                Console.WriteLine("ERROR different sizes");
               // return false;
            }

            int len = Math.Min(arr_1.Length, arr_2.Length);

            for (int i = 0; i < len; i++)
            {
              //  if (!arr_1[i].Contains(".00"))
              //  {
                arr_1[i] = arr_1[i].Replace(",", "");
                arr_2[i] = arr_2[i].Replace(",", "");

                    if (!arr_1[i].Equals(arr_2[i]))
                    {
                        Console.WriteLine("ERROR {0} {1} {2}", i, arr_1[i], arr_2[i]);
                    }
              //  }
            }
         
            return true;
        }

    }
}