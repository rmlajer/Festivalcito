using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivalcito.Shared.Classes{

    public class ShiftAssigned{

        public int ShiftAssignedlistId { get; set; }
        public int? ShiftId { get; set; }
        public int? AssignmentId { get; set; }

        public ShiftAssigned()
        {
        }
    }
}
