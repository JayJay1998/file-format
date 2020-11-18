using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using File.Models.Mapping;

namespace File.Models
{
    public partial class RecordTableContext : DbContext
    {
        static RecordTableContext()
        {
            Database.SetInitializer<RecordTableContext>(null);
        }

        public RecordTableContext()
            : base("Name=RecordTableContext")
        {
        }

        
    }
}
