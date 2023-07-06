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
            Stopwatch sw = Stopwatch.StartNew();
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

            sw.Restart();
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
            Debug.WriteLine(sw.ElapsedMilliseconds);

            //sw.Restart();
            //files.ToList().ForEach(f =>
            //{
            //    var result = SeekInFile(f, keyword, keyWords, isIgnoreCase, isRegex);
            //    if (result != null)
            //        results.AddRange(result);
            //});
            //Debug.WriteLine(sw.ElapsedMilliseconds);

            return results.ToList();
        }

        List<SeekFileResultModel> SeekInFile(string path, string keyword, string[] keyWords, bool isIgnoreCase = false, bool isRegex = false)
        {
            List<SeekFileResultModel> result;
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

            if (keyWords.Any())
                result = contentList
                .Where(filter.Compile()).Where(f => keyWords.Any(a => f.Content.Contains(a)))
                .Select(s => new SeekFileResultModel(path, s)).ToList();
            else
                result = contentList
                    .Where(filter.Compile())
                    .Select(s => new SeekFileResultModel(path, s)).ToList();

            if (result.Any())
            {
                return result;
            }
            return null;
        }
    }
}
