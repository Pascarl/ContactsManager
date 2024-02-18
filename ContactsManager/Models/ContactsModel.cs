using SQLite;

namespace ContactsManager.Models
{
    [Table("Contacts")]
    public class ContactsModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string Email { get; set; }
        [NotNull]
        public string Cell { get; set; }
        public string Cell2 { get; set; }
    }
}
