using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using prueba.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace prueba.Reports
{
    public class StudentReport
    {
        private IWebHostEnvironment _oHostEnvironment;
        public StudentReport(IWebHostEnvironment oHostEnvironment)
        {
            _oHostEnvironment = oHostEnvironment;
        }

        #region Declaracion
        int _maxColumn = 3;
        Document _document;
        Font _fontStyle;
        PdfPCell _pdfCell;
        PdfPTable _pdfTable = new PdfPTable(3);
        MemoryStream _memoryStream = new MemoryStream();

        List<Student> _oStudents = new List<Student>();

        #endregion

        public byte[] Report(List<Student> oStudents)
        {
            _oStudents = oStudents;

            _document = new Document();

            _document.SetPageSize(PageSize.Letter);
            _document.SetMargins(5f, 5f, 20f, 5f);

            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);

            PdfWriter docWrite = PdfWriter.GetInstance(_document, _memoryStream);

            _document.Open();

            float[] sizes = new float[_maxColumn];
            for(var i = 0; i < _maxColumn; i++)
            {
                if (i == 0) sizes[i] = 20;
                else sizes[i] = 100;
            }

            _pdfTable.SetWidths(sizes);

            this.ReportHeader();
            this.EmptyRow(2);
            this.ReportBody();

            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);

            _document.Close();

            return _memoryStream.ToArray();
        }

        private void ReportHeader()
        {
            _pdfCell = new PdfPCell(this.AddLogo());
            _pdfCell.Colspan = 1;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(this.SetPageTitle());
            _pdfCell.Colspan = _maxColumn - 1;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();
        }
        private PdfPTable AddLogo()
        {
            int maxColmn = 1;
            PdfPTable pdfPTable = new PdfPTable(maxColmn);

            string path = _oHostEnvironment.WebRootPath + "/Image";

            string imgCombine = Path.Combine(path, "logo.png");
            Image img = Image.GetInstance(imgCombine);

            _pdfCell = new PdfPCell(img);
            _pdfCell.Colspan = maxColmn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);

            pdfPTable.CompleteRow();

            return pdfPTable;
        }

        private PdfPTable SetPageTitle()
        {
            int maxColumn = 3;
            PdfPTable pdfPTable = new PdfPTable(maxColumn);

            _fontStyle = FontFactory.GetFont("Tahoma", 18f, 1);
            _pdfCell = new PdfPCell(new Phrase("Student Information", _fontStyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 14f, 1);
            _pdfCell = new PdfPCell(new Phrase("School Name", _fontStyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();

            return pdfPTable;
        }

        private void EmptyRow(int nCount)
        {
            for(int i = 0; i <= nCount; i++)
            {
                _pdfCell = new PdfPCell(new Phrase("", _fontStyle));
                _pdfCell.Colspan = _maxColumn;
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.Border = 0;
                _pdfCell.ExtraParagraphSpace = 10;
                _pdfTable.AddCell(_pdfCell);
                _pdfTable.CompleteRow();
            }
        }
        private void ReportBody()
        {
            var fontStyleBold = FontFactory.GetFont("Tahoma", 9f, 1);
            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);

            #region Detalles del Header
            _pdfCell = new PdfPCell(new Phrase("Id", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Name", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Address", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();
            #endregion

            #region Detalles del Body
            foreach(var oStudent in _oStudents)
            {
                _pdfCell = new PdfPCell(new Phrase(oStudent.Id.ToString(), _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(oStudent.Name, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(oStudent.Address, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);

                _pdfTable.CompleteRow();
            }
            #endregion
        }
    }
}
