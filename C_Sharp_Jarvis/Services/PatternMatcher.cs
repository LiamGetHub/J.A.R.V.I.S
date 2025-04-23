// Services/PatternMatcher.cs
using EngineeringJarvis.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EngineeringJarvis.Services
{
    public class PatternMatcher
    {
        private readonly List<KnowledgeEntry> _entries;

        public PatternMatcher(List<KnowledgeEntry> entries)
        {
            _entries = entries;
        }

        public string GetResponse(string input)
        {
            foreach (var entry in _entries)
            {
                if (Regex.IsMatch(input.ToLower(), entry.Pattern))
                    return entry.Response;
            }
            return "Sorry, I don't have an answer for that yet.";
        }
    }
}