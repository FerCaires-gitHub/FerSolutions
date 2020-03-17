using Application.Parser;
using Application.Service;
using CrossCutting.Ioc;
using Domain.Dictionary;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp
{
    public class Program
    {
        private static ILog _log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            _log.Info($"Getting instances");
            var container = ModuleBase.Config();
            var service = container.GetInstance<BetService>();
            var bet365Parser = container.GetInstance<Bet365Parser>();
            var sportingbetParser = container.GetInstance<SportingBetParser>();
            var oddsMonitor = container.GetInstance<OddsMonitorParser>();


            oddsMonitor.GetEvents();
            sportingbetParser.GetEvents();
            bet365Parser.GetOdds();

            


            //service.GetAll();
            Console.WriteLine("Inicio das tasks paralelas");
            Task.Run( 
                () => {
                    Console.WriteLine("Simulação de todas as possibilidade");
                    var sw = Stopwatch.StartNew(); ;
                    service.GetAll();
                    sw.Stop();
                    Console.WriteLine($"Tempo de processamento: {sw.ElapsedMilliseconds} ms | Contagem: {BetDictionaries.BetPairs.Count()} combinações");
                    }
            );

            Thread.Sleep(10000);
            var bets = service.GetBet(1.44, 2.605);
            if (bets == null) Console.WriteLine("No bets found");
            else
            {
                foreach (var item in bets)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            
            Console.WriteLine(BetDictionaries.BetPairs.Count);
            Console.ReadKey();

        }
    }
}

