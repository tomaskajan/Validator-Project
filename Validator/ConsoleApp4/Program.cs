using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Validator
{
    class Program
    {
        static void Main(string[] args)
        {
            var validator = new NameValidator();
            validator.Run();
        }
    }

    public class NameValidator
    {
        static string GetName()
        {
            Console.Write("Enter your name: ");
            return Console.ReadLine();
        }
        static bool IsValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name) ||name.Length < 3)
            {
                Console.WriteLine("Your name must be longer than 2 letters.");
                return false;
            }
            else if (name.Any(c => "§.,+%^&*()-=_|'][/;".Contains(c)))
            {
                Console.WriteLine("Your name cannot contain special symbols.");
                return false;
            }

            return true;
        }

        static string SuggestName(string name)
        {
            Dictionary<char, char> replacement = new Dictionary<char, char>()
            {
                { '0', 'o' },
                { '1', 'i' },
                { '3', 'e' },
                { '5', 's' },
                { '4', 'a' },
                { '7', 't' },
            };

            string result = "";

            foreach (char c in name)
            {
                if (replacement.ContainsKey(c))
                    result += replacement[c];
                else if (Char.IsLetter(c))
                {
                    result += c;
                }
            }
            
            return Capitalize(result);
        }

        static string Capitalize(string s)
        {
            if (s.Length == 0) return s;
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        public void Run()
        {
            while (true)
            {
                string name = GetName();
                if (!IsValid(name)) continue;

                string suggestion = SuggestName(name);
                Console.WriteLine($"Did you mean: {suggestion}");
                break;
            }
        }
    }
}
