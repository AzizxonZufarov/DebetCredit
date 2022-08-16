using System.Linq;
using System.Collections.Generic;

namespace DebetCredit.Models
{
    public class BalanceTableModel
    {
        public int TotalSum => TotalIncomeSum + TotalOutcomeSum;
        public int TotalOutcomeSum => Rows.Where(row => row.Category == "Расход").Select(row => row.Summ).Sum();
        public int TotalIncomeSum => Rows.Where(row => row.Category == "Доход").Select(row => row.Summ).Sum();

        public List<BalanceModel> Rows { get; set; } = new List<BalanceModel>();
    }
}