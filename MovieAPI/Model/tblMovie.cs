// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MovieAPI_Context.Model
{
    public partial class TblMovie
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public DateTime? DateOfRealese { get; set; }
        public int? ActorId { get; set; }
        public int? ProducerId { get; set; }
    }
}