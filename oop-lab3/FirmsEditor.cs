public class FirmsEditor
{
    public static void SetFieldForFirms(List<Firm> firmsToEdit, string field, string newValue)
    {
        foreach (var firm in firmsToEdit)
        {
            firm.SetField(field, newValue);
        }
    }

    public static void AddContactForFirms(List<Firm> firmsToEdit, Contact contact)
    {
        foreach (var firm in firmsToEdit)
        {
            firm.AddCont(contact.Clone());
        }
    }

    public static void RenameField(List<Firm> firms, List<string> fields, string oldName, string newName)
    {
        int findedIndex = -1;
        for (int i = 0; i < fields.Count; i++)
        {
            if (fields[i] == oldName)
            {
                findedIndex = i;
                break;
            }
        }

        if (findedIndex == -1)
        {
            throw new Exception("Field not finded!!");
        }

        foreach (var firm in firms)
        {
            firm.RenameField(oldName, newName);
        }
    }
}

