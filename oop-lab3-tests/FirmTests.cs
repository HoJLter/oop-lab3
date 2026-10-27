namespace oop_lab3_tests
{
    [TestClass]
    public class FirmTests
    {
        private Firm _firm;
        private Dictionary<string, string> _initFields;
        private SubFirm _mainSubFirm;
        private SubFirm _otherSubFirm;

        [TestInitialize]
        public void Setup()
        {
            _initFields = new Dictionary<string, string> {
                { "п1", "зн1" },
                { "п2", "зн2" },
                { "п3", "зн3" },
                { "п4", "зн4" },
                { "п5", "зн5" }
            };

            var mainType = new SbFirmType("Основное", true);
            var otherType = new SbFirmType("Обычное", false);


            _mainSubFirm = new SubFirm(mainType)
            {
                Name = "Основное подразделение"
            };

            _otherSubFirm = new SubFirm(otherType)
            {
                Name = "Дополнительное подразделение"
            };

            _firm = new Firm(_initFields);
            _firm.AddSubFirm(_mainSubFirm);
            _firm.AddSubFirm(_otherSubFirm);
        }

        [TestMethod]
        public void Firm_Constructor_CreatesFields()
        {
            foreach (var initField in _initFields)
            {
                Assert.AreSame(initField.Value, _firm.GetField(initField.Key));
            }
        }


        [TestMethod]
        public void Firm_UpdateExistingFieldValue()
        {
            foreach (var initField in _initFields)
            {
                string newValue = initField.Value + "НОВОЕ ЗНАЧЕНИЕ";
                _firm.SetField(initField.Key, newValue);
                Assert.AreEqual(newValue, _firm.GetField(initField.Key));
            }
        }


        [TestMethod]
        public void Firm_GetMain()
        {
            Assert.AreEqual(_mainSubFirm, _firm.GetMain());
        }

        [TestMethod]
        public void Firm_GetMain_ThrowsIfNoMain()
        {
            var firm = new Firm(_initFields);
            firm.AddSubFirm(_otherSubFirm);

            Assert.Throws<Exception>(() => firm.GetMain());
        }

        [TestMethod]
        public void Firm_AddContactToSubfirm()
        {
            Contact contact = new Contact();
            _firm.AddContToSbFirm(contact, _otherSubFirm);
            Assert.IsTrue(_otherSubFirm.ExistContact(contact));
            Assert.IsFalse(_mainSubFirm.ExistContact(contact));
        }

        [TestMethod]
        public void Firm_RenameField()
        {
            foreach (var initField in _initFields)
            {
                string newFieldName = initField.Key + "_new";
                _firm.RenameField(initField.Key, newFieldName);
                Assert.AreEqual(_firm.GetField(newFieldName), _initFields[initField.Key]);

                _firm.RenameField(newFieldName, initField.Key);
                Assert.AreEqual(_firm.GetField(initField.Key), _initFields[initField.Key]);
            }
        }


        [TestMethod]
        public void Firm_AddSecondMainSubFirm()
        {
            var mainType = new SbFirmType("Основное", true);

            var secondMain = new SubFirm(mainType);

            Assert.Throws<Exception>(() =>
            {
                _firm.AddSubFirm(secondMain);
            });

            Assert.AreSame(_mainSubFirm, _firm.GetMain());
        }

    }
}