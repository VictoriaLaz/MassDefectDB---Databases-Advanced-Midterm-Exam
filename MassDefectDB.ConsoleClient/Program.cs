
using MassDefectDB.Data;

namespace MassDefectDB.ConsoleClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            MassDefectContext context = new MassDefectContext();
            context.Database.Initialize(true);
        }
    }
}
