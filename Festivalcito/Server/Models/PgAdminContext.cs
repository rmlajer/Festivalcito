using System;
using Festivalcito.Server.Models.ShiftRepositoryFolder;
namespace Festivalcito.Server.Models
{
	internal class PgAdminContext
	{
        public string connstring = "";

        public PgAdminContext()
		{
            connstring = "UserID=postgres;Password=sally0312;Host=localhost;Port=5432;Database=DesignSkema";
        }
    }
}

