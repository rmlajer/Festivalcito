using System;
namespace Festivalcito.Client.Services.ShiftServicesFolder
{
	public interface IShiftService
	{
        Task<Shelter[]?> GetAllItems();
        Task<CustomerBooking[]?> GetAllBookings();

        Task<int> AddItem(CustomerBooking costumerInfo);
        Task<int> DeleteItem(string id);
        Task<Shelter> GetItem(string id);
        Task<int> UpdateItem(CustomerBooking item);



    }
}

