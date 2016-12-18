using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using VideoModel;
using System.Web.Mvc;
using System.Linq;

namespace TestProject
{
    [TestClass]
    public class MockTests
    {
        [TestMethod]
        public void TestVideoContextWithMock()
        {
            var mocklist=new List<Video>
                {
                    new VideoModel.Video() { ChannelTitle="title1", Comment="comment1", Dislikes=1, Id="id1", Likes=1, Rating="*", Title="title1", PublishDate=DateTime.Today },
                    new VideoModel.Video() { ChannelTitle="title2", Comment="comment2", Dislikes=2, Id="id2", Likes=2, Rating="**", Title="title2", PublishDate=DateTime.Today }
                }.AsQueryable();

            var mockContext = new Mock<DataAccess.VideoContext>();
            mockContext.Setup(m => m.GetVideos()).Returns(mocklist);

            List<Video> testoutput = mockContext.Object.GetVideos().ToList();
            Assert.IsTrue(testoutput.Count == 2);
            Video first = testoutput.First();
            Assert.AreEqual(first.ChannelTitle, "title1");
            Assert.AreEqual(first.Dislikes, 1);
            Assert.AreEqual(first.Comment, "comment1");
            Assert.AreEqual(first.Likes, 1);
            Assert.AreEqual(first.PublishDate, DateTime.Today);
            Assert.AreEqual(first.Id, "id1");
            Assert.AreEqual(first.Rating, "*");

            Video last = testoutput.Last();
            Assert.AreEqual(last.ChannelTitle, "title2");
            Assert.AreEqual(last.Dislikes, 2);
            Assert.AreEqual(last.Comment, "comment2");
            Assert.AreEqual(last.Likes, 2);
            Assert.AreEqual(last.PublishDate, DateTime.Today);
            Assert.AreEqual(last.Id, "id2");
            Assert.AreEqual(last.Rating, "**");

        }

        [TestMethod]
        public void HelpPage()
        {
            var controller = new MyYouTube.Controllers.HomeController();
            Assert.AreEqual(typeof(ViewResult), controller.Help().GetType());
        }
        [TestMethod]
        public void TechHelpPage()
        {
            var controller = new MyYouTube.Controllers.HomeController();
            Assert.AreEqual(typeof(ViewResult), controller.Search().GetType());
        }
        [TestMethod]
        public void SearchPage()
        {
            var controller = new MyYouTube.Controllers.HomeController();
            Assert.AreEqual(typeof(ViewResult), controller.HelpTech().GetType());
        }
    }
}
