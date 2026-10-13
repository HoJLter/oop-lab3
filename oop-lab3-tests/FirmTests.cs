namespace oop_lab3_tests
{
    [TestClass]
    public class FirmTests
    {

        [TestMethod]
        public void AddSubFirm_AddsSubFirm()
        {
            Firm firm = new Firm();
            SubFirm subFirm = new SubFirm();

            firm.AddSubFirm(subFirm);

            Assert.AreEqual(1, firm.SbFirms.Count);
            Assert.AreSame(subFirm, firm.SbFirms[0]);
        }


        [TestMethod]
        public void CannotAddTwoMainSubFirmsTest()
        {
            Firm firm = new Firm();

            SbFirmType mainType = new SbFirmType();
            mainType.IsMain = true;

            SubFirm firstMain = new SubFirm();
            firstMain.Type = mainType;

            SubFirm secondMain = new SubFirm();
            secondMain.Type = mainType;

            firm.AddSubFirm(firstMain);

            Assert.Throws<InvalidOperationException>(() => firm.AddSubFirm(secondMain));
        }

        [TestMethod]
        public void AddAndGetFieldTest()
        {
            Firm firm = new Firm();

            firm.AddField("ИНН");
            firm.SetField("ИНН", "345634567435");

            Assert.AreEqual("345634567435", firm.GetField("ИНН"));
        }

        [TestMethod]
        public void GetField_WhenFieldDoesNotExist()
        {
            Firm firm = new Firm();

            string result = firm.GetField("Несуществующее поле");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void SetField_WhenFieldDoesNotExist()
        {
            Firm firm = new Firm();

            Assert.Throws<InvalidOperationException>(
                () => firm.SetField("ИНН", "345634567435"));
        }

        [TestMethod]
        public void RenameFieldTest()
        {
            Firm firm = new Firm();

            firm.AddField("Старое имя");
            firm.SetField("Старое имя", "Значение");

            firm.RenameField("Старое имя", "Новое имя");

            Assert.AreEqual("Значение", firm.GetField("Новое имя"));
            Assert.IsNull(firm.GetField("Старое имя"));
        }

        [TestMethod]
        public void RenameField_WhenOldNameDoesNotExist()
        {
            Firm firm = new Firm();

            Assert.Throws<InvalidOperationException>(
                () => firm.RenameField("Старое имя", "Новое имя"));
        }


        [TestMethod]
        public void AddContToSbFirm_WhenSubFirmDontInFirm()
        {
            Firm firm = new Firm();
            SubFirm anotherSubFirm = new SubFirm();
            Contact contact = new Contact();

            Assert.Throws<InvalidOperationException>(
                () => firm.AddContToSbFirm(contact, anotherSubFirm));
        }

        [TestMethod]
        public void ExistContact_WhenContactExists()
        {
            Firm firm = new Firm();
            SubFirm subFirm = new SubFirm();
            Contact contact = new Contact();

            firm.AddSubFirm(subFirm);
            firm.AddContToSbFirm(contact, subFirm);

            Assert.IsTrue(firm.ExistContact(contact));
        }

        [TestMethod]
        public void ExistContact_WhenContactDoesNotExist()
        {
            Firm firm = new Firm();
            SubFirm subFirm = new SubFirm();

            firm.AddSubFirm(subFirm);

            Assert.IsFalse(firm.ExistContact(new Contact()));
        }

        [TestMethod]
        public void GetMainTest()
        {
            Firm firm = new Firm();

            SbFirmType mainType = new SbFirmType();
            mainType.IsMain = true;

            SubFirm mainSubFirm = new SubFirm();
            mainSubFirm.Type = mainType;

            firm.AddSubFirm(mainSubFirm);

            Assert.AreSame(mainSubFirm, firm.GetMain());
        }
    }
}