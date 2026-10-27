using System;
using System.Collections.Generic;
using System.Text;

namespace oop_lab3_tests
{
    [TestClass]
    public class TestSetup
    {
        public static SbFirmTypeCol sbFirmsTypeRepo;
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            sbFirmsTypeRepo = new SbFirmTypeCol();
            sbFirmsTypeRepo.Add(new SbFirmType("Основное", true));
            sbFirmsTypeRepo.Add(new SbFirmType("Первое подразделение", false));

            var fields = new List<string>
            {
                "Поле 1",
                "Поле 2",
                "Поле 3",
                "Поле 4",
                "Поле 5"
            };

            FirmFactory.Initialize(sbFirmsTypeRepo[0], fields);
        }
    }
}
