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
    public class FileSeekService : IFileSeekService
    {
        [BenchmarkDotNet.Attributes.Benchmark]
        public List<SeekFileResultModel> SeekInFolder(string path, string[] fileExtension, string[] keywords, bool isIgnoreCase = false, bool isRegex = false)
        {
            #region Input validation
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("foler path required");
            }
            if (!keywords.Any())
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
                var result = SeekInFile(s, keywords, isIgnoreCase, isRegex);
                if (result != null)
                {
                    foreach (var each in result)
                        results.Add(each);
                }
            })).ToList();
            Task.WhenAll(tasks).GetAwaiter().GetResult();

            return results.ToList();
        }

        List<SeekFileResultModel> SeekInFile(string path, string[] keywords, bool isIgnoreCase = false, bool isRegex = false)
        {
            var fileContent = File.ReadAllLines(path);
            var contentList = fileContent.Select((s, i) => new SeekResultDetailModel { Index = i + 1, Content = s }).ToList();
            Expression<Func<SeekResultDetailModel, bool>> filter = f => false;

            foreach (var keyword in keywords)
            {
                Expression<Func<SeekResultDetailModel, bool>> seekfilter = f => true;
                if (isRegex)
                {
                    seekfilter = seekfilter.AndAlso(f => Regex.IsMatch(f.Content, keyword, isIgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None));
                }
                else
                {
                    seekfilter = seekfilter.AndAlso(f => f.Content.Contains(keyword, isIgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.CurrentCulture));
                }
                filter = filter.OrElse(seekfilter);
            }

            var result = contentList
                    .AsQueryable()
                    .Where(filter.Compile());

            if (result.Any())
            {
                return result.Select(s => new SeekFileResultModel(path, s)).ToList();
            }
            return null;
        }
        [BenchmarkDotNet.Attributes.Benchmark]
        public IEnumerable<KeyValuePair<string, IEnumerable<SeekFileResultModel>>> SeekFromPath(string path, string[] fileExtension, string[] keywords, bool isIgnoreCase = false, bool isRegex = false)
        {
            Expression<Func<SeekResultDetailModel, bool>> filter = f => false;
            foreach (var keyword in keywords)
            {
                Expression<Func<SeekResultDetailModel, bool>> seekfilter = f => true;
                if (isRegex)
                {
                    seekfilter = seekfilter.AndAlso(f => Regex.IsMatch(f.Content, keyword, isIgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None));
                }
                else
                {
                    seekfilter = seekfilter.AndAlso(f => f.Content.Contains(keyword, isIgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.CurrentCulture));
                }
                filter = filter.OrElse(seekfilter);
            }
            return GetFiles(path, new string[] { }, filter);
        }
        IEnumerable<KeyValuePair<string, IEnumerable<SeekFileResultModel>>> GetFiles(string path, string[] fileExtension, Expression<Func<SeekResultDetailModel, bool>> seekfilter)
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            if (fileExtension.Any())
                files = files.Where(f => fileExtension.Contains(Path.GetExtension(f))).ToArray();
            foreach (var file in files)
            {
                var result = SeekFromFile(file, seekfilter);
                if (result.Any())
                    yield return new KeyValuePair<string, IEnumerable<SeekFileResultModel>>(file, result);
            }

            IEnumerable<SeekFileResultModel> SeekFromFile(string path, Expression<Func<SeekResultDetailModel, bool>> seekfilter)
            {
                var result = File.ReadAllLines(path).Select((s, i) => new SeekResultDetailModel { Index = i + 1, Content = s }).Where(seekfilter.Compile()).Select(s => new SeekFileResultModel(path, s));
                return result.ToList();
            }
        }
    }
}
