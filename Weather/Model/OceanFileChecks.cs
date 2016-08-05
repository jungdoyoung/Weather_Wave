namespace Weather.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OceanFileChecks
    {
        public int ID { get; set; }

        public string FILE_NAME { get; set; }

        public long FILE_SIZE { get; set; }

        public DateTime TODAY { get; set; }
    }
}
