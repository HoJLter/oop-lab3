using System;
using System.Collections.Generic;
using System.Text;

namespace oop_lab3_tests
{
    [TestClass]
    public class SubFirmsTests
    {
        private SbFirmType _mainType;
        private SbFirmType _otherType;
        private SubFirm _mainSubFirm;
        private SubFirm _otherSubfirm;

        [TestInitialize]
        public void Setup()
        {
            _mainType = TestSetup.sbFirmsTypeRepo[0];
            _otherType = TestSetup.sbFirmsTypeRepo[1];

            _mainSubFirm = new SubFirm(_mainType);
            _otherSubfirm = new SubFirm(_otherType);
        }

        [TestMethod]
        public void SubFirm_Constructor()
        {
            Assert.AreEqual(_mainSubFirm.Type, _mainType);
            Assert.AreEqual(_otherSubfirm.Type, _otherType);
        }

        [TestMethod]
        public void SubFirm_AddContact()
        {
            Contact contact = new Contact();

            _mainSubFirm.AddContact(contact);
            Assert.IsTrue(_mainSubFirm.ExistContact(contact));
        }

        [TestMethod]
        public void SubFirm_AddContactTwice()
        {

            void addContact()
            {
                Contact contact1 = new Contact();

                _mainSubFirm.AddContact(contact1);
                _mainSubFirm.AddContact(contact1);
            }

            Assert.Throws<Exception>(addContact);
        }


        [TestMethod]
        public void SubFirm_IsYourType()
        {
            Assert.IsTrue(_mainSubFirm.IsYourType(_mainType));
        }

        [TestMethod]
        public void SubFirm_ContactExistsAtPeriod()
        {
            Contact contact = new Contact();
            contact.BeginDt = DateTime.Parse("13.10.2026");
            contact.EndDt = DateTime.Parse("17.10.2026");

            _mainSubFirm.AddContact(contact);

            bool isExists1 = _mainSubFirm.ExistContactInPeriod(
                DateTime.Parse("10.10.2026"),
                DateTime.Parse("19.10.2026")
            );

            bool isExists2 = _mainSubFirm.ExistContactInPeriod(
                DateTime.Parse("3.10.2026"),
                DateTime.Parse("10.10.2026")
            );

            Assert.IsTrue(isExists1);
            Assert.IsFalse(isExists2);
        }

        [TestMethod]
        public void Subfirm_ContactExistsWithType()
        {
            Contact contact = new Contact();
            ContType type = new ContType();
            contact.Type = type;

            _mainSubFirm.AddContact(contact);

            bool isExists1 = _mainSubFirm.ExistContactWithType(type);

            Assert.IsTrue(isExists1);
        }

        [TestMethod]
        public void SbFirmTypeCol_Foreach()
        {
            var first = new SbFirmType
            {
                Name = "Основное",
                IsMain = true
            };
            var second = new SbFirmType
            {
                Name = "Филиал",
                IsMain = false
            };

            var collection = new SbFirmTypeCol();
            collection.Add(first);
            collection.Add(second);

            var actual = new List<SbFirmType>();

            foreach (var item in collection)
            {
                actual.Add(item);
            }

            Assert.AreEqual(2, actual.Count);
            Assert.AreSame(first, actual[0]);
            Assert.AreSame(second, actual[1]);
        }
    }
}