using Moq;
using NUnit.Framework;

namespace EmailTemplateLibrary.UnitTests
{
    [TestFixture]
    public class RouteCollectionTests
    {
        [Test]
        public void AddHomeRouteToCollection_ShouldResolve()
        {
            //Arrange
            RouteCollection collection = new RouteCollection();
            var dispatcher = new Mock<IDashboardDispatcher>().Object;
            collection.Add("/", dispatcher);

            //Act
            var result = collection.FindDispatcher("/");

            //Assert            
            Assert.AreEqual(true, result.Item2.Success);
        }

        [Test]
        [TestCase("/Templates")]
        [TestCase("/templates")]
        [TestCase("/tempLateS")]
        [Parallelizable(ParallelScope.All)]
        public void AddDashboardRouteToCollection_ShouldResolve(string requestPath)
        {
            //Arrange
            RouteCollection collection = new RouteCollection();
            var dispatcher = new Mock<IDashboardDispatcher>().Object;
            collection.Add("/templates", dispatcher);

            //Act
            var result = collection.FindDispatcher(requestPath);

            //Assert            
            Assert.AreEqual(true, result.Item2.Success);
        }
    }
}
