using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        public static string answer;
        public static HashSet<char> guessed = new HashSet<char>();
        
        // This method removes all elements from the hashset dictionary, that do not match the guessed pattern so far.
        private static bool Remover(string s)
        {
            HashSet<string> set = new HashSet<string>();
            if (s[0] != answer[0] || s[s.Length - 1] != answer[answer.Length - 1]) set.Add(s);
            for (int i = 0; i < answer.Length; i++)
            { 
                try
                {
                    if (guessed.Contains(answer[i]) && s[i] != answer[i]) set.Add(s);
                }
                catch (Exception e) {};
            }
                return (set.Contains(s));
        }

        // This method prints the guessed letters from the guessed hash and then reveals the guessed positionfs of the sought
        // word, whilst still keeping the non-guessed ones hidden.
        public static void PrintGuessed(HashSet<char> input)
        {
            HashSet<char> h = new HashSet<char>(input);
            Console.Write("Guessed so far:");
            while (h.Count != 0)
            {
                Console.Write(" " + h.First());
                h.Remove(h.First());
            }
            Console.Write("\n Hangman: ");
            for (int i = 0; i < answer.Length; i++)
            {
                if (input.Contains(answer[i])) Console.Write(answer[i]); else Console.Write('_');
            }
            Console.Write("\n\n");
        }

        static void Main(string[] args)
        {
            /* The words of the dictionary this evil hangman example works with are supplied in the linear array below.
             * For a more sophisticated implementation loading from an external file is obviously possible, but the idea here
             * was to provide a simple solution to the problem, so beginner programmers could understand how they could solve
             * it themselves.
             */
            string[] dict = {"bakalava", "balamata", "balerina", "balirina", "baniceta", "kalotina", "kolibata", "korubata"};
            HashSet<string> words = new HashSet<string>(dict);
            char[] seq = {'l', 'r', 'i', 'o', 'e', 'n', 'm', 'k', 'v', 't', 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'p', 'r', 's', 'u', 'w', 'x', 'y', 'z'};
            HashSet<char> toGuess = new HashSet<char>(seq);
            Random rand = new Random();
            Console.WriteLine("Pick a word: (1-" + words.Count + ")");
            int ind = int.Parse(Console.ReadLine());
            
            Console.WriteLine("The word you chose is " + answer + ". Let's see whether the computer can guess it...");
            answer = dict[ind - 1];
            
            guessed.Add(answer[0]);
            guessed.Add(answer[answer.Length - 1]);
            
            while (words.Count != 1)
            {
                words.RemoveWhere(Remover);
                PrintGuessed(guessed);
                Console.WriteLine(string.Join(", ", words));
                guessed.Add(toGuess.First());
                toGuess.Remove(toGuess.First());
            }
            Console.WriteLine("The word is: " + words.First());
            Console.ReadLine();
        }
    }
}
