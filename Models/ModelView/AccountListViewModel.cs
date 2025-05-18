using TrueMatch.Models.Data;

namespace TrueMatch.Models.ViewModels

{
    public class AccountListViewModel
    {
        public IEnumerable<Account> Accounts { get; set; }
        public string CurrentFilter { get; set; }
    }
}
