using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shop.WebUserInterface.Tests.Mocks
{
    public class MyFileBase: HttpPostedFileBase
    {
        Stream stream;
        string contentType;
        string fileName;

        public MyFileBase(Stream stream, string contentType, string flieName)
        {
            this.stream = stream;
            this.contentType = contentType;
            this.fileName = flieName;
        }

        public override string ContentType
        {
            get { return contentType; }
        }

        public override int ContentLength => (int)stream.Length;

        public override string FileName {
            get { return fileName; }
        }

        public override Stream InputStream => stream;

        public override void SaveAs(string filename)
        {
            using (var file = File.Open(filename, FileMode.CreateNew))
            {
                stream.CopyTo(file);
            }
        }
    }
}
