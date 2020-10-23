using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeserializeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TriageRepository> repos = new();

            using (var stream = File.OpenRead("data.json"))
            {
                repos = JsonSerializer.DeserializeAsync<List<TriageRepository>>(stream, CreateOptions()).GetAwaiter().GetResult();
            }

            Console.Write(repos[0].Issues.Count);
        }

        private static JsonSerializerOptions CreateOptions()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                IncludeFields = true,
                MaxDepth = 30
            };

            return options;
        }
    }

    public class TriageRepository
    {
        public List<Issue> Issues { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
    }
}
