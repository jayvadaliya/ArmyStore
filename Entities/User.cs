namespace ArmyStore.Entities
{
    public class User
    {
        public User()
        {
        }

        public User(string name, byte[] passwordHash, byte[] passwordSalt)
        {
            Name = name;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            UpdatedOn = System.DateTime.Now;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}