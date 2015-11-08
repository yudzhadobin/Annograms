using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Annograms
{
    class Program
    {
        static void Main(string[] args)
        {

            HashSet<String> allUniqueWords = new HashSet<String>();
            String filePath;
            do
            {
                Console.WriteLine("Input file name:");

                filePath = Console.ReadLine();
                if (File.Exists(filePath))
                {
                    Console.WriteLine("File name incorrect, no file with this name");
                }
            } while (!File.Exists(filePath));

            String[] words;
            int numb = 0;
            foreach (String cur in File.ReadLines(filePath))
            {
                words = cur.Split(' ', ',', '.', '"', '!', '?', '-', ';', ':', ')', '(');
                foreach (string word in words)
                {
                    numb++;
                    if (word.Length > 1 && !allUniqueWords.Contains(word))
                    {
                        allUniqueWords.Add(word.ToLower());
                    }
                }
            }
            List<SortedWord> allWords = new List<SortedWord>();

            foreach (var word in allUniqueWords)
            {

                allWords.Add(new SortedWord(word));
            }
            allWords.Sort();

            List<String> anagrams = getAllAnagrams(allWords);
            foreach (String str in anagrams)
            {
                Console.WriteLine(str);
            }
            Console.ReadKey();


        }

        static List<String> getAllAnagrams(List<SortedWord> words)
        {
            List<String> result = new List<String>();

            StringBuilder builder = new StringBuilder();

            int count;
            for (int i = 0; i < words.Count - 1; i++)
            {
                count = 0;
                builder.Append(words[i].originalWord);
                for (int j = i + 1; j < words.Count - 1; j++)
                {
                    if (words[i].Equals(words[j + 1]))
                    {
                        builder.Append(" " + words[j + 1].originalWord);
                        count++;
                    }
                }
                if (count != 0)
                {
                    result.Add(builder.ToString());
                }
                builder.Clear();
            }
            return result;
        }
    }

    class SortedWord : IComparable<SortedWord>
    {
        public string originalWord;
        public string sortedWord;

        public int CompareTo(SortedWord another)
        {
            return originalWord.CompareTo(another.originalWord);
        }


        public override int GetHashCode()
        {
            return originalWord.GetHashCode() / 2 + sortedWord.GetHashCode() / 2;
        }

        public override bool Equals(Object another)
        {
            if (another is SortedWord)
            {
                return this.sortedWord.Equals(((SortedWord)another).sortedWord);
            }
            return false;
        }

        public SortedWord(String word)
        {
            this.originalWord = word;
            List<Char> sub = word.ToLower().ToList();
            sub.Sort();
            StringBuilder builder = new StringBuilder();
            foreach (char ch in sub)
            {
                builder.Append(ch);
            }
            this.sortedWord = builder.ToString();
        }


    }
}
