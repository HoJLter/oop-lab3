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

    public static void RenameField(
        List<Firm> firms,
        string oldName,
        string newName)
    {
        foreach (var firm in firms)
        {
            firm.RenameField(oldName, newName);
        }
    }
}

