using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Lan_Adress_Scanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "Lan Adres Ve İndex Tarayıcı";
            List<string> aktifler = new List<string>();
            for (; ; )
            {
                aktifler.Clear();
                Console.Clear();
                Stopwatch stop = new Stopwatch();
                stop.Start();
                Console.WriteLine("Website : kodzamani.weebly.com");
                Console.WriteLine("Instagram : @kodzamani.tk");
                Console.Write("Alt internet adresi ( Varsayılan 192.168.1 ) :");
                string altadres = Console.ReadLine();
                if (altadres == "")
                    altadres = "192.168.1";
                Console.WriteLine("--------------------İp Adresleri--------------------");
                Parallel.For(2, 255, i =>
                  {
                      try
                      {
                          string ip = $"{altadres}.{i}";
                          Ping ping = new Ping();
                          PingReply reply = ping.Send(ip, 100);
                          if (reply.Status == IPStatus.Success)
                          {
                              Console.WriteLine(ip + " > Bağlantı Var");
                              aktifler.Add(ip);
                          }
                          else
                              Console.WriteLine(ip + " > Bağlantı Yok");
                      }
                      catch
                      {
                          Console.WriteLine($"{altadres}.{i}" + " > Bağlantı Hatası");
                      }
                  });
                Console.WriteLine("--------------------İndexler--------------------");
                if (aktifler.Count == 0)
                    Console.WriteLine("Hiçbir açık bağlantı bulunamadı.");
                Parallel.ForEach(aktifler, aktif =>
                 {
                     try
                     {
                         HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + aktif);
                         HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                             Console.WriteLine(aktif + " > İndex Var");
                     }
                     catch { Console.WriteLine(aktif + " > İndex Yok"); }
                 });
                stop.Stop();
                Console.WriteLine("--------------------Sonuç--------------------");
                Console.WriteLine("Geçen süre saniye : " + stop.Elapsed.TotalSeconds);
                Console.WriteLine("Tüm işlemler bitirildi. tekrar taramak için bir tuşa basın.");
                Console.ReadLine();
            }
        }
    }
}