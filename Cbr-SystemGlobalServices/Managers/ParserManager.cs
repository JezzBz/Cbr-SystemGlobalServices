using System;
using System.Text.Json;
using System.Text.RegularExpressions;
using Cbr_SystemGlobalServices.ServerModels;

namespace Cbr_SystemGlobalServices.Managers
{
	public class ParserManager
	{
		private static readonly Regex regex = new("(\"[A-Z]{3}\":\\s\\{(.*?|\\s)* \\})", RegexOptions.Compiled);
		/// <summary>
        /// Method to parse Json by regex to objects
        /// </summary>
        /// <param name="json">Json text</param>
        /// <returns>Enumeration of currency objects</returns>
		public static IEnumerable<Currency> ParseCurrencies(string json)
        {
			var Matches = regex.Matches(json);
			IEnumerable<string> RatesString = Matches.Select(x => Regex.Replace(x.Value,"\"[A-Z]{3}\":\\s",""));
			List<Currency> Valutes = new();

			foreach (var item in RatesString)
			{
				Currency? currency = JsonSerializer.Deserialize<Currency>(item);

				if (currency != null)
				{
					Valutes.Add(currency);
				}

			}
			return Valutes;
            
        }

		/// <summary>
        /// Method to parse Json and find targer currency
        /// </summary>
        /// <param name="id">Target id</param>
        /// <param name="json">Json text</param>
        /// <returns>Currency object</returns>
		public static Currency? ParseCurrencyById(string id,string json)
        {
			var Matches = regex.Matches(json);
			IEnumerable<string> RatesString = Matches.Select(x => Regex.Replace(x.Value, "\"[A-Z]{3}\":\\s", ""));
			string currencyString=RatesString.First(x=>x.Contains($"\"ID\": \"{id}\","));
			return JsonSerializer.Deserialize<Currency>(currencyString);
		}
	}
}

