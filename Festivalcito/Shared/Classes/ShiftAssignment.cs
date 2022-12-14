using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivalcito.Shared.Classes{

    public class ShiftAssignment
    {

        public int ShiftAssignmentid { get; set; }
        public int? ShiftId { get; set; }
        public int? personassignmentid { get; set; }

        public ShiftAssignment()
        {
        }

        public ShiftAssignment(int newShiftId, int newPersonassignmentid)
        {
            this.ShiftId = newShiftId;
            this.personassignmentid = newPersonassignmentid;
        }
    }
}
