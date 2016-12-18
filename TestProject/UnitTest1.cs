using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using VideoModel;
using System.Data.Entity;
using System.Linq;

namespace TestProject
{
    [TestClass]
    public class Mockery
    {
        [TestMethod]
        public void TestWithMock()
        {
            var mocklist=new List<Video>
                {
                    new VideoModel.Video() { ChannelTitle="title1", Comment="comment1", Dislikes=1, Id="id1", Likes=1, Rating="*", Title="title1", PublishDate=DateTime.Today.ToShortDateString() },
                    new VideoModel.Video() { ChannelTitle="title2", Comment="comment2", Dislikes=2, Id="id2", Likes=2, Rating="**", Title="title2", PublishDate=DateTime.Today.ToShortDateString() }
                }.AsQueryable();

            var mockSet = new Mock<System.Data.Entity.DbSet<Video>>();
            mockSet.As<IQueryable<Video>>().Setup(m => m.Provider).Returns(mocklist.Provider);
            mockSet.As<IQueryable<Video>>().Setup(m => m.Expression).Returns(mocklist.Expression);
            mockSet.As<IQueryable<Video>>().Setup(m => m.ElementType).Returns(mocklist.ElementType);
            mockSet.As<IQueryable<Video>>().Setup(m => m.GetEnumerator()).Returns(mocklist.GetEnumerator());
            var mockContext = new Mock<DataAccess.VideoContext>();

            mockContext.Setup(m => m.GetVideos()).Returns(mocklist);
            List<Video> testoutput = mockContext.Object.GetVideos().ToList();
            Assert.IsTrue(testoutput.Count == 2);

            Video first = testoutput.First();
            Assert.AreEqual(first.ChannelTitle, "title1");
            Assert.AreEqual(first.Dislikes, 1);
            Video last = testoutput.Last();
            Assert.AreEqual(last.ChannelTitle, "title2");
            Assert.AreEqual(last.Dislikes, 2);

        }
    }
}
