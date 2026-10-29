using System.Security.Cryptography;

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
            Firm firm = FirmFactory.Instance.Create();


            List<string> fields = FirmFactory.Instance.GetFields();

            foreach (string field in fields)
            {
                Assert.IsNotNull(firm.GetField(field));
            }

           Assert.AreEqual(firm.GetMain().Type, TestSetup.sbFirmsTypeRepo[0]);
        }
    }
}
