namespace oop_lab3_tests
{
    [TestClass]
    public class FirmsSearcherAndEditorTests
    {
        private Firm CreateFirm(string status)
        {
            Firm firm = FirmFactory.Instance.Create();

            firm.RenameField("Поле 1", "Статус");
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

            Dictionary<string, string> fields = new Dictionary<string, string>();

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
            List<Firm> firmsList = new List<Firm>();
            firmsList.Add(firstFirm);
            firmsList.Add(secondFirm);

            FirmsSearcher searcher = new FirmsSearcher(firmsList);

            List<Firm> result = searcher.SearchFirmsByContactPeriod(
                new DateTime(2026, 5, 12),
                new DateTime(2026, 5, 20),
                true);

            Assert.AreEqual(1, result.Count);
            Assert.AreSame(firstFirm, result[0]);
        }

        [TestMethod]
        public void SetFieldForFirmsTest()
        {
            Firm first = CreateFirm("Новый");
            Firm second = CreateFirm("Постоянный");

            List<Firm> firmsToEdit = new List<Firm> { first, second };

            FirmsEditor.SetFieldForFirms(firmsToEdit, "Статус", "Обновлён");

            Assert.AreEqual("Обновлён", first.GetField("Статус"));
            Assert.AreEqual("Обновлён", second.GetField("Статус"));
        }

        [TestMethod]
        public void AddContactForFirmsTest()
        {
            Firm first = CreateFirm("Новый");
            Firm second = CreateFirm("Постоянный");

            FirmsEditor.AddContactForFirms(
                new List<Firm> { first, second },
                new Contact());

            Assert.AreEqual(1, first.GetMain().CountCont);
            Assert.AreEqual(1, second.GetMain().CountCont);
        }

        [TestMethod]
        public void RenameFieldTest()
        {
            Firm first = CreateFirm("Новый");
            Firm second = CreateFirm("Постоянный");

            var firms = new List<Firm> { first, second };

            FirmsEditor.RenameField(firms, "Статус", "Категория");

            Assert.IsNull(first.GetField("Статус"));
            Assert.IsNull(second.GetField("Статус"));

            Assert.AreEqual("Новый", first.GetField("Категория"));
            Assert.AreEqual("Постоянный", second.GetField("Категория"));
        }

    }
}
