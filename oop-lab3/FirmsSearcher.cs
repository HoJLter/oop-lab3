public class FirmsSearcher
{
    private List<Firm> _firms;

    public FirmsSearcher(List<Firm> list)
    {
        _firms = list;
    }

    public List<Firm> SearchFirmsByFields(Dictionary<string, string> fields)
    {
        List<Firm> findedFirms = new List<Firm>();
        foreach (var firm in _firms)
        {
            bool isNeedFirm = true;

            foreach (var field in fields)
            {
                if (firm.GetField(field.Key) != field.Value)
                {
                    isNeedFirm = false;
                    break;
                }
            }

            if (isNeedFirm) findedFirms.Add(firm);
        }
        return findedFirms;
    }

    public List<Firm> SearchFirmsByContact(Contact contact, bool hasContact)
    {
        List<Firm> findedFirms = new List<Firm>();
        foreach (var firm in _firms)
        {
            bool exists = firm.ExistContact(contact);
            if ((exists && hasContact) || 
               (!exists && !hasContact))
            {
                findedFirms.Add(firm);
            }
        }
        return findedFirms;
    }

    public List<Firm> SearchFirmsByContactType(ContType type, bool hasContact)
    {
        List<Firm> findedFirms = new List<Firm>();

        foreach (var firm in _firms)
        {
            bool exists = firm.ExistContactWithType(type);
            if (exists == hasContact)
            {
                findedFirms.Add(firm);
            }
        }

        return findedFirms;
    }

    public List<Firm> SearchFirmsByContactPeriod(DateTime begin, DateTime end, bool hasContact)
    {
        List<Firm> findedFirms = new List<Firm>();

        foreach (var firm in _firms)
        {
            bool exists = firm.ExistContactInPeriod(begin, end);
            if (exists == hasContact)
            {
                findedFirms.Add(firm);
            }
        }

        return findedFirms;
    }
}

