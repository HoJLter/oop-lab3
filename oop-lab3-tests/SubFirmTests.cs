using System;
using System.Collections.Generic;
using System.Text;

namespace oop_lab3_tests
{
    [TestClass]
    public class SubFirmsTests
    {
        [TestMethod]
        public void SubFirm_Constructor()
        {
            var mainType = TestSetup.sbFirmsTypeRepo[0];
            var otherType = TestSetup.sbFirmsTypeRepo[1];

            var mainSubFirm = new SubFirm(mainType);
            var otherSubfirm = new SubFirm(otherType);

            Assert.AreEqual(mainSubFirm.Type, mainType);
            Assert.AreEqual(otherSubfirm.Type, otherType);
        }

        [TestMethod]
        public void SubFirm_AddContact()
        {
            var mainSubFirm = new SubFirm(TestSetup.sbFirmsTypeRepo[0]);

            Contact contact = new Contact();

            mainSubFirm.AddContact(contact);
            Assert.IsTrue(mainSubFirm.ExistContact(contact));
        }

        [TestMethod]
        public void SubFirm_AddContactTwice()
        {
            var mainSubFirm = new SubFirm(TestSetup.sbFirmsTypeRepo[0]);

            void addContact()
            {
                Contact contact1 = new Contact();

                mainSubFirm.AddContact(contact1);
                mainSubFirm.AddContact(contact1);
            }

            Assert.Throws<Exception>(addContact);
        }

        [TestMethod]
        public void SubFirm_IsYourType()
        {
            var mainType = TestSetup.sbFirmsTypeRepo[0];
            var mainSubFirm = new SubFirm(mainType);

            Assert.IsTrue(mainSubFirm.IsYourType(mainType));
        }

        [TestMethod]
        public void SubFirm_ContactExistsAtPeriod()
        {
            var mainSubFirm = new SubFirm(TestSetup.sbFirmsTypeRepo[0]);

            Contact contact = new Contact();
            contact.BeginDt = DateTime.Parse("13.10.2026");
            contact.EndDt = DateTime.Parse("17.10.2026");

            mainSubFirm.AddContact(contact);

            bool isExists1 = mainSubFirm.ExistContactInPeriod(
                DateTime.Parse("10.10.2026"),
                DateTime.Parse("19.10.2026")
            );

            bool isExists2 = mainSubFirm.ExistContactInPeriod(
                DateTime.Parse("3.10.2026"),
                DateTime.Parse("10.10.2026")
            );

            Assert.IsTrue(isExists1);
            Assert.IsFalse(isExists2);
        }

        [TestMethod]
        public void Subfirm_ContactExistsWithType()
        {
            var mainSubFirm = new SubFirm(TestSetup.sbFirmsTypeRepo[0]);

            Contact contact = new Contact();
            ContType type = new ContType();
            contact.Type = type;

            mainSubFirm.AddContact(contact);

            bool isExists1 = mainSubFirm.ExistContactWithType(type);

            Assert.IsTrue(isExists1);
        }
    }
}