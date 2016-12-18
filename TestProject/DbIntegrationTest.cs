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
        public void DeleteDefaultHanselmanVideo()
        {
            using (var context = _context)
            {
                Video theRecord = context.Videos.Find("H2KkiRbDZyc");
                Video theRecord2 = null;

                if (theRecord != null)
                {
                    context.Videos.Remove(theRecord);
                    context.SaveChanges();
                    theRecord2 = context.Videos.Find("H2KkiRbDZyc");
                    Assert.IsNull(theRecord2);
                }
                Assert.IsNull(theRecord2);
            }
        }

        [TestMethod]
        public void Insert()
        {
            ////////////////just make sure scott is gone
            using (var context = _context)
            {
                Video theRecord = context.Videos.Find("H2KkiRbDZyc");
                Video theRecord2 = null;

                if (theRecord != null)
                {
                    context.Videos.Remove(theRecord);
                    context.SaveChanges();
                    theRecord2 = context.Videos.Find("H2KkiRbDZyc");
                    Assert.IsNull(theRecord2);
                }
                Assert.IsNull(theRecord2);

                ////////////now add scott
                var newvid = new Video
                {
                    Id = "H2KkiRbDZyc",
                    ChannelTitle = "Scott",
                    Comment = "Comment",
                    Dislikes = 10,
                    Likes = 11,
                    PublishDate = DateTime.Now.AddMonths(-8),
                    Rating = "*****",
                    Title = "Hanselman's Video"
                };

                //////////prove scott was added
                context.Videos.Add(newvid);
                context.SaveChanges();
                Video theRecord3 = context.Videos.Find("H2KkiRbDZyc");
                Assert.IsNotNull(theRecord3);
            }
        }
    }
}
