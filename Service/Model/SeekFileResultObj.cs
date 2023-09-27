using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Service.Model
{
    public class SeekFileResultModel
    {
        public SeekFileResultModel()
        { }

        public SeekFileResultModel(string path, SeekResultDetailModel deail)
        {
            FileName = System.IO.Path.GetFileName(path);
            Path = path;
            Line = deail.Index;
            Data = deail;
        }
        public string FileName { get; set; }
        public string Path { get; set; }
        public int Line { get; set; }
        public SeekResultDetailModel Data { get; set; }
    }
    public class SeekResultDetailModel : IndexContentModel
    {
        public IndexContentModel? SeekNextPosition(string[] filter, int startIndex, bool ignoreCase = false)
        {
            var results = filter.Select(s => SeekNextPosition(s, startIndex, ignoreCase)).Where(w => w.Index > 0).ToList();
            return results.Any(a => a.Index == results.Min(m => m.Index)) ? results.FirstOrDefault(f => f.Index == results.Min(m => m.Index)) : new IndexContentModel();
        }

        IndexContentModel SeekNextPosition(string filter, int startIndex, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(filter))
                return new IndexContentModel();

            if (Content == filter)
                return new IndexContentModel(0, Content);

            int seekindex = startIndex + 1;
            while (seekindex + filter.Length <= Content.Length)
            {
                var searchIndex = Content.IndexOf(filter, seekindex, ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture);
                if (searchIndex > 1)
                {
                    return new IndexContentModel(searchIndex, filter);
                }
                else
                {
                    break;
                }
            }
            return new IndexContentModel();
        }
    }
    public class IndexContentModel
    {
        public IndexContentModel(int index=-1,string content="")
        {
            Index = index;
            Content = content;
        }

        public int Index { get; set; } = -1;
        public string Content { get; set; }
    }
}
