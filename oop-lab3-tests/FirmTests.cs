namespace oop_lab3_tests
{
    [TestClass]
    public class FirmTests
    {
        [TestMethod]
        public void Firm_Factory_CreatesEmptyFields()
        {
            var factory = FirmFactory.Instance;
            var firm = factory.Create();

            foreach (string field in factory.GetFields())
            {
                Assert.AreEqual("", firm.GetField(field));
            }
        }


        [TestMethod]
        public void Firm_UpdateExistingFieldValue()
        {
            var factory = FirmFactory.Instance;
            var firm = factory.Create();

            foreach (var initFieldName in factory.GetFields())
            {
                string newValue = "НОВОЕ ЗНАЧЕНИЕ" + initFieldName;
                firm.SetField(initFieldName, newValue);
                Assert.AreEqual(newValue, firm.GetField(initFieldName));
                
            }
        }

        [TestMethod]
        public void Firm_AddContactToSubfirm()
        {
            var factory = FirmFactory.Instance;
            var firm = factory.Create();

            var otherSbFirm = new SubFirm(TestSetup.sbFirmsTypeRepo[1]) { Name = "Конторка" };
            firm.AddSubFirm(otherSbFirm);

            var contact = new Contact() { Descr="Контакт с 'ООО' Рога и копыта"};

            firm.AddContToSbFirm(contact, otherSbFirm);
            Assert.IsTrue(otherSbFirm.ExistContact(contact));
        }

        [TestMethod]
        public void Firm_AddContToMain()
        {
            var factory = FirmFactory.Instance;
            var firm = factory.Create();

            var mainSubFirm = firm.GetMain();

            var deliveryType = new ContType()
            {
                Name="Поставка",
                Note="Поставка огурцов"
            };

            var contact = new Contact()
            {
                Descr = "Контакт с 'ООО' Рога и копыта",
                Type = deliveryType
            };

            firm.AddContToMain(contact);

            Assert.IsTrue(mainSubFirm.ExistContact(contact));
            Assert.AreEqual(contact, firm.GetContact(contact));
            Assert.AreNotSame(contact, firm.GetContact(contact));
        }

        [TestMethod]
        public void AddContactTwice()
        {
            var factory = FirmFactory.Instance;
            var firm = factory.Create();

            var mainSubFirm = firm.GetMain();

            var deliveryType = new ContType()
            {
                Name = "Поставка",
                Note = "Поставка огурцов"
            };

            var buildingType = new ContType()
            {
                Name = "Стройка",
                Note = "Постройка здания на ул. Минина"
            };

            var contact1 = new Contact()
            {
                Descr = "Контакт с 'ООО' Рога и копыта",
                Type = buildingType
            };

            var contact2 = new Contact()
            {
                Descr = "Контакт с 'ООО' Строим дома",
                Type = deliveryType
            };


            firm.AddContToMain(contact1);
            firm.AddContToMain(contact2);

            Assert.AreEqual(contact1, firm.GetContact(buildingType)[0]);
            Assert.AreNotSame(contact1, firm.GetContact(buildingType)[0]);


            Assert.AreEqual(contact2, firm.GetContact(deliveryType)[0]);
            Assert.AreNotSame(contact2, firm.GetContact(deliveryType)[0]);
        }




        [TestMethod]
        public void Firm_RenameField()
        {
            var factory = FirmFactory.Instance;
            var factoryFields = factory.GetFields();
            var firm = factory.Create();

            foreach (var field in factoryFields)
            {
                firm.SetField(field, "ЗНАЧЕНИЕ" + field);
            }

            foreach (var field in factoryFields)
            {
                firm.RenameField(field, field + "_new");
            }

            foreach (var field in factoryFields)
            {
                string expectedValue = "ЗНАЧЕНИЕ" + field;
                string actualValue = firm.GetField(field + "_new");

                Assert.AreEqual(expectedValue, actualValue);
            }
        }

        [TestMethod]
        public void Firm_AddSubFirm()
        {
            var factory = FirmFactory.Instance;
            var firm = factory.Create();

            string sbFirmName = "Имя подразделения";

            var sbType = TestSetup.sbFirmsTypeRepo[1];
            var subFirm = new SubFirm(sbType)
            {
                Name=sbFirmName
            };

            int countBefore = firm.SbFirmCount;

            firm.AddSubFirm(subFirm);

            Assert.AreEqual(countBefore + 1, firm.SbFirmCount);
            Assert.IsNotNull(firm.GetByName(sbFirmName));
            Assert.IsNotNull(firm.GetByType(sbType));
        }



        [TestMethod]
        public void Firm_AddSecondMainSubFirm()
        {
            var factory = FirmFactory.Instance;
            var firm = factory.Create();

            var mainType = TestSetup.sbFirmsTypeRepo[0];
            var secondMain = new SubFirm(mainType);

            Assert.Throws<Exception>(() =>
            {
                firm.AddSubFirm(secondMain);
            });
        }
    }
}