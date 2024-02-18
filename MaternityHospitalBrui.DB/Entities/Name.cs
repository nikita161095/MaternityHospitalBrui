using MaternityHospitalBrui.DB.Common;
using MaternityHospitalBrui.States;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace MaternityHospitalBrui.DB.Entities
{
    internal class Name : IEntity
    {
        public int Id { get; set; }
        public string? Use { get; set; }
        public string? Family { get; set; }
        public string? Given { get; set; }
        public Patient Patient { get; set; }
    }
}