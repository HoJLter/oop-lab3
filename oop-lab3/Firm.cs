using System;
using System.Collections.Generic;

public class Firm
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
    public int SbFirmCount { get { return _sbFirms.Count; } }
    public string Street { get { return _street; } set { _street = value; } }
    public string Town { get { return _town; } set { _town = value; } }
    public string Web { get { return _web; } set { _web = value; } }

    public Firm(List<string> usrFields)
    {
        _sbFirms = new List<SubFirm>();
        _usrFields = new Dictionary<string, string>();

        foreach (string field in usrFields)
        {
            _usrFields.Add(field, "");
        }
    }

    public Firm(Dictionary<string, string> usrFields)
    {
        _sbFirms = new List<SubFirm>();
        _usrFields = new Dictionary<string, string>(usrFields);
    }

    public void AddSubFirm(SubFirm subFirm)
    {
        if (subFirm == null) throw new ArgumentNullException("Subfirm cannot be null!!");

        if (subFirm.Type.IsMain)
        {
            foreach (var sb in _sbFirms)
            {
                if (sb.Type.IsMain)
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
        if (!_usrFields.ContainsKey(oldName))
            throw new InvalidOperationException("Field not founded!!");

        if (oldName == newName)
            return;

        _usrFields.Add(newName, _usrFields[oldName]);
        _usrFields.Remove(oldName);
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

    public bool ExistContactWithType(ContType type)
    {
        foreach (var subFirm in _sbFirms)
        {
            if (subFirm.ExistContactWithType(type))
            {
                return true;
            }
        }

        return false;
    }

    public bool ExistContactInPeriod(DateTime begin, DateTime end)
    {
        foreach (var subFirm in _sbFirms)
        {
            if (subFirm.ExistContactInPeriod(begin, end))
            {
                return true;
            }
        }

        return false;
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
            return null;

        return _usrFields[field];
    }

    public void SetField(string field, string value)
    {
        if (!_usrFields.ContainsKey(field))
            throw new Exception("Field not finded!");

        _usrFields[field] = value;
    }

    public SubFirm GetMain()
    {
        foreach (SubFirm subFirm in _sbFirms)
        {
            if (subFirm.Type.IsMain)
                return subFirm;
        }

        throw new Exception("Main subfirm not finded!");
    }
}
