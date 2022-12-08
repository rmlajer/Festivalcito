using System;
using Festivalcito.Server.Models.ShiftRepositoryFolder;
namespace Festivalcito.Server.Models
{
	internal class PgAdminContext
	{
        public string connstring = "";

        public PgAdminContext()
		{
            connstring = "UserID=arvppmkz;Password=PRfiDeTpnyfXNpAqQ221u9tQsx2_RUrV;Host=mouse.db.elephantsql.com;Port=5432;Database=arvppmkz";
        }
    }
}

