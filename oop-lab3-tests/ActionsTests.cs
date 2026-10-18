namespace oop_lab3_tests
{
    [TestClass]
    public class FirmsSearcherAndEditorTests
    {
        private Firm CreateFirm(string status)
        {
            SbFirmType mainType = TestSetup.sbFirmsTypeRepo[0];

            FirmFactory factory = FirmFactory.Instance;
            Firm firm = factory.Create();

            firm.AddField("Статус");
            firm.SetField("Статус", status);

            return firm;
        }

        [TestMethod]
        public void SearchFirmsByFieldsTest()
        {
            Firm firstFirm = CreateFirm("Новый");
            Firm secondFirm = CreateFirm("Постоянный");

            List<Firm> firms = new List<Firm>();
            firms.Add(firstFirm);
            firms.Add(secondFirm);

            FirmsSearcher searcher = new FirmsSearcher(firms);

            Dictionary<string, string> fields =
                new Dictionary<string, string>();

            fields.Add("Статус", "Новый");

            List<Firm> result = searcher.SearchFirmsByFields(fields);

            Assert.AreEqual(1, result.Count);
            Assert.AreSame(firstFirm, result[0]);
        }

        [TestMethod]
        public void SearchFirmsByContactTest()
        {
            Firm firstFirm = CreateFirm("Новый");
            Firm secondFirm = CreateFirm("Постоянный");

            Contact contact = new Contact();
            firstFirm.AddCont(contact);

            List<Firm> firms = new List<Firm>();
            firms.Add(firstFirm);
            firms.Add(secondFirm);

            FirmsSearcher searcher = new FirmsSearcher(firms);

            List<Firm> result = searcher.SearchFirmsByContact(contact, true);

            Assert.AreEqual(1, result.Count);
            Assert.AreSame(firstFirm, result[0]);
        }


        [TestMethod]
        public void SearchFirmsByContactTypeTest()
        {
            Firm firstFirm = CreateFirm("Новый");
            Firm secondFirm = CreateFirm("Постоянный");

            ContType callType = new ContType();
            callType.Name = "Звонок";

            Contact contact = new Contact();
            contact.Type = callType;

            firstFirm.AddCont(contact);

            List<Firm> firms = new List<Firm>();
            firms.Add(firstFirm);
            firms.Add(secondFirm);

            FirmsSearcher searcher = new FirmsSearcher(firms);

            List<Firm> result =
                searcher.SearchFirmsByContactType(callType, true);

            Assert.AreEqual(1, result.Count);
            Assert.AreSame(firstFirm, result[0]);
        }

        [TestMethod]
        public void SearchFirmsByContactPeriodTest()
        {
            Firm firstFirm = CreateFirm("Новый");
            Firm secondFirm = CreateFirm("Постоянный");

            Contact contact = new Contact();
            contact.BeginDt = new DateTime(2026, 5, 10);
            contact.EndDt = new DateTime(2026, 5, 15);

            firstFirm.AddCont(contact);
            FirmRepository repo = new FirmRepository();
            repo.addFirm(firstFirm);
            repo.addFirm(secondFirm);

            FirmsSearcher searcher = new FirmsSearcher(repo.Firms);

            List<Firm> result = searcher.SearchFirmsByContactPeriod(
                new DateTime(2026, 5, 12),
                new DateTime(2026, 5, 20),
                true);

            Assert.AreEqual(1, result.Count);
            Assert.AreSame(firstFirm, result[0]);
        }
    }
}
