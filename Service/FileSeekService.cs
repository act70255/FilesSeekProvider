using Utility.Extension;
using Service.Interface;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.IO;

namespace Service
{
    public class FileSeekService : IFileSeekService
    {
        ConcurrentBag<SeekResultModel> SeekResultList = new ConcurrentBag<SeekResultModel>();

        #region Iterator version
        public IEnumerable<SeekResultModel> ProcessSeek(string path, string pathKeyword, string rawkeyWord, string searchPattern = "*.*", bool isIgnoreCase = false, bool isRegex = false)
        {
            const char separator = '&';
            string[] files = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            string[] fileKeywords = pathKeyword.Split(separator);
            string[] keyWords = rawkeyWord.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Expression<Func<string, bool>> filter = f => false;

            foreach (var keyword in keyWords)
            {
                Expression<Func<string, bool>> seekfilter = f => true;
                if (isRegex)
                {
                    seekfilter = seekfilter.AndAlso(f => Regex.IsMatch(f, keyword, isIgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None));
                }
                else
                {
                    seekfilter = seekfilter.AndAlso(f => f.Contains(keyword, isIgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.CurrentCulture));
                }
                filter = filter.OrElse(seekfilter);
            }

            return ProcessSeek(files.Where(f => fileKeywords.Any(a => f.Contains(a))), filter);
        }
        IEnumerable<SeekResultModel> ProcessSeek(IEnumerable<string> files, Expression<Func<string, bool>> filter)
        {
            foreach (var file in files)
            {
                int index = 0;
                string currentFile = "";
                if (currentFile != file)
                {
                    currentFile = file;
                    yield return new SeekResultModel
                    {
                        FileName = System.IO.Path.GetFileName(file),
                        Path = file,
                        Line = -999,
                        Content = ""
                    };
                }
                foreach (var line in File.ReadLines(file))
                {
                    if (filter.Compile()(line))
                    {
                        yield return new SeekResultModel
                        {
                            FileName = System.IO.Path.GetFileName(file),
                            Path = file,
                            Content = line,
                            Line = index + 1
                        };
                    }
                    index++;
                }
            }
        }
        #endregion
        #region Async version
        public List<SeekResultModel> ProcessSeekTask(string path, string pathKeyword, string rawkeyWord, string searchPattern = "*.*", bool isIgnoreCase = false, bool isRegex = false)
        {
            ConcurrentBag<SeekResultModel> results = new ConcurrentBag<SeekResultModel>();
            List<Task> tasks = new List<Task>();

            #region Input validation
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("foler path required");
            }
            if (string.IsNullOrEmpty(rawkeyWord))
            {
                throw new ArgumentException("Must be setup keyword");
            }
            #endregion
            #region Filters
            const char separator = '&';
            string[] files = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            string[] fileKeywords = pathKeyword.Split(separator);
            string[] keyWords = rawkeyWord.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Expression<Func<string, bool>> filter = f => false;

            foreach (var keyword in keyWords)
            {
                Expression<Func<string, bool>> seekfilter = f => true;
                if (isRegex)
                {
                    seekfilter = seekfilter.AndAlso(f => Regex.IsMatch(f, keyword, isIgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None));
                }
                else
                {
                    seekfilter = seekfilter.AndAlso(f => f.Contains(keyword, isIgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.CurrentCulture));
                }
                filter = filter.OrElse(seekfilter);
            }
            #endregion

            //if (fileExtension.Any())
            //    files = files.Where(f => fileExtension.Contains(Path.GetExtension(f))).ToArray();

            tasks = files.Select(s => Task.Run(() =>
            {
                var result = ProcessSeek(s, filter);
                if (result != null)
                {
                    foreach (var each in result)
                        results.Add(each);
                }
            })).ToList();
            Task.WhenAll(tasks).GetAwaiter().GetResult();
            return results.OrderBy(o => o.FileName).ThenBy(o => o.Line).ToList();
        }

        List<SeekResultModel> ProcessSeek(string file, Expression<Func<string, bool>> filter)
        {
            return File.ReadLines(file)
                .AsQueryable()
                .Where(filter.Compile())
                    .Select((s, i) => new SeekResultModel
                    {
                        Line = i + 1,
                        FileName = System.IO.Path.GetFileName(file),
                        Path = file,
                        Content = s })
                .ToList();
        }
        #endregion
        #region Extract
        public string ExtractByKeyword(string path, string[] keywords, string fileKey = "")
        {
            List<string> matchContent = new List<string>();
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            if (!string.IsNullOrEmpty(fileKey))
            {
                files = files.Where(o => o.Contains(fileKey)).ToArray();
            }
            foreach (string file in files)
            {
                foreach (string line in File.ReadLines(file))
                {
                    if (keywords.Length > 1)
                    {
                        bool isMatch = true;
                        foreach (string key in keywords)
                        {
                            if (!line.Contains(key))
                            {
                                isMatch = false;
                                break;
                            }
                        }
                        if (isMatch)
                        {
                            matchContent.Add(line);
                        }
                    }
                }
            }

            if (matchContent.Count > 0)
            {
                string targetFileName = $"[{string.Join('#', keywords)}]({DateTime.Now.ToString("yyyyMMddHHmmss")}";
                string fileName = ParseValidFileName(targetFileName).Aggregate("", (current, c) => current + c);
                string targetFilePath = $"{Path.GetDirectoryName(path)}\\{fileName}.txt";
                File.WriteAllLines(targetFilePath, matchContent.OrderBy(o => o));
                return targetFilePath;
            }
            return "";

            IEnumerable<char> ParseValidFileName(string fileName)
            {
                foreach (char c in fileName)
                {
                    if (!Path.GetInvalidFileNameChars().Contains(c))
                    {
                        yield return c;
                    }
                }
            }
        }
        #endregion
    }
}
