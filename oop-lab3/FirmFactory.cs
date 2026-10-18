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
            throw new ArgumentException("Тип должен обозначать основное подразделение.", nameof(mainFirmType));
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
        Firm firm = new Firm();

        foreach (string name in _flds)
        {
            firm.UsrFields[name] = "";
        }

        SubFirm mainSubfirm = new SubFirm
        {
            Type = _mainSbFirmType
        };

        firm.AddSubFirm(mainSubfirm);

        return firm;
    }

    public List<string> GetFields()
    {
        return new List<string>(_flds);
    }
}

public class FirmRepository
{
    private List<Firm> _firms;
    public List<Firm> Firms { get { return _firms; } }
    public int Count { get { return _firms.Count; } }

    public FirmRepository()
    {
        _firms = new List<Firm>();
    }

    public void addFirm(Firm newFirm)
    {
        _firms.Add(newFirm);
    }
}
