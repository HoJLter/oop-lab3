using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;



public sealed class FirmFactory
{
    private readonly List<string> _flds;
    private readonly SbFirmType _mainSbFirmType;
    private const string NameMain = "Основное подразделение";

    private static FirmFactory _instance;

    public static FirmFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("Сначала вызовите FirmFactory.Initialize().");
            }

            return _instance;
        }
    }

    public static void Initialize(SbFirmType mainFirmType)
    {
        if (_instance != null)
        {
            throw new InvalidOperationException("фабрика уже инициализирована.");
        }

        _instance = new FirmFactory(mainFirmType);
    }


    private FirmFactory(SbFirmType mainFirmType)
    {
        if (mainFirmType == null)
        {
            throw new ArgumentNullException("mainFirmType не может быть нулл");
        }

        if (!mainFirmType.IsMain)
        {
            throw new ArgumentException("тип должен обозначать основное подразделение.");
        }

        _mainSbFirmType = mainFirmType;
        _flds = new List<string>()
        {
            "Поле 1", 
            "Поле 2",
            "Поле 3",
            "Поле 4",
            "Поле 5",
        };
    }

    public Firm Create()
    {
        Firm firm = new Firm(_flds);

        SubFirm mainSubfirm = new SubFirm(_mainSbFirmType)
        {
            Name = NameMain
        };

        firm.AddSubFirm(mainSubfirm);

        return firm;
    }

    public List<string> GetFields()
    {
        return new List<string>(_flds);
    }
}
