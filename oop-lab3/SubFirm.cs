using System;
using System.Collections.Generic;
using System.Collections;


public class SbFirmType
{
    private bool _isMain;
    private string _name;

    public bool IsMain { get { return _isMain; } set { _isMain = value; } }
    public string Name { get { return _name; } set { _name = value; } }
}

public class SbFirmTypeCol : IEnumerable<SbFirmType>
{
    private List<SbFirmType> _list;


    public int Count { get { return _list.Count; } }

    public SbFirmType this[int i]
    {
        get { return _list[i]; }
    }

    public SbFirmTypeCol()
    {
        _list = new List<SbFirmType>();
    }

    public void Add(SbFirmType type)
    {
        _list.Add(type);
    }

    public void Clear()
    {
        _list.Clear();
    }

    public IEnumerator<SbFirmType> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


public class SubFirm
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
    public bool IsMain { get { return _type.IsMain; } }
    public string Name { get { return _name; } set { _name = value; } }
    public string OfcBossName { get { return _ofcBossName; } set { _ofcBossName = value; } }
    public SbFirmType Type { get { return _type; } }
    public string Tel { get { return _tel; } set { _tel = value; } }


    public bool ExistContactWithType(ContType type)
    {
        foreach (var contact in _conts)
        {
            if (contact.Type == type)
            {
                return true;
            }
        }
        return false;
    }

    public bool ExistContactInPeriod(DateTime begin, DateTime end)
    {
        foreach (var contact in _conts)
        {
            if (contact.BeginDt <= end && contact.EndDt >= begin)
            {
                return true;
            }
        }
        return false;
    }

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
        if (contact is null)
        {
            throw new Exception("Contact cannot be null!");
        }

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

    public SubFirm(SbFirmType type)
    {
        _type = type;
        _conts = new List<Contact>();
    }
}
