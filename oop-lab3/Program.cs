


using System.Collections;

class ContType
{
    private string _name;
    private string _note;

    public string Name { get { return _name; } set { _name = value; } }
    public string Note { get { return _note; } set { _note = value; } }
}

class ContTypeCol
{
    private List<ContType> _list;

    public int Count
    {
        get { return _list.Count; }
    }

    public ContType this[int i]
    {
        get { return _list[i]; }
    }


    public void Add(ContType contType)
    {
        _list.Add(contType);
    }
    
    public void Clear()
    {
        _list.RemoveAll(_ => true);
    }

    public ContTypeCol(){
        _list = new List<ContType>();
    }
}

class Contact
{
    private DateTime _beginDt;
    private DateTime _endDt;
    private string _descr;
    private string _dataInfo;
    private ContType _type;

    public DateTime BeginDt { get { return _beginDt; } set { _beginDt = value; } }
    public DateTime EndDt { get { return _endDt; } set { _endDt = value; } }
    public string Descr { get { return _descr; } set { _descr = value; } }
    public string DataInfo { get { return _dataInfo; } set { _dataInfo = value; } }
    public ContType Type { get { return _type; } set { _type = value; } }


    public static bool operator ==(Contact a, Contact b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;

        return a._beginDt == b._beginDt
            && a._endDt == b._endDt
            && a._descr == b._descr
            && a._dataInfo == b._dataInfo
            && Equals(a._type, b._type);
    }

    public static bool operator !=(Contact a, Contact b)
    {
        return !(a == b);
    }

    public Contact()
    {
        _beginDt = DateTime.Now;
        _endDt = DateTime.Now;
        _descr = string.Empty;
        _dataInfo = string.Empty;
    }

    public Contact Clone()
    {
        Contact clonned = new Contact();

        clonned.BeginDt = _beginDt;
        clonned.EndDt = _endDt;
        clonned.Descr = _descr;
        clonned.DataInfo = _dataInfo;
        clonned.Type = _type;

        return clonned;
    }
}

class SbFirmType
{
    private bool _isMain;
    private string _name;

    public bool IsMain{  get { return _isMain; } set {_isMain = value; } }
    public string Name { get { return _name; } set { _name = value; } }
}

class SbFirmTypeCol: IEnumerable<SbFirmType>, IEnumerator<SbFirmType>
{
    private List<SbFirmType> _list;

    private int curIndex = -1;
    public SbFirmType Current { get { return _list[curIndex]; } }
    object IEnumerator.Current { get { return Current; } }

    public bool MoveNext()
    {
        curIndex++;
        return curIndex < _list.Count;
    }

    public void Reset()
    {
        curIndex = -1;
    }

    public void Dispose() { }


    public IEnumerator<SbFirmType> GetEnumerator()
    {
        Reset();
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    public SbFirmTypeCol()
    {
        _list = new List<SbFirmType>();
    }

    public void Add(SbFirmType t)
    {
        _list.Add(t);
    }

    public SbFirmType this[int i]
    {
        get { return _list[i]; }
    }
}

class SubFirm
{
    private string _bossName;
    private List<Contact> _conts;
    private string _email;
    private string _name;
    private string _ofcBossName;
    private string _tel;
    private SbFirmType _type;

    public string BossName { get { return _bossName; } set { _bossName = value; } }
    public int CountCont { get { return _conts.Count; } }
    public string Email { get { return _email; } set { _email = value; } }
    public bool IsMain { get { return Type.IsMain; } }
    public string Name { get { return _name; } set { _name = value; } }
    public string OfcBossName { get { return _ofcBossName; } set { _ofcBossName = value; } }
    public SbFirmType Type { get { return _type; } set { _type = value; } }
    public string Tel { get { return _tel; } set { _tel = value; } }


    public bool ExistContact(Contact contact)
    {
        foreach (var cont in _conts)
        {
            if (cont == contact) return true;
        }
        return false;
    }

    public void AddContact(Contact contact)
    {
        if (!ExistContact(contact))
        {
            _conts.Add(contact);
        }
        else
        {
            throw new Exception("Thi scontact is already addedd!!!");
        }
    }

    public bool IsYourType(SbFirmType type)
    {
        return _type == type;
    }

    public SubFirm()
    {
        _conts = new List<Contact>();
    }
}


class Firm
{
    private string _country;
    private DateTime _dateIn;
    private string _email;
    private string _name;
    private string _postInx;
    private string _region;
    private List<SubFirm> _sbFirms;
    private string _street;
    private string _town;
    private Dictionary<string, string> _usrFields;
    private string _web;

    public string Country { get { return _country; } set { _country = value; } }
    public DateTime DateIn { get { return _dateIn; } set { _dateIn = value; } }
    public string Email { get { return _email; } set { _email = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public string PostInx { get { return _postInx; } set { _postInx = value; } }
    public string Region { get { return _region; } set { _region = value; } }
    public List<SubFirm> SbFirms { get { return _sbFirms; } set { _sbFirms = value; } }
    public string Street { get { return _street; } set { _street = value; } }
    public string Town { get { return _town; } set { _town = value; } }
    public Dictionary<string, string> UsrFields { get { return _usrFields; } set { _usrFields = value; } }
    public string Web { get { return _web; } set { _web = value; } }

    public Firm()
    {
        _sbFirms = new List<SubFirm>();
        _usrFields = new Dictionary<string, string>();
    }

    public void AddSubFirm(SubFirm subFirm)
    {
        if (subFirm == null) throw new ArgumentNullException("Subfirm cannot be null!!");

        if (subFirm.Type != null && subFirm.Type.IsMain)
        {
            foreach (var sf in _sbFirms)
            {
                if (sf.Type != null && sf.Type.IsMain)
                    throw new InvalidOperationException("Main subfirm already created!!!!!!");
            }
        }
        _sbFirms.Add(subFirm);
    }

    public void AddField(string field)
    {
        _usrFields.Add(field, "");
    }

    public void RenameField(string oldName, string newName)
    {
        bool flag = false;
        foreach (var name in _usrFields.Keys)
        {
            if (name == oldName)
            {
                flag = true;
                break;
            }
        }

        if (flag)
        {
            _usrFields.Add(newName, _usrFields[oldName]);
            _usrFields.Remove(oldName);
        }
        else
        {
            throw new InvalidOperationException("Name not found!!!!!!!");
        }

    }

    public void AddCont(Contact cont)
    {
        SubFirm main = GetMain();
        main.AddContact(cont);
    }

    public void AddContToSbFirm(Contact cont, SubFirm subFirm)
    {
        if (!_sbFirms.Contains(subFirm))
            throw new InvalidOperationException("Subfirm not found!");

        subFirm.AddContact(cont);
    }

    public bool ExistContact(Contact cont)
    {
        foreach (SubFirm subFirm in _sbFirms)
        {
            if (subFirm.ExistContact(cont))
            {
                return true;
            }
        }

        return false;
    }

    public string GetField(string field)
    {
        if (!_usrFields.ContainsKey(field))
            throw new InvalidOperationException("Field not finded!");

        return _usrFields[field];
    }

    public void SetField(string field, string value)
    {
        if (!_usrFields.ContainsKey(field))
            throw new InvalidOperationException("Field not finded!");

        _usrFields[field] = value;
    }

    public SubFirm GetMain()
    {
        foreach (SubFirm subFirm in _sbFirms)
        {
            if (subFirm.Type != null && subFirm.Type.IsMain)
                return subFirm;
        }

        throw new InvalidOperationException("Main subfirm not finded!");
    }


}

class FirmFactory
{
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
    private FirmFactory() { }

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

        return firm;
    }

    private List<string> _flds;
    private string _nameMain;
}