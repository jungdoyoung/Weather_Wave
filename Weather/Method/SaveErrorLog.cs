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
    /////////////////////////에러 로그 저장
    ////////////////////////////////////////////////////////////////////////////////
    public class SaveErrorLog
    {
        public static void ErrorSave(string path, string errLog) {
            DbCon db = new DbCon();
            List<ErrorLogs> ErrorLogs = new List<ErrorLogs>() {
                new ErrorLogs { FILE_NAME=path, ERROR_LOG = errLog}
            };

            EFBatchOperation.For(db, db.ErrorLogs).InsertAll(ErrorLogs);
            ErrorLogs.Clear();
        }
    }
}
