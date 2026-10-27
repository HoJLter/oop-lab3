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

    public static void Initialize(
        SbFirmType mainFirmType,
        List<string> fields)
    {
        if (mainFirmType == null)
        {
            throw new ArgumentNullException(nameof(mainFirmType));
        }

        if (!mainFirmType.IsMain)
        {
            throw new ArgumentException("тип должен обозначать основное подразделение.");
        }

        if (fields == null)
        {
            throw new ArgumentNullException(nameof(fields));
        }

        if (fields.Count != 5)
        {
            throw new ArgumentException("должно быть ровно пять полей.");
        }

        if (fields.Distinct().Count() != fields.Count)
        {
            throw new ArgumentException("названия полей не должны повторяться.");
        }

        if (_instance != null)
        {
            throw new InvalidOperationException("фабрика уже инициализирована.");
        }

        _instance = new FirmFactory(mainFirmType, fields);
    }

    private FirmFactory(SbFirmType mainFirmType, List<string> fields)
    {
        _mainSbFirmType = mainFirmType;
        _flds = new List<string>(fields);
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
