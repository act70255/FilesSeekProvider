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
            Line = deail.Line;
            Data = deail;
        }
        public string FileName { get; set; }
        public string Path { get; set; }
        public int Line { get; set; }
        public SeekResultDetailModel Data { get; set; }
    }
    public class SeekResultDetailModel
    {
        public int Line { get; set; }
        public string Content { get; set; }
        public string FilterKey { get; set; }
        public List<int> MatchPositions { get; set; }

        public List<int> SetFilter(string filter, bool ignoreCase = false)
        {
            if (FilterKey == filter && MatchPositions != null && MatchPositions.Any())
                return MatchPositions;

            MatchPositions = new List<int>();
            FilterKey = filter;
            int seekindex = 0;

            while (seekindex + filter.Length <= Content.Length)
            {
                var searchIndex = Content.IndexOf(filter, seekindex, ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture);
                if (searchIndex > 1)
                {
                    MatchPositions.Add(searchIndex);
                    seekindex = searchIndex + 1;
                }
                else
                {
                    break;
                }
            }
            return MatchPositions;
        }
    }
}
