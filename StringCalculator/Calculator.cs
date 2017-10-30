using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class Calculator
    {
        public string Add(string numbers)
        {
            var result = 0;

            if (numbers == "") return "";

            var delimetersList = new List<string>();

            var newLineIndex = numbers.IndexOf("\n", StringComparison.Ordinal);
            if (newLineIndex == 1)
            {
                delimetersList.Add(numbers[0].ToString());
                numbers = numbers.Substring(newLineIndex + 1);
            }
            else if (numbers.StartsWith("["))
            {
                var delimiters = numbers.Substring(0, newLineIndex);
                var regex = new Regex(@"\[(.*?)\]", RegexOptions.IgnoreCase);
                var matches = regex.Matches(delimiters);
                if (matches.Count > 0)
                {
                    delimetersList.AddRange(from Match match in matches select match.Value.Substring(1, match.Value.Length - 2));
                }
                numbers = numbers.Substring(newLineIndex + 1);
            }

            if (delimetersList.Count == 0)
            {
                delimetersList.Add(",");
            }

            delimetersList.Add("\n");

            var delimiterSplits = numbers.Split(delimetersList.ToArray(), StringSplitOptions.None);

            var negativeNumbers = "";

            foreach (var number in delimiterSplits)
            {
                if (int.TryParse(number, out var convertedNumber))
                {
                    if (convertedNumber < 0)
                    {
                        if (string.IsNullOrEmpty(negativeNumbers))
                        {
                            negativeNumbers += " " + convertedNumber;
                        }
                        else
                        {
                            negativeNumbers += ", " + convertedNumber;
                        }
                    }
                    if (convertedNumber <= 1000)
                    {
                        result += convertedNumber;
                    }
                }
                else
                {
                    throw new FormatException(nameof(numbers));
                }
            }

            if (!string.IsNullOrEmpty(negativeNumbers))
            {
                throw new FormatException("negatives not allowed:" + negativeNumbers);
            }

            return result.ToString();
        }
    }
}
