using EntityFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Method
{
    ////////////////////////////////////////////////////////////////////////////////
    /////////////////////////자동화시 중복DB안넣기 위해서 DB에 파일명 넣어둠
    ////////////////////////////////////////////////////////////////////////////////
    public class SaveFileCheck
    {
       
        public static void Save(string path) {
            DbCon db = new DbCon();

            try
            {
                List<WaveFileChecks> FileChecks = new List<WaveFileChecks>()
            {
                new WaveFileChecks { FILE_NAME = path, GET_DATE = DateTime.Now}
            };

                EFBatchOperation.For(db, db.WaveFileChecks).InsertAll(FileChecks);
                FileChecks.Clear();
            }
            catch (Exception e) {
                SaveErrorLog.ErrorSave(path, e.ToString());
            }
        }
    }
}
