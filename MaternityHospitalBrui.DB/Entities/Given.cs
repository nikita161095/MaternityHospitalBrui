using MaternityHospitalBrui.DB.Common;
using MaternityHospitalBrui.States;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace MaternityHospitalBrui.DB.Entities
{
    internal class Given : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}