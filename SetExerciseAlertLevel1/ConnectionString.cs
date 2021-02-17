using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetExerciseAlertLevel1
{
    public class ConnectionString
    {
        public static string GetConnectionString()
        {
            string conString = "Server=tcp:azure-latte-database.database.windows.net,1433;Initial Catalog=LocalWeaponControlStatus;Persist Security Info=False;User ID=latteorganizationza;Password=Lattedragon9%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return conString;
        }
    }
}
