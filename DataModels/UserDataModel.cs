namespace ArmyStore.DataModels
{
    public class UserDataModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}