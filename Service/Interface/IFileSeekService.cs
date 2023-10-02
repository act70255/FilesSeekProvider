using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFileSeekService
    {
        IEnumerable<SeekResultModel> ProcessSeek(string path, string pathKeyword, string keyWord, string searchPattern = "*.*", bool isIgnoreCase = false, bool isRegex = false);
        public List<SeekResultModel> ProcessSeekTask(string path, string pathKeyword, string rawkeyWord, string searchPattern = "*.*", bool isIgnoreCase = false, bool isRegex = false);
        string ExtractByKeyword(string path, string[] keywords, string fileKey = "");
    }
}
