using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoModel;
using DataAccess;


namespace TestProject
{
    [TestClass]
    public class DbIntegrationTest
    {


        [TestMethod]
        public void CantFindAnything()
        {
            using (var context = new VideoContext())
            {
                Video theRecord = context.Videos.Find("wZZ7oFKsKzY");
                Assert.IsNull(theRecord);
            }
        }



        [TestMethod]
        public void InsertAndDelete()
        {
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
            using (var context = new VideoContext())
            {
                context.Videos.Add(newvid);
                context.SaveChanges();
                Video theRecord = context.Videos.Find("wZZ7oFKsKzY");
                Assert.IsNotNull(theRecord);
            }


            using (var context = new VideoContext())
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
    }
}
