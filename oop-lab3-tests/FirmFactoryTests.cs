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
            List<string> firmFields = firm.UsrFields.Keys.ToList();
            Assert.AreEqual(5, fields.Count);
            for (int i = 0; i < fields.Count; i++)
            {
                Assert.AreEqual(fields[i], firmFields[i]);
            }
            Assert.AreEqual(firm.GetMain().Type, TestSetup.sbFirmsTypeRepo[0]);
        }

        [TestMethod]
        public void FieldRenaming()
        {
            FirmFactory factory = FirmFactory.Instance;
            Firm firm = factory.Create();

            List<string> values = new List<string>{ "0", "1", "2", "3", "4" };
            List<string> fields = factory.GetFields();
            for (int i = 0; i < fields.Count; i++)
            {
                firm.SetField(fields[i], values[i]);
            }

            for (int i = 0; i < fields.Count; i++)
            {
                string oldName = fields[i];
                string newName = "Renamed_" + oldName;
                firm.RenameField(oldName, newName);
            }

            for (int i = 0; i < fields.Count; i++)
            {
                string newName = "Renamed_" + fields[i];
                Assert.IsNull(firm.GetField(fields[i]));
                Assert.IsNotNull(firm.GetField(newName));

                Assert.AreEqual(firm.GetField(newName), values[i]);
            }
        }
    }
}
