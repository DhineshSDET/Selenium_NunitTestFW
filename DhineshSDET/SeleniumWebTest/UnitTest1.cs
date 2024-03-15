namespace SeleniumWebTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Setup Method Execution");
        }

        [Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("This is Test1 Method");
        }
        [Test]
        public void Test2()
        {
            TestContext.Progress.WriteLine("This is Test2 Method");
        }

        [TearDown]
        public void BrowserClose()
        {
            TestContext.Progress.WriteLine("TearDown Method");
        }
    }
}