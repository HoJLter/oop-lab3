using System;
using System.Collections.Generic;
using System.Xml.Linq;

public class FirmFactory
{
    private List<string> _flds;
    private string _nameMain;
    private List<Firm> _firms;

    public List<Firm> Firms { get { return _firms; } }

    private static FirmFactory _instance = null;
    public static FirmFactory Instance {
        get {
            if (_instance == null)
            {
                _instance = new FirmFactory();
            }
            return _instance;
        }
    }

    private FirmFactory() {
        _flds = new List<string>();
        _firms = new List<Firm>();
        _nameMain = "Основное подразделение";

        _flds.Add("Поле 1");
        _flds.Add("Поле 2");
        _flds.Add("Поле 3");
        _flds.Add("Поле 4");
        _flds.Add("Поле 5");
    }

    public Firm Create()
    {
        Firm firm = new Firm();

        foreach (var name in _flds)
        {
            firm.UsrFields[name] = "";
        }

        SubFirm mainSubfirm = new SubFirm();
        mainSubfirm.Name = _nameMain;
        mainSubfirm.Type = new SbFirmType
        {
            Name = _nameMain,
            IsMain = true,
        };
        firm.AddSubFirm(mainSubfirm);

        _firms.Add(firm);
        return firm;
    }

    public void RenameField(string oldName, string newName)
    {
        int findedIndex = -1;
        for (int i = 0; i < _flds.Count; i++)
        {
            if (_flds[i] == oldName) findedIndex = i;
        }

        if (findedIndex == -1)
        {
            throw new Exception("Field not finded!!");
        }

        _flds[findedIndex] = newName;
        foreach (var firm in _firms)
        {
            firm.RenameField(oldName, newName);
        }
    }
}
