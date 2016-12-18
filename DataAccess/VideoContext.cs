﻿using System.Data.Entity;
using VideoModel;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class VideoContext : DbContext, IVideoContext
    {
        public VideoContext() : base("name=YouTubeDBConnectionString")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<Video> Videos { get; set; }

        public virtual IQueryable<Video> GetVideos()
        {
            return Videos.Where(x => x.Id != null);
        }

        public virtual Video FindVideoById(string id)
        {
            return Videos.Find(id);
        }
    }
}