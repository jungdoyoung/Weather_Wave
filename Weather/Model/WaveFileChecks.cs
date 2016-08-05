namespace Weather.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WaveFileChecks
    {
        public long ID { get; set; }

        public string FILE_NAME { get; set; }

        public DateTime GET_DATE { get; set; }
    }
}
