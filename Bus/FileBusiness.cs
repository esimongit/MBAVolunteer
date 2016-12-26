using System;
using System.Collections.Generic; 
using System.Text;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace NQN.Bus
{
    public class FileBusiness
    {
        string FileDir = "e:\\MBA\\Docs";
        string tmpDir = "C:\\Windows\\temp";
        
        public void Import(string fname)
        {
            string sourcePath = Path.Combine(tmpDir, fname);
            File.Copy(sourcePath, Path.Combine(FileDir, fname));
        }
        public List<FilesObject> ListFiles()
        {
            List<FilesObject> fList = new List<FilesObject>();
            foreach (string fname in Directory.GetFiles(FileDir))
            {
                FilesObject fobj = new FilesObject();
                FileInfo fi = new FileInfo(Path.Combine(FileDir, fname));
                
                fobj.FileName = fi.Name;
                fobj.FileSize = fi.Length;
                fobj.Extension = fi.Extension;
                fList.Add(fobj);
            }
            return fList;
        }
        public void RemoveFile(string FileName)
        {
            string fPath = Path.Combine(FileDir, FileName);
            File.Delete(fPath);
        }
        public string CompletedText(string txt)
        {
            string host = HttpContext.Current.Request.Url.Host;
          
            foreach (string fpath in Directory.GetFiles(FileDir))
            {
                FileInfo fi = new FileInfo(fpath);
                txt = txt.Replace("[" + fi.Name + "]", String.Format(@"<a href='/Docs/{0}' target='_blank'>{1}</a>",  
                    HttpContext.Current.Server.HtmlEncode(fi.Name), fi.Name));
            }
            return txt;
        }
    }
    public class FilesObject
    {
        public long FileSize
        {
            get;set;
        }
        public string FileName
        {
            get; set;
        }
        public string Extension
        {
            get; set;
        }
    }
}

