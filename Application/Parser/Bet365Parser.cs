using Domain.Model;
using Domain.Parser;
using Domain.Service;
using HtmlAgilityPack;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Parser
{
    public class Bet365Parser : IBet365Parser
    {
        private const string url = "https://www.bet365.com/#/AC/B1/C1/D8/E87174040/F3/G0/H0/I1/P^13/Q^45694224";
        private const string groups = "gl-MarketGroup";
        private const string GroupText = "gl-MarketGroupButton_Text";
        private const string ParticipantName = "gl-Participant_Name";
        private const string ParticipantOdd = "gl-Participant_Odds";
        private int ThreadSleepTime = 5000;

        private string UrlString = string.Empty;
        private List<HtmlDocument> htmlDocuments = new List<HtmlDocument>();
        private List<string> Urls = new List<string>() { url };
        private ILog _log;
        

        public Bet365Parser()
        {
            _log = LogManager.GetLogger(typeof(Bet365Parser));
        }
        public IEnumerable<OddModel> GetOdds()
        {
            var response = GetHtmlDocuments(Urls);
            return new List<OddModel>();
        }

        private string GetContent()
        {
            var response = string.Empty;
            using (var driver = new PhantomJSDriver())
            {
                driver.Navigate().GoToUrl(url);
                response = driver.PageSource;
            }

            return response;
        }

        public IDictionary<string,IEnumerable<OddModel>> GetMatchOdds()
        {
            return new Dictionary<string, IEnumerable<OddModel>>();
        }


        private IEnumerable<HtmlDocument> GetHtmlDocuments(IEnumerable<string> Urls)
        {
            try
            {
                _log.Info($"Getting html documents...");
                var response = new List<HtmlDocument>();
                _log.Info($"Open webdriver element..");
                using (var driver  = new PhantomJSDriver())
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

        private IReadOnlyCollection<IWebElement> GetElements()
        {
            var response = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
            using (var driver = new PhantomJSDriver())
            { 
                driver.Navigate().GoToUrl(url);
                
                var pageSource = driver.PageSource;
                Thread.Sleep(5000);
                var newPageSource = driver.PageSource;
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(driver.PageSource);
                htmlDocuments.Add(htmlDoc);

                response = driver.FindElementsByClassName(groups);
                driver.Quit();
            }
            return response;
        }        

        public IEnumerable<EventModel> GetEvents()
        {
            return new List<EventModel>();
        }

        public IEnumerable<OddModel> GetDoubleChance()
        {
            return new List<OddModel>();
        }
        public void Dispose()
        {

        }
    }
}
