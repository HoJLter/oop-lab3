using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;



public sealed class FirmFactory
{
    private readonly List<string> _flds;
    private readonly SbFirmType _mainSbFirmType;

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
        if (mainFirmType == null)
        {
            throw new ArgumentNullException(nameof(mainFirmType));
        }

        if (!mainFirmType.IsMain)
        {
            throw new ArgumentException("Тип должен обозначать основное подразделение.");
        }

        if (_instance != null)
        {
            throw new InvalidOperationException("Фабрика уже инициализирована.");
        }

        _instance = new FirmFactory(mainFirmType);
    }

    private FirmFactory(SbFirmType mainFirmType)
    {
        _mainSbFirmType = mainFirmType;

        _flds = new List<string>
        {
            "Поле 1",
            "Поле 2",
            "Поле 3",
            "Поле 4",
            "Поле 5"
        };
    }

    public Firm Create()
    {
        Firm firm = new Firm(_flds);

        SubFirm mainSubfirm = new SubFirm(_mainSbFirmType);

        firm.AddSubFirm(mainSubfirm);

        return firm;
    }

    public List<string> GetFields()
    {
        return new List<string>(_flds);
    }
}
