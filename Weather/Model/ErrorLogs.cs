namespace Weather.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ErrorLogs
    {
        public long ID { get; set; }

        public string FILE_NAME { get; set; }

        public string ERROR_LOG { get; set; }

        public DateTime GET_DATE { get; set; }
    }
}
