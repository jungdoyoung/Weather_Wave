using EntityFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Weather.Model;

namespace Weather.Method
{

    ////////////////////////////////////////////////////////////////////////////////
    /////////////////////////에러난 파일 테스트  (그냥 테스트용)
    ////////////////////////////////////////////////////////////////////////////////
    public class FileSearchTest
    {
        object obj = new object();
        DbCon db = new DbCon();
        Form1 Frm;
        Label lblTotalCount, lblIngCount;
        RichTextBox richTextBox;
        ProgressBar Progress;

        public FileSearchTest(Form1 Frm)
        {
            this.Frm = Frm;
        }

        public  void Find()
        {
           
            string fullPath = @"I:\1\nww3_weather\2015\20150617\nww3.t18z.grib.grib2";
            WaveParsing waveParse = new WaveParsing();
            var wavedataout = waveParse.WaveParsingReturn(fullPath);

            EFBatchOperation.For(db, db.Waves).InsertAll(wavedataout);
            wavedataout.Clear();      
        }
    }
}
