using System;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Components;


namespace Festivalcito.Client.Shared{

	partial class CoordinatorShiftOverview{

        [Parameter, EditorRequired]
        public List<Shift>? PresentedShiftsListFromParent { get; set; }

        [Parameter, EditorRequired]
        public EventCallback<Shift> SelectShift { get; set; }

        [Parameter, EditorRequired]
        public EventCallback<int> DeleteShift { get; set; }

        public async Task SelectShiftClicked(Shift shift)
        {
            await SelectShift.InvokeAsync(shift);
        }

        public async Task DeleteShiftClicked(int shiftId)
        {
            await DeleteShift.InvokeAsync(shiftId);
        }



    }
}

