// Program.cs
using EngineeringJarvis.Models;
using EngineeringJarvis.Services;
using System;

namespace EngineeringJarvis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Engineering JARVIS!");
            Console.WriteLine("Ask me engineering questions. Type 'exit' to quit.\n");

            var knowledgeEntries = KnowledgeBaseLoader.LoadKnowledge("KnowledgeBase");
            var matcher = new PatternMatcher(knowledgeEntries);

            while (true)
            {
                Console.Write("You: ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.ToLower() == "exit")
                    break;

                string response = matcher.GetResponse(input);
                Console.WriteLine($"JARVIS: {response}\n");
            }
        }
    }
}