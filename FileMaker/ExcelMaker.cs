using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMaker
{
    class ExcelMaker
    {
        public class ExcelData
        {
            public string Name { get; set; }
            public string Anime { get; set; }
            public string PowerLevel { get; set; }
            public string SecretMove { get; set; }
            public string Rank { get; set; }
        }
        private string FileName { get; set; }

        List<ExcelData> people = new List<ExcelData>()
        {
            new ExcelData() {Rank="1", Name="Goku-san", Anime ="DBZ", PowerLevel="10000M", SecretMove ="Kamehameha"},
            new ExcelData() {Rank="2", Name="Saitama", Anime ="One-Punch Man", PowerLevel="90000M", SecretMove ="Super Serious Punch"},
            new ExcelData() {Rank="3", Name="Ainz Ooal Gown ", Anime ="Overlord", PowerLevel="10000", SecretMove ="Bahemoth"},
            new ExcelData() {Rank="4", Name="Sora", Anime ="Kingdom Hearts", PowerLevel="99", SecretMove ="Mega Flare"}
        };
        public ExcelMaker(string _fileName)
        {
            FileName = _fileName;
        }
        
        public void CreateExcelFile()
        {

            // Convert in table 
            DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(people), (typeof(DataTable)));
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(FileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);

                Row headerRow = new Row();

                List<String> columns = new List<string>();
                foreach (System.Data.DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);

                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(column.ColumnName);
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dsrow in table.Rows)
                {
                    Row newRow = new Row();
                    foreach (String col in columns)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(dsrow[col].ToString());
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }
        }
    }
}
