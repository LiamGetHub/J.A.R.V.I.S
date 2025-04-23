// Services/KnowledgeBaseLoader.cs
using EngineeringJarvis.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EngineeringJarvis.Services
{
    public static class KnowledgeBaseLoader
    {
        public static List<KnowledgeEntry> LoadKnowledge(string directoryPath)
        {
            var entries = new List<KnowledgeEntry>();
            foreach (var file in Directory.GetFiles(directoryPath, "*.json"))
            {
                string json = File.ReadAllText(file);
                var fileEntries = JsonSerializer.Deserialize<List<KnowledgeEntry>>(json);
                if (fileEntries != null)
                {
                    entries.AddRange(fileEntries);
                }
            }
            return entries;
        }
    }
}