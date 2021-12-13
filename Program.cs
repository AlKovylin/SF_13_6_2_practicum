using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SF_13_6_2_practicum
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "Text1.txt";

            string text = File.ReadAllText(path);//получаем текст

            string noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());//убираем знаки препинания

            string[] separator = { " ", "\n", "\r" };

            string[] noSpacingText = noPunctuationText.Split(separator, StringSplitOptions.RemoveEmptyEntries);//убираем переносы строк, пробелы

            for (int i = 0; i < noSpacingText.Length; i++)
            {
                noSpacingText[i] = noSpacingText[i].ToLower();//приводим к нижнему регистру
            }

            Dictionary<string, int> sumOriginalWords = new Dictionary<string, int>();

            //выделяем оригинальные слова и получим их кол-во
            foreach (var word in noSpacingText)
            {
                if (!sumOriginalWords.ContainsKey(word))
                    sumOriginalWords.Add(word, 1);
                else
                    sumOriginalWords[word]++;
            }
            
            //сортируем по значению
            sumOriginalWords = sumOriginalWords.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Десять самых часто\n  встречающихся слов: \n");
            Console.WriteLine("-----------------------");
            Console.WriteLine("| №п/п |Слово| Кол-во |");
            Console.WriteLine("-----------------------");

            int ch = 1;
            
            foreach (var word in sumOriginalWords.Reverse())//переворачиваем сортировку для вывода на экран нужных значений
            {
                if (ch < 10)
                    Console.WriteLine("|  {0, -1}.  | {1, -3} |  {2, -5} |", ch, word.Key, word.Value);
                else
                    Console.WriteLine("|  {0, -2}. | {1, -3} |  {2, -5} |", ch, word.Key, word.Value);

                ch++;

                if (ch > 10)
                    break;
            }

            Console.WriteLine("-----------------------");
            Console.ResetColor();
        }
    }
}