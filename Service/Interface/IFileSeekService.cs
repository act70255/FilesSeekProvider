﻿using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFileSeekService
    {
        List<SeekFileResultModel> SeekInFolder(string path, string[] fileExtension, string[] keywords, bool isIgnoreCase = false, bool isRegex = false);
        IEnumerable<KeyValuePair<string, IEnumerable<SeekFileResultModel>>> SeekFromPath(string path, string[] fileExtension, string[] keywords, bool isIgnoreCase = false, bool isRegex = false);
    }
}
