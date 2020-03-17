using Domain.Model;
using Domain.Parser;
using HtmlAgilityPack;
using log4net;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Parser
{
    public class OddsMonitorParser : IOddsMonitorParser
    {
        public const string url = "https://oddspedia.com/football/";
        private ILog _log;
        private int ThreadSleepTime = 15000;
        private List<string> Urls = new List<string>() { "https://www.betmonitor.com/surebets/" };

        public OddsMonitorParser()
        {
            _log = LogManager.GetLogger(typeof(OddsMonitorParser));
        }

        public IDictionary<string, IEnumerable<OddModel>> GetMatchOdds() => throw new NotImplementedException();
        public IEnumerable<OddModel> GetDoubleChance() => throw new NotImplementedException();

        public IEnumerable<EventModel> GetEvents()
        {
            var response = GetHtmlDocuments(Urls);
            return new List<EventModel>();
        }

        public IEnumerable<OddModel> GetOdds() => throw new NotImplementedException();

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
                        var sw = Stopwatch.StartNew();
                        _log.Info($"Getting informations for url : {item}");
                        driver.Navigate().GoToUrl(item);
                        _log.Info($"Waiting for parametrized ThreadSleep: {ThreadSleepTime}");
                        Thread.Sleep(ThreadSleepTime);
                        var html = new HtmlDocument();
                        html.LoadHtml(driver.PageSource);
                        response.Add(html);
                        sw.Stop();
                        _log.Info($"ElapsedTime: {sw.ElapsedMilliseconds} ms");
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
    }
}
