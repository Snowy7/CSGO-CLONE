﻿using System.IO;
using Snowy.IO;

namespace Snowy.CSharp.IO
{
    public static class IOExtensions
    {
        public static string GetParentPath(this DirectoryInfo self, int steps = 1)
        {
            return PathUtility.GetParentPath(self.FullName, steps);
        }

        public static string GetParentPath(this FileInfo self, int steps = 1)
        {
            return PathUtility.GetParentPath(self.FullName, steps);
        }
    }
}
