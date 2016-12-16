using System.Data.Entity;
using VideoModel;

namespace DataAccess
{
    public class VideoContext : DbContext
    {
        public VideoContext() : base("name=Ubbycat")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<Video> Videos { get; set; }
    }
}