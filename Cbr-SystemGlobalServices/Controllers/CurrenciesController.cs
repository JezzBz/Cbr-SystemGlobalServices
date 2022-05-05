using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cbr_SystemGlobalServices.Managers;
using Cbr_SystemGlobalServices.ServerModels;
using Microsoft.AspNetCore.Mvc;



namespace Cbr_SystemGlobalServices.Controllers
{

    // The controller that is responsible for processing requests at the exchange currency
    public class CurrenciesController : Controller
    {
        //We create an http client to request from the central bank
        private readonly HttpClient Client = new() { };
        //cbr url
        private readonly Uri RequestUri = new("https://www.cbr-xml-daily.ru/daily_json.js");
        private readonly HttpClientManager HttpManager = new();

        /// <summary>
        /// Method that allows you to get all the exchange currencies or use pagination
        /// </summary>
        /// <param name="pageNumber">Pagination page number</param>
        /// <param name="pageSize">Pagination page size</param>
        /// <returns>Enumeration on currency(Json)</returns>
        [HttpGet]
        [Route("/currencies")]
        public async Task<IActionResult> Сurrencies(int pageNumber = 1, int pageSize=-1)
        {
            var CurrensiesJson = "";
            IEnumerable<Currency> Currencies;
            try
            {
                //Send request
                CurrensiesJson = await HttpManager.HttpGetAsync(Client,RequestUri);
                //Parse json to object list
                Currencies=ParserManager.ParseCurrencies(CurrensiesJson);
            }
            catch (Exception ex)
            {
                //notify about errors
                return BadRequest("Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message);
            }



            //Checking for pagination
            if (pageSize==-1)
            {
                return Ok(Currencies);
            }
            return Ok(Currencies.Skip((pageNumber - 1) * pageSize).Take(pageSize));


        }
        /// <summary>
        /// Method that allows you to get currency by ID
        /// </summary>
        /// <returns>Currency Object(Json)</returns>
        [HttpGet]
        [Route("/currency")]
        public async Task<IActionResult> Currency(string Id)
        {
            var CurrensiesJson = "";
            Currency? currency;
            try
            {
                //Send request
                CurrensiesJson = await HttpManager.HttpGetAsync(Client, RequestUri);
                //Parse json to the object
                currency = ParserManager.ParseCurrencyById(Id, CurrensiesJson);
                if (currency!=null)
                {
                    return Ok(currency);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message);
            }
            return BadRequest("Currency not found!");
        }


    }
    
    
}

