using System;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Components;

namespace Festivalcito.Client.Shared{

	partial class ShiftCalenderCell{

        [Parameter]
        public Shift? shift { get; set; }

        ShiftAssignment tmpShiftAssignment = new ShiftAssignment();

        [Parameter, EditorRequired]
        public EventCallback<Shift> TakeShift { get; set; }

        [Parameter, EditorRequired]
        public EventCallback<Shift> RemoveShift { get; set; }

        public async Task TakeShiftClicked()
        {
            await TakeShift.InvokeAsync(shift);
        }

        public async Task RemoveShiftClicked()
        {
            await RemoveShift.InvokeAsync(shift);
        }

        public string ShiftButtonStatus(Shift shift, string button){
            if (shift.IsLocked == true)
            {
                return "none";
            }
            if (button == "takeButton")
            {
                if (shift.backgroundColor == "red")
                {
                    return "none";
                }
                else
                {
                    return "inline-block";
                }
            }

            if (button == "removeButton")
            {
                if (shift.backgroundColor == "red")
                {
                    return "inline-block";
                }
                else
                {
                    return "none";
                }
            }
            if (shift.IsLocked != false)
            {
                shift.backgroundColor = "grey";
                return "none";
            }
            else
            {
                return "inline-block";
            }
        }




    }
}

