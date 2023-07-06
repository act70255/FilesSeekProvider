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

namespace Service
{
    internal class FileSeekService : IFileSeekService
    {
        public List<SeekFileResultModel> SeekInFolder(string path, string[] fileExtension, string keyword, string[] keyWords, bool isIgnoreCase = false, bool isRegex = false)
        {
            #region Input validation
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("foler path required");
            }
            if (string.IsNullOrEmpty(keyword))
            {
                throw new ArgumentException("Must be setup keyword");
            }
            #endregion

            ConcurrentBag<SeekFileResultModel> results = new ConcurrentBag<SeekFileResultModel>();
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            List<Task> tasks = new List<Task>();

            if (fileExtension.Any())
                files = files.Where(f => fileExtension.Contains(Path.GetExtension(f))).ToArray();

            tasks = files.Select(s => Task.Run(() =>
            {
                var result = SeekInFile(s, keyword, keyWords, isIgnoreCase, isRegex);
                if (result != null)
                {
                    foreach (var each in result)
                        results.Add(each);
                }
            })).ToList();
            Task.WhenAll(tasks).GetAwaiter().GetResult();

            return results.ToList();
        }
        
        List<SeekFileResultModel> SeekInFile(string path, string keyword, string[] keyWords, bool isIgnoreCase = false, bool isRegex = false)
        {
            var fileContent = File.ReadAllLines(path);
            var contentList = fileContent.Select((s, i) => new SeekResultDetailModel { Line = i + 1, Content = s }).ToList();
            Expression<Func<SeekResultDetailModel, bool>> filter = f => true;

            if (isRegex)
            {
                filter = filter.AndAlso(f => Regex.IsMatch(f.Content, keyword, isIgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None));
            }
            else
            {
                filter = filter.AndAlso(f => f.Content.Contains(keyword, isIgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.CurrentCulture));
            }

            var result = contentList
                    .AsQueryable()
                    .Where(filter.Compile());
            if (keyWords.Any())
                result = result.Where(f => keyWords.Any(a => f.Content.Contains(a)));

            if (result.Any())
            {
                return result.Select(s => new SeekFileResultModel(path, s)).ToList();
            }
            return null;
        }
    }
}
