using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities
{
	public class Helper
	{
		public static string GetCopyPath(string root,params string[] folders)
		{
            var path = root;
           
            foreach (var folder in folders)
            {
                path = Path.Combine(path, folder);
            }
           
            return path;
        }
	}
}
