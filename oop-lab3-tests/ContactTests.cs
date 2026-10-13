namespace oop_lab3_tests
{
    [TestClass]
    public sealed class ContactTests
    {
        [TestMethod]
        public void ContactCloneTest()
        {
            Contact contact = new Contact();
            Contact clonnedContact = contact.Clone();

            Assert.IsTrue(contact == clonnedContact);
        }

        [TestMethod]
        public void ContactNotEqualTest()
        {
            Contact contact = new Contact();
            Contact anotherContact = new Contact();

            Assert.IsTrue(contact != anotherContact);
        }

        [TestMethod]
        public void ContTypeColAddTest()
        {
            ContType contact = new ContType();
            ContTypeCol collection = new ContTypeCol();

            collection.Add(contact);

            Assert.AreSame(contact, collection[0]);
        }

        [TestMethod]
        public void ContTypeColClearTest()
        {
            ContType contact = new ContType();
            ContTypeCol collection = new ContTypeCol();

            collection.Add(contact);
            Assert.AreSame(contact, collection[0]);
            collection.Clear();
            Assert.AreEqual(0, collection.Count);
        }
    }
}
