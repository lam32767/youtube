using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using VideoModel;


namespace DataAccess
{
    public interface IVideoContext:IDisposable
    {
        DbSet<Video> Videos { get; }
        int SaveChanges();
        IQueryable<Video> GetVideos();
        Video FindVideoById(string id);
    }
}
