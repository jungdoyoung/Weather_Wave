using EntityFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Weather.Model;

namespace Weather.Method
{
    
    public class DirectorySearch
    {
        object obj = new object();
        DbCon db = new DbCon();
        Form1 Frm;
        Label lblTotalCount, lblIngCount;
        RichTextBox richTextBox;
        ProgressBar Progress;

        public DirectorySearch(Form1 Frm) {
            this.Frm = Frm;
                       
        }

        public void Find(string path, ProgressBar progressBar, Label lblTotalCount, Label lblIngCount, RichTextBox richTextBox) {
            Monitor.Enter(obj);
            
            try
            {
                this.Progress = progressBar;
                this.lblTotalCount = lblTotalCount;
                this.lblIngCount = lblIngCount;
                this.richTextBox = richTextBox;
                

                if (Directory.Exists(path)) //폴더경로가 있는것만
                {
                    

                    int equerCount = 0;
                    //폴더명 읽음
                    DirectoryInfo di = new DirectoryInfo(path);

                    //파일 갯수 구하기
                    int diCount = Directory.GetFiles(path, "*.grib2", SearchOption.AllDirectories).Length;
                    int dbCount = db.WaveFileChecks.Where(s => s.FILE_NAME.Contains(path)).Count();
                    int MaxProgressCount = diCount; /*- dbCount;*/

                    //프로그래스바
                    Frm.Invoke((MethodInvoker)delegate {

                        progressBar.Maximum = MaxProgressCount;

                    });

                    int ingCount = 1;
                    foreach (var forder in di.GetDirectories().OrderBy(s => s.Name))  //이름순으로 정렬
                    {

                        foreach (var files in forder.GetFiles("*.grib2").OrderBy(s => s.Name))  //grib2파일만
                        {


                            string fullPath = path + "\\" + forder + "\\" + files;



                            ///////////////////////////////////////////////////////////////
                            //////////////////파일 중복 체크
                            ///////////////////////////////////////////////////////////////

                            foreach (var filecheck in db.WaveFileChecks)
                            {
                                /*
                                  기존 DB 중복 파일 I:\Wave\2016\20160530\gwes00.glo_30m.t18z.grib2 되어있어서
                                  중복 체크가 안되서 Wave로 split 한다음 그뒤부분 부터 중복 검사
                                  ex)\2016\20160530\gwes00.glo_30m.t18z.grib2 == \2016\20160530\gwes00.glo_30m.t18z.grib2
                                */
                            
                                var filechecks = Regex.Split(filecheck.FILE_NAME.Trim(),"Wave")[1];
                                var fullPaths  = Regex.Split(fullPath.Trim(),"Wave")[1];

                                if (filechecks == fullPaths)
                                {
                                    equerCount++;
                                    var aa = 0;
                                }
                            }

                            ///////////////////////////////////////////////////////////////
                            //////////////////새로운 파일만 DB넣음
                            ///////////////////////////////////////////////////////////////
                            

                            if (equerCount == 0)
                            {
                                try
                                {

                                    ///////////////////////////////////////////////////////////////
                                    //////////////////진행중 프로그래스바, totalCount, ingCount
                                    ///////////////////////////////////////////////////////////////

                                    Frm.Invoke((MethodInvoker)delegate {
                                        lblTotalCount.Text = MaxProgressCount.ToString();
                                        lblIngCount.Text = ingCount.ToString();
                                        richTextBox.AppendText(fullPath + "\n");

                                        if (ingCount > progressBar.Maximum)
                                        {
                                            ingCount = progressBar.Maximum;
                                        }
                                        else
                                        {
                                            progressBar.Value = ingCount;
                                        }
                                        

                                    });
                                    ///////////////////////////////////////////////////////////////
                                    ////////////////// DB넣음
                                    ///////////////////////////////////////////////////////////////

                                    WaveParsing waveParse = new WaveParsing();


                                    var wavedataout = waveParse.WaveParsingReturn(fullPath);
                                    this.db.Database.CommandTimeout = 9999;
                                    EFBatchOperation.For(db, db.Waves).InsertAll(wavedataout);
                                    ingCount++;
                                }
                                catch //(Exception e) //파일에러났을때 프로그램이 멈춰서 주석처리함
                                {
                                    //SaveErrorLog.ErrorSave(fullPath, e.ToString());
                                }
                                SaveFileCheck.Save(fullPath);

                            }
                            equerCount = 0; //중복 체크 하기위해서는 초기화 해줘야함

                        }


                    }


                }
                ///////////////////////////////////////////////////////////////
                //////////////////UI 초기화
                ///////////////////////////////////////////////////////////////

                string initCount = "0";
                Frm.Invoke((MethodInvoker)delegate {
                    lblTotalCount.Text = initCount;
                    lblIngCount.Text = initCount;
                    richTextBox.Text= "";
                    progressBar.Value = 0;

                });

            }

            catch (Exception e) {
                SaveErrorLog.ErrorSave("동기화 에러", e.ToString());
            }
            finally { Monitor.Exit(obj); }

        }
    }
}
