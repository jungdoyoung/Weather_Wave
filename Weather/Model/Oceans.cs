namespace Weather.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Oceans
    {
        public long ID { get; set; }

        public DateTime UTC { get; set; }

        public float i { get; set; }

        public float j { get; set; }

        public float DENSITY { get; set; }

        public float SSS { get; set; }

        public float SST { get; set; }

        public float Current_UV { get; set; }

        public float Current_VV { get; set; }
    }
}
