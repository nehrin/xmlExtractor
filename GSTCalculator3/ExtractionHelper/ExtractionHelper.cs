using GSTCalculator.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GSTCalculator.ExtractionHelper
{
    public class ExtractionHelper
    {
        private List<IItem> _items;
        private string Text;
        private bool unmatchedTag;

        public ExtractionHelper(string text)
        {
            Text = text;
            _items = new List<IItem>();
        }

        public List<IItem> extractAsync()
        {
            bool totalFound = false;
            Regex rg = new Regex("<[^<>]+>");
            Match match = rg.Match(Text);
            MatchCollection matches = rg.Matches(Text);

            string node_str = match.ToString();
            string match_expense = node_str.Substring(1, node_str.Length - 2);
            double total = 0.0;
            string cost_centre = "";
            string payment_method = "UNKNOWN";
            string vendor = "";
            string description = "";
            string date_str = "";
            DateTime date = new DateTime();
            for (int i = 1; i < matches.Count; i++)
            {
                string xml = matches[i].ToString();
                if (xml.Contains("total"))
                {
                    totalFound = true;
                    total = Convert.ToDouble(extractValue("total>"));
                }
                if (xml.Contains("cost_centre"))
                    cost_centre = extractValue("cost_centre>");
                if (xml.Contains("<payment_method>"))
                    payment_method = extractValue("payment_method>");
                if (xml.Contains("vendor>"))
                    vendor = extractValue("vendor>");
                if (xml.Contains("<description>"))
                    description = extractValue("description>");
                if (xml.Contains("date>"))
                {
                    date_str = extractValue("date>");
                    Regex reg_space = new Regex(" ");
                    MatchCollection match_space = reg_space.Matches(date_str);

                    string day = date_str.Substring(0, match_space[0].Index);
                    string dateNumber = date_str.Substring(match_space[0].Index, match_space[1].Index - match_space[0].Index).Trim();
                    string month = date_str.Substring(match_space[1].Index, match_space[2].Index - match_space[1].Index).Trim();
                    string year = date_str.Substring(date_str.Length - 4).Trim();
                    date = Convert.ToDateTime(dateNumber + "-" + month + "-" + year);
                }
            }
            if (totalFound && !unmatchedTag)
            {
                if (match_expense.Trim().Equals("expense"))
                {
                    ExpenseItem expense = new ExpenseItem
                    {
                        CostCentre = cost_centre,
                        Total = total,
                        PaymentMethod = payment_method
                    };

                    _items.Add(expense);
                }

                if (Text.Contains("reservation"))
                {
                    ReservationItem reservation = new ReservationItem
                    {
                        Vendor = vendor,
                        Description = description,
                        ReservationTime = date
                    };

                    _items.Add(reservation);
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid input in the message.");
            }
            return _items;
        }

        private string extractValue(string match_str)
        {
            Regex rg_end = new Regex(match_str);
            MatchCollection matches_end = rg_end.Matches(Text);
            string xml;
            if (matches_end.Count == 2)
            {
                int match_end = matches_end[1].Index;
                int match_start = matches_end[0].Index;
                xml = Text.Substring(match_start + match_str.Length, match_end - match_start - match_str.Length - 2);

                Regex rg_return = new Regex("\r\n");
                xml = rg_return.Replace(xml, string.Empty);
            }
            else
            {
                xml = "NOT FOUND";
                unmatchedTag = true;
            }
            return xml;
        }
    }
}
