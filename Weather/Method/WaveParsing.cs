using Grib.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using Weather.Model;

namespace Weather.Method
{
    ////////////////////////////////////////////////////////////////////////////////
    /////////////////////////WeatherParsing (대표님 함수)
    ////////////////////////////////////////////////////////////////////////////////
    public class WaveParsing
    {
        public List<Waves> WaveParsingReturn(string path)
        {
            using (GribFile file = new GribFile(path))

            {
                file.Context.EnableMultipleFieldMessages = true;

                DateTime UTC;
                double[] LAT = { };
                double[] LONG = { };
                double[] ICEC = { };
                double[] SWDIR_Seq1 = { };
                double[] SWDIR_Seq2 = { };
                double[] WVDIR = { };
                double[] MWSPER = { };
                double[] SWPER_Seq1 = { };
                double[] SWPER_Seq2 = { };
                double[] WVPER = { };
                double[] DIRPW = { };
                double[] PERPW = { };
                double[] DIRSW = { };
                double[] PERSW = { };
                double[] HTSGW = { };
                double[] SWELL_Seq1 = { };
                double[] SWELL_Seq2 = { };
                double[] WVHGT = { };
                double[] UGRD = { };
                double[] VGRD = { };
                double[] WDIR = { };
                double[] WIND = { };

                List<GribMessage> WaveWind = new List<GribMessage>();

                foreach (GribMessage item in file)
                {
                    if (item.StepRange != "0")
                    {
                        break;
                    }

                    WaveWind.Add(item);
                }

                UTC = WaveWind.ElementAt(0).Time;
                LAT = WaveWind.ElementAt(0).GeoSpatialValues.Select(d => d.Latitude).ToArray();
                LONG = WaveWind.ElementAt(0).GeoSpatialValues.Select(d => d.Longitude).ToArray();

                if (UTC < new DateTime(2015, 01, 07, 23, 00, 00))
                {
                    //뉴월드 날씨 2015 - 01 - 07 까지
                    WaveWind.ElementAt(14).Values(out SWDIR_Seq1);
                    WaveWind.ElementAt(13).Values(out SWDIR_Seq2);
                    WaveWind.ElementAt(7).Values(out WVDIR);
                    WaveWind.ElementAt(11).Values(out SWPER_Seq1);
                    WaveWind.ElementAt(10).Values(out SWPER_Seq2);
                    WaveWind.ElementAt(5).Values(out WVPER);
                    WaveWind.ElementAt(8).Values(out DIRPW);
                    WaveWind.ElementAt(6).Values(out PERPW);
                    WaveWind.ElementAt(4).Values(out HTSGW);
                    WaveWind.ElementAt(2).Values(out UGRD);
                    WaveWind.ElementAt(3).Values(out VGRD);
                    WaveWind.ElementAt(1).Values(out WDIR);
                    WaveWind.ElementAt(0).Values(out WIND);
                }
                else if

                     (UTC <= new DateTime(2015, 09, 02, 23, 00, 00))
                {
                    //뉴월드 날씨2 2015 - 01 - 08 부터 2015 - 09 - 02 까지
                    WaveWind.ElementAt(17).Values(out SWDIR_Seq1);
                    WaveWind.ElementAt(16).Values(out SWDIR_Seq2);
                    WaveWind.ElementAt(7).Values(out WVDIR);
                    WaveWind.ElementAt(14).Values(out SWPER_Seq1);
                    WaveWind.ElementAt(13).Values(out SWPER_Seq2);
                    WaveWind.ElementAt(5).Values(out WVPER);
                    WaveWind.ElementAt(8).Values(out DIRPW);
                    WaveWind.ElementAt(6).Values(out PERPW);
                    WaveWind.ElementAt(4).Values(out HTSGW);
                    WaveWind.ElementAt(11).Values(out SWELL_Seq1);
                    WaveWind.ElementAt(10).Values(out SWELL_Seq2);
                    WaveWind.ElementAt(9).Values(out WVHGT);
                    WaveWind.ElementAt(2).Values(out UGRD);
                    WaveWind.ElementAt(3).Values(out VGRD);
                    WaveWind.ElementAt(1).Values(out WDIR);
                    WaveWind.ElementAt(0).Values(out WIND);
                }
                else
                {
                    //// Lab021 날씨
                    WaveWind.ElementAt(4).Values(out ICEC);

                    WaveWind.ElementAt(17).Values(out SWDIR_Seq1);
                    WaveWind.ElementAt(18).Values(out SWDIR_Seq2);
                    WaveWind.ElementAt(16).Values(out WVDIR);
                    WaveWind.ElementAt(6).Values(out MWSPER);
                    WaveWind.ElementAt(14).Values(out SWPER_Seq1);
                    WaveWind.ElementAt(15).Values(out SWPER_Seq2);
                    WaveWind.ElementAt(13).Values(out WVPER);
                    WaveWind.ElementAt(9).Values(out DIRPW);
                    WaveWind.ElementAt(7).Values(out PERPW);
                    WaveWind.ElementAt(5).Values(out HTSGW);
                    WaveWind.ElementAt(11).Values(out SWELL_Seq1);
                    WaveWind.ElementAt(12).Values(out SWELL_Seq2);
                    WaveWind.ElementAt(10).Values(out WVHGT);
                    WaveWind.ElementAt(2).Values(out UGRD);
                    WaveWind.ElementAt(3).Values(out VGRD);
                    WaveWind.ElementAt(1).Values(out WDIR);
                    WaveWind.ElementAt(0).Values(out WIND);
                }

                List<Waves> wavedataout = new List<Waves> { };

                for (int i = 0; i < LAT.Length; i++)
                {
                    if (UTC < new DateTime(2015, 01, 07, 23, 00, 00))
                    {
                        wavedataout.Add(new Waves
                        {
                            // 뉴월드 날씨2 2015-01-07 까지

                            UTC = UTC,
                            lat = LAT[i],
                            lon = LONG[i],
                            ICEC = 9998,
                            SWDIR_Seq1 = SWDIR_Seq1[i],
                            SWDIR_Seq2 = SWDIR_Seq2[i],
                            WVDIR = WVDIR[i],
                            MWSPER = 9998,
                            SWPER_Seq1 = SWPER_Seq1[i],
                            SWPER_Seq2 = SWPER_Seq2[i],
                            WVPER = WVPER[i],
                            DIRPW = DIRPW[i],
                            PERPW = PERPW[i],
                            HTSGW = HTSGW[i],
                            SWELL_Seq1 = 9998,
                            SWELL_Seq2 = 9998,
                            WVHGT = 9998,
                            UGRD = UGRD[i],
                            VGRD = VGRD[i],
                            WDIR = WDIR[i],
                            WIND = WIND[i]


                        });
                    }
                    else if (UTC < new DateTime(2015, 09, 02, 23, 00, 00))
                    {
                        wavedataout.Add(new Waves
                        {
                            // 뉴월드 날씨2 2015-01-08 부터

                            UTC = UTC,
                            lat = LAT[i],
                            lon = LONG[i],
                            ICEC = 9998,
                            SWDIR_Seq1 = SWDIR_Seq1[i],
                            SWDIR_Seq2 = SWDIR_Seq2[i],
                            WVDIR = WVDIR[i],
                            MWSPER = 9998,
                            SWPER_Seq1 = SWPER_Seq1[i],
                            SWPER_Seq2 = SWPER_Seq2[i],
                            WVPER = WVPER[i],
                            DIRPW = DIRPW[i],
                            PERPW = PERPW[i],
                            HTSGW = HTSGW[i],
                            SWELL_Seq1 = SWELL_Seq1[i],
                            SWELL_Seq2 = SWELL_Seq2[i],
                            WVHGT = WVHGT[i],
                            UGRD = UGRD[i],
                            VGRD = VGRD[i],
                            WDIR = WDIR[i],
                            WIND = WIND[i]
                        });
                    }
                    else
                    {
                        wavedataout.Add(new Waves
                        {
                            //lab021 2015-09-02

                            UTC = UTC,
                            lat = LAT[i],
                            lon = LONG[i],
                            ICEC = ICEC[i],
                            SWDIR_Seq1 = SWDIR_Seq1[i],
                            SWDIR_Seq2 = SWDIR_Seq2[i],
                            WVDIR = WVDIR[i],
                            MWSPER = MWSPER[i],
                            SWPER_Seq1 = SWPER_Seq1[i],
                            SWPER_Seq2 = SWPER_Seq2[i],
                            WVPER = WVPER[i],
                            DIRPW = DIRPW[i],
                            PERPW = PERPW[i],
                            HTSGW = HTSGW[i],
                            SWELL_Seq1 = SWELL_Seq1[i],
                            SWELL_Seq2 = SWELL_Seq2[i],
                            WVHGT = WVHGT[i],
                            UGRD = UGRD[i],
                            VGRD = VGRD[i],
                            WDIR = WDIR[i],
                            WIND = WIND[i]
                        });
                    }
                }


                return wavedataout;
            }
        }
    }
}