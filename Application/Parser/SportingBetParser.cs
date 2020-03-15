using Domain.Model;
using Domain.Parser;
using HtmlAgilityPack;
using log4net;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Parser
{
    public class SportingBetParser : ISportingBetParser
    {
        public const string marketGroup = "option-panel";
        private List<string> Urls = new List<string>() { "https://sports.sportingbet.com/pt-br/sports/eventos/atletico-acreano-galvez-ec-9846281" };
        private List<HtmlDocument> htmlDocuments = new List<HtmlDocument>();
        private int ThreadSleepTime = 3000;
        private ILog _log;

        public SportingBetParser()
        {
            _log = LogManager.GetLogger(typeof(SportingBetParser));
        }
        public IDictionary<string, IEnumerable<OddModel>> GetMatchOdds()
        {
            return new Dictionary<string, IEnumerable<OddModel>>();
        }
        public IEnumerable<OddModel> GetDoubleChance()
        {
            return new List<OddModel>();
        }

        private IEnumerable<HtmlDocument> GetHtmlDocuments(IEnumerable<string> Urls)
        {
            try
            {
                _log.Info($"Getting html documents...");
                var response = new List<HtmlDocument>();
                _log.Info($"Open webdriver element..");
                using (var driver = new PhantomJSDriver())
                {
                    foreach (var item in Urls)
                    {
                        _log.Info($"Getting informations for url : {item}");
                        driver.Navigate().GoToUrl(item);
                        _log.Info($"Waiting for parametrized ThreadSleep: {ThreadSleepTime}");
                        Thread.Sleep(ThreadSleepTime);
                        var html = new HtmlDocument();
                        html.LoadHtml(driver.PageSource);
                        response.Add(html);
                    }
                    _log.Info($"Closing webdriver element");
                    driver.Quit();
                }
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<EventModel> GetEvents()
        {
            var response = GetHtmlDocuments(Urls);
            return new List<EventModel>();    
        }

        public IEnumerable<OddModel> GetOdds()
        {
            throw new NotImplementedException();
        }

    }
}
