using System.Text;

namespace FileMaker
{
    class PdfMaker
    {
        private string Path { get; set; }
        private string FileName { get; set; }

        public PdfMaker(string _path, string _fileName)
        {
            Path = _path;
            FileName = _fileName;
        }
        public void CreatePDF()
        {
            var Renderer = new IronPdf.HtmlToPdf();

            var PDF = Renderer.RenderHtmlAsPdf(GetHTMLString());
            PDF.SaveAs(Path+FileName);
        }


        public static string GetHTMLString()
        {
            var sb = new StringBuilder();
            sb.Append(@"
                    <html>
                        <head>
                        </head>
                        <body>
                            <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                            <table align='center'>
                                <tr>
                                    <th>Name</th>
                                    <th>LastName</th>
                                    <th>Power</th>
                                    <th>Rank</th>
                                </tr>");

            sb.AppendFormat(@"<tr>
                            <td>{0}</td>
                            <td>{1}</td>
                            <td>{2}</td>
                            <td>{3}</td>
                            </tr>", "Johnny", "Karate", "Karate", "100");

            sb.AppendFormat(@"<tr>
                            <td>{0}</td>
                            <td>{1}</td>
                            <td>{2}</td>
                            <td>{3}</td>
                            </tr>", "Vegeta", "Prince", "Final Flash", "9000");

            sb.Append(@"
                            </table>
                        </body>
                    </html>");
            return sb.ToString();
        }
    }
    
}
