using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoModel;
using DataAccess;


namespace TestProject
{
    [TestClass]
    public class DbIntegrationTest
    {
        IVideoContext _context = new VideoContext();

        [TestMethod]
        public void CantFindInvalid()
        {
            using (var context = _context)
            {
                Video theRecord = context.Videos.Find("invalid_key_that_doesnt_exist");
                Assert.IsNull(theRecord);
            }
        }

        [TestMethod]
        public void DeleteNyanCat()
        {
            using (var context = _context)
            {
                Video theRecord = context.Videos.Find("wZZ7oFKsKzY");
                Video theRecord2 = null;

                if (theRecord != null)
                {
                    context.Videos.Remove(theRecord);
                    context.SaveChanges();
                    theRecord2 = context.Videos.Find("wZZ7oFKsKzY");
                    Assert.IsNull(theRecord2);
                }
                Assert.IsNull(theRecord2);
            }
        }

        [TestMethod]
        public void Insert()
        {
            ////////////////just make sure nyan is gone
            using (var context = _context)
            {
                Video theRecord = context.Videos.Find("wZZ7oFKsKzY");
                Video theRecord2 = null;

                if (theRecord != null)
                {
                    context.Videos.Remove(theRecord);
                    context.SaveChanges();
                    theRecord2 = context.Videos.Find("wZZ7oFKsKzY");
                    Assert.IsNull(theRecord2);
                }
                Assert.IsNull(theRecord2);

                ////////////now add nyan
                var newvid = new Video
                {
                    Id = "wZZ7oFKsKzY",
                    ChannelTitle = "Meow",
                    Comment = "Comment",
                    Dislikes = 10,
                    Likes = 11,
                    PublishDate = DateTime.Now.AddMonths(-8).ToShortDateString(),
                    Rating = "*****",
                    Title = "Nyan cat 10 hour"
                };

                //////////prove nyan was added
                context.Videos.Add(newvid);
                context.SaveChanges();
                Video theRecord3 = context.Videos.Find("wZZ7oFKsKzY");
                Assert.IsNotNull(theRecord3);
            }
        }
    }
}
