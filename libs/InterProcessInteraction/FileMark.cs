using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Karen.Types;
namespace Karen.InterProcess
{
    internal static class FileMark
    {
        internal static void MarkReaded(this FileInfo fileInfo)
        {
            if (fileInfo.Name.StartsWith('0'))
            {
                fileInfo.Rename(new string(fileInfo.Name.Skip(1).ToArray()));
            }
        }
        internal static void MarkUnreaded(this FileInfo fileInfo)
        {
            if (!fileInfo.Name.StartsWith('0'))
            {
                fileInfo.Rename('0'+fileInfo.Name);
            }
        }
    }
}
