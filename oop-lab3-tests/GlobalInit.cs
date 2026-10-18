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
            sbFirmsTypeRepo.Add(new SbFirmType { IsMain = true, Name = "Основное"});

            FirmFactory.Initialize(sbFirmsTypeRepo[0]);
        }
    }

}
