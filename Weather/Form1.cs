using Microsoft.Research.Science.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Weather.Method;
using Weather.Model;
using EntityFramework.Utilities;
using System.IO;
using System.Diagnostics;

namespace Weather
{
    public partial class Form1 : Form
    {
        ////////////////////////////////////////////////////////////////
        ///////////////////전역변수
        ////////////////////////////////////////////////////////////////
        DbCon db = new DbCon();
        Thread T1;
        DirectorySearch directorySearch;

        //FileSearchTest fileSearchTest;


        ////////////////////////////////////////////////////////////////
        ///////////////////생성자
        ////////////////////////////////////////////////////////////////

        public Form1()
        {
            InitializeComponent();
            directorySearch = new DirectorySearch(this);
            //fileSearchTest = new FileSearchTest(this);  //에러난 파일 테스트

        }

        private void btnStart_Click(object sender, EventArgs e)
        {


            WavePatsing();

        }

        ////////////////////////////////////////////////////////////////////////////////
        /////////////////////////에러난 파일 테스트
        ////////////////////////////////////////////////////////////////////////////////
        //public void FileTest()
        //{
        //    fileSearchTest.Find();
        //}


        ////////////////////////////////////////////////////////////////////////////////
        /////////////////////////Weather Pasing ThreadMethod
        ////////////////////////////////////////////////////////////////////////////////
        public void WavePatsing() {
            //경로 지정 I:\Wave0402\2014~2016
            string serverPath = @"\\192.168.0.2\날씨데이터\Wave";

            DirectoryInfo di = new DirectoryInfo(serverPath);

            foreach(var forder in di.GetDirectories().OrderBy(s=>s.FullName))
            {
                //파라메터명 (string path, ProgressBar progressBar, Label lblTotalCount, Label lblIngCount, RichTextBox richTextBox)
                directorySearch.Find(forder.FullName, progressBar1, lblTotalCount, lblIngCount, richTextBoxLog);

            }
        }


  
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var oceanout = OceanParsing();
            //  EFBatchOperation.For(db, db.Oceans).InsertAll(oceanout);

        }



        private static List<Oceans> OceanParsing()
        {
            List<Oceans> oceanModel = new List<Oceans>();

            DataSet ocean = DataSet.Open("1.nc");

            List<Array> box = new List<Array> { };

            foreach (var item in ocean.Variables)
            {
                box.Add(item.GetData());
            }

            float[] tttt = new float[] { };

            var tt = box.ElementAt(0);

            var number = tt.GetLength(0) * tt.GetLength(2) * tt.GetLength(3);
            DateTime[] UTC = new DateTime[number];
            object[] LAT = new object[number];
            object[] LONG = new object[number];
            object[] u = new object[number];
            object[] v = new object[number];

            for (int i = 0; i < tt.GetLength(0); i++)
            {
                for (int j = 0; j < tt.GetLength(2); j++)
                {
                    for (int k = 0; k < tt.GetLength(3); k++)
                    {
                        LONG[i * tt.GetLength(2) * tt.GetLength(3) + j * tt.GetLength(3) + k] = box.ElementAt(2).GetValue(k);

                        LAT[i * tt.GetLength(2) * tt.GetLength(3) + j * tt.GetLength(3) + k] = box.ElementAt(3).GetValue(j);

                        UTC[i * tt.GetLength(2) * tt.GetLength(3) + j * tt.GetLength(3) + k] = new DateTime(2014, 07, 17, 00, 00, 00).AddDays(i * 5);

                        if (Convert.ToString(tt.GetValue(i, 0, j, k)) == "NaN")
                        {
                            v[i * tt.GetLength(2) * tt.GetLength(3) + j * tt.GetLength(3) + k] = 9999;
                        }
                        else
                        {
                            v[i * tt.GetLength(2) * tt.GetLength(3) + j * tt.GetLength(3) + k] = box.ElementAt(0).GetValue(i, 0, j, k);
                        }
                    }
                }
            }

            for (int i = 0; i < tt.GetLength(0); i++)
            {
                for (int j = 0; j < tt.GetLength(2); j++)
                {
                    for (int k = 0; k < tt.GetLength(3); k++)
                    {
                        if (Convert.ToString(tt.GetValue(i, 0, j, k)) == "NaN")
                        {
                            u[i * tt.GetLength(2) * tt.GetLength(3) + j * tt.GetLength(3) + k] = 9999;
                        }
                        else
                        {
                            u[i * tt.GetLength(2) * tt.GetLength(3) + j * tt.GetLength(3) + k] = box.ElementAt(1).GetValue(i, 0, j, k);
                        }
                    }
                }
            }

            for (int i = 0; i < LAT.Length; i++)
            {
                oceanModel.Add(new Oceans
                {
                    UTC = UTC[i],
                    i = Convert.ToSingle(LAT[i]),
                    j = Convert.ToSingle(LONG[i]),
                    Current_UV = Convert.ToSingle(u[i]),
                    Current_VV = Convert.ToSingle(v[i]),
                });
            }

            return oceanModel;
        }
    }
}
