using System.Text;

namespace ArmyStore.Helper
{
    public static class SqlHelper
    {
        public static string GetQueryValues(IEnumerable<string> fields)
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            foreach(string columnName in fields) {
                columns.Append($"{columnName}, ");
                values.Append($"@{columnName.Remove(new char[] {'_'})}, ");
            }
            string insertQuery = $"({ columns.ToString().TrimEnd(',', ' ')}) VALUES ({ values.ToString().TrimEnd(',', ' ')}) ";

            return insertQuery;
        }

        public static string Remove(this string s, IEnumerable<char> chars)
        {
            return new string(s.Where(c => !chars.Contains(c)).ToArray());
        }
    }
}