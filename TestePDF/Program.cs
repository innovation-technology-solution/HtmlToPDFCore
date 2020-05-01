using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using HtmlToPDFCore;

namespace TestePDF
{
    class Program
    {
        static void Main(string[] args)
        {
            var htmlCode = "<html><body><b>TESTE PDF</b></body></html>";

            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("https://colorlib.com/wp/template/shutter/");
            }

            var pdfFile = @"teste.pdf";

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                htmlCode = "<html><body><b>TESTE PDF no Linux</b></body></html>";
                pdfFile = "teste-linux.pdf";
            }

            var pdf = new HtmlToPDF();
            var buffer = pdf.ReturnPDF(htmlCode);
            if(File.Exists(pdfFile)) File.Delete(pdfFile);
            using(var f = new FileStream(pdfFile,FileMode.Create))
            {
                f.Write(buffer,0,buffer.Length);
                f.Flush();
                f.Close();
            }
        }
    }
}
