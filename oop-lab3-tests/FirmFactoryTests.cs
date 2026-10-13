namespace oop_lab3_tests
{
    [TestClass]
    public class FirmFactoryTests
    {
        [TestMethod]
        public void InstanceReturnsSameFactoryTest()
        {
            FirmFactory first = FirmFactory.Instance;
            FirmFactory second = FirmFactory.Instance;

            Assert.AreSame(first, second);
        }

        [TestMethod]
        public void CreateCreatesFirmTest()
        {
            FirmFactory factory = FirmFactory.Instance;

            Firm firm = factory.Create();

            Assert.IsNotNull(firm);
            Assert.AreEqual(1, firm.SbFirms.Count);
        }

        [TestMethod]
        public void CreateAddsMainSubFirmTest()
        {
            FirmFactory factory = FirmFactory.Instance;

            Firm firm = factory.Create();

            SubFirm mainSubFirm = firm.GetMain();

            Assert.IsTrue(mainSubFirm.IsMain);
        }
    }
}
