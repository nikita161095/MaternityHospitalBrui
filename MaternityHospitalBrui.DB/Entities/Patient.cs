using MaternityHospitalBrui.DB.Common;
using MaternityHospitalBrui.States;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace MaternityHospitalBrui.DB.Entities
{
    internal class Patient : IEntity
    {
        public int Id { get; set; }
        public int NameId { get; set; }
        public Name Name { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
    }
}