using System;
using System.Collections.Generic;
using System.Text;

namespace oop_lab3_tests
{
    [TestClass]
    public class SubFirmsTests
    {
        [TestMethod]
        public void SbFirmTypeColAddTest()
        {
            SbFirmType type = new SbFirmType();
            SbFirmTypeCol collection = new SbFirmTypeCol();

            collection.Add(type);

            Assert.AreSame(type, collection[0]);
        }

        [TestMethod]
        public void SbFirmTypeColForeachTest()
        {
            SbFirmType first = new SbFirmType();
            SbFirmType second = new SbFirmType();
            SbFirmTypeCol collection = new SbFirmTypeCol();

            collection.Add(first);
            collection.Add(second);

            int count = 0;

            foreach (SbFirmType type in collection)
            {
                count++;
            }

            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void SubFirmAddContactTest()
        {
            SubFirm subFirm = new SubFirm();
            Contact contact = new Contact();

            subFirm.AddContact(contact);

            Assert.AreEqual(1, subFirm.CountCont);
            Assert.IsTrue(subFirm.ExistContact(contact));
        }

        [TestMethod]
        public void SubFirmAddSameContactTwiceTest()
        {
            SubFirm subFirm = new SubFirm();
            Contact contact = new Contact();

            subFirm.AddContact(contact);

            Assert.Throws<Exception>(() => subFirm.AddContact(contact));
        }

        [TestMethod]
        public void SubFirmIsYourTypeTest()
        {
            SbFirmType salesType = new SbFirmType();
            SbFirmType otherType = new SbFirmType();

            SubFirm subFirm = new SubFirm();
            subFirm.Type = salesType;

            Assert.IsTrue(subFirm.IsYourType(salesType));
            Assert.IsFalse(subFirm.IsYourType(otherType));
        }
    }
}