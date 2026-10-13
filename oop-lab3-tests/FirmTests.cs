using System;
using System.Collections.Generic;
using System.Text;

namespace oop_lab3_tests
{
    [TestClass]
    public class FirmTests
    {
        [TestMethod]
        public void AddSubFirmTest()
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
            firm.SetField("ИНН", "1234567890");

            Assert.AreEqual("1234567890", firm.GetField("ИНН"));
        }

        [TestMethod]
        public void RenameFieldTest()
        {
            Firm firm = new Firm();

            firm.AddField("Старое имя");
            firm.SetField("Старое имя", "Значение");

            firm.RenameField("Старое имя", "Новое имя");

            Assert.AreEqual("Значение", firm.GetField("Новое имя"));
        }

        [TestMethod]
        public void AddContToMainSubFirmTest()
        {
            Firm firm = new Firm();

            SbFirmType mainType = new SbFirmType();
            mainType.IsMain = true;

            SubFirm mainSubFirm = new SubFirm();
            mainSubFirm.Type = mainType;

            firm.AddSubFirm(mainSubFirm);

            Contact contact = new Contact();

            firm.AddCont(contact);

            Assert.AreEqual(1, mainSubFirm.CountCont);
            Assert.IsTrue(firm.ExistContact(contact));
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
