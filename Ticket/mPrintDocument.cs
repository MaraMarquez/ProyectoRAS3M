using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;

namespace Ticket
{
    public class mPrintDocument
    {

        /// <summary>
        /// Punto de entrada de la clase
        /// </summary>
        /// <param name=prmText">Texto que será impreso</param>"
        public mPrintDocument(string prmText)
        {
            //Constructor
            InitializeComponent();
            txtDocument.Text = prmText;
            leftmargin = pdoc.DefaultPageSettings.Margins.Left;
            topmargin = pdoc.DefaultPageSettings.Margins.Top;
        }
        private PrintDocument pdoc = new PrintDocument();
        private TextBox txtDocument = new TextBox();
        static int intCurrentChar;
        private string text = "Kic";
        private int fontsize = 10;
        private string fontname = "Courier New";
        int leftmargin = 0;
        int rightmargin = 0;
        int topmargin = 0;
        int bottommargin = 0;
        protected void InitializeComponent()
        {
            pdoc.PrintPage += new
            PrintPageEventHandler(pDoc_PrintPage);
        }

        public string Text
        {
            get
            {
                return (text);
            }
            set
            {
                text = value;
            }
        }
        /// <summary>
        /// Propiedad FontSize, Representa 
        /// el tamaño de la fuente
        /// </summary>
        /// <value>Representa el tamaño de la fuente</value>
        public int FontSize
        {
            set
            {
                fontsize = value;
            }
            get
            {
                return (fontsize);
            }
        }
        /// <summary>
        /// Propiedad FontName, Representa 
        /// el nombre de la fuente
        /// </summary>
        public string FontName
        {
            set
            {
                fontname = value;
            }
            get
            {
                return (fontname);
            }
        }
        /// <summary>
        /// Representa la distancia entre el contenido
        /// y el borde izquierdo de la página
        /// </summary>
        public int LeftMargin
        {
            set
            {
                leftmargin = value;
            }
            get
            {
                return (leftmargin);
            }
        }

        /// <summary>
        /// Representa la distancia entre el contenido
        /// y el borde derecho de la página
        /// </summary>
        public int RightMargin
        {
            set
            {
                rightmargin = value;
            }
            get
            {
                return (rightmargin);
            }
        }
        /// <summary>
        /// Representa la distancia entre el contenido 
        /// y el borde superior de la página
        /// </summary>
        public int TopMargin
        {
            set
            {
                topmargin = value;
            }
            get
            {
                return (topmargin);
            }
        }
        /// <summary>
        /// Representa la distancia entre el contenido y 
        /// el borde inferior de la página
        /// </summary>
        public int BottomMargin
        {
            set
            {
                bottommargin = value;
            }
            get
            {
                return (bottommargin);
            }
        }
        /// <summary>
        /// Mostrar en pantalla
        /// </summary>
        public void PrintPreview()
        {
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            try
            {
                ppd.Document = pdoc;
                ppd.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Error al intentar cargar " + "la vista preeliminar el documento", this.Text,
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Mandar directamente a la impresora
        /// </summary>
        public void Print()
        {
            PrintDialog dialog = new PrintDialog();
            dialog.Document = pdoc;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pdoc.Print();
            }
        }
        void pDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font(fontname, fontsize);
            int intPrintAreaHeight;
            int intPrintAreaWidth;
            intPrintAreaHeight = pdoc.DefaultPageSettings.PaperSize.Height - pdoc.DefaultPageSettings.Margins.Top - pdoc.DefaultPageSettings.Margins.Bottom;
            intPrintAreaWidth = pdoc.DefaultPageSettings.PaperSize.Width - pdoc.DefaultPageSettings.Margins.Left - pdoc.DefaultPageSettings.Margins.Right;

            if (pdoc.DefaultPageSettings.Landscape)
            {
                int intTemp = intPrintAreaHeight;
                intPrintAreaHeight = intPrintAreaWidth;
                intPrintAreaWidth = intTemp;
            }
            int intLineCount = (int)(intPrintAreaHeight / font.Height);
            RectangleF rectPrintingArea = new RectangleF(leftmargin, topmargin,
             intPrintAreaWidth, intPrintAreaHeight);
            StringFormat fmt = new StringFormat(StringFormatFlags.LineLimit);
            int intLinesFilled;
            int intCharsFitted;
            e.Graphics.MeasureString(txtDocument.Text.Substring(intCurrentChar), font,
             new SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
             out intCharsFitted, out intLinesFilled);
            e.Graphics.DrawString(txtDocument.Text.Substring(intCurrentChar), font,
             Brushes.Black, rectPrintingArea, fmt);
            intCurrentChar += intCharsFitted;
            if (intCurrentChar < (txtDocument.Text.Length - 1))
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                intCurrentChar = 0;
            }
        }
    }
}