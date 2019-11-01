using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaSky.SyTemplater.WebApi.Comment
{
    class UploadHelper
    {
        public static string GetSaveFilePath(string fileName)
        {
            var fileSuffix = Path.GetExtension(fileName);
            string month = DateTime.Now.ToString("yyyyMM");
            string time = DateTime.Now.ToString("ddHHmmssms");
            string dir = string.Format(@"upload\{0}\{1}", fileSuffix, month);
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + dir))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + dir);
            }
            dir += "\\" + Path.GetFileNameWithoutExtension(fileName) + "_" + time + fileSuffix;
            return dir;
        }
    }
}
