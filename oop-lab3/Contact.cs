using System;
using System.Collections.Generic;


using System;

public class ContType
{
    private string _name;
    private string _note;

    public string Name { get { return _name; } set { _name = value; } }
    public string Note { get { return _note; } set { _note = value; } }
}

public class ContTypeCol
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

    public ContTypeCol()
    {
        _list = new List<ContType>();
    }
}


public class Contact
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

    public override bool Equals(object? obj)
    {
        if (obj is Contact)
        {
            Contact other = (Contact)obj;
            return this == other;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_beginDt, _endDt, _descr, _dataInfo, _type);
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
