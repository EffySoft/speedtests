using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SpeedTest {
    static class SpeedTestHelper {
        public static string GetInternetSpeed () {
            var nics = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces ();
            // Select desired NIC
            var nic = nics.Single (n => n.Name.Contains("Wireless"));
            var reads = Enumerable.Empty<double> ();
            var sw = new Stopwatch ();
            var lastBr = nic.GetIPv4Statistics ().BytesReceived;
            for (var i = 0; i < 1000; i++) {

                sw.Restart ();
                Thread.Sleep (100);
                var elapsed = sw.Elapsed.TotalSeconds;
                var br = nic.GetIPv4Statistics ().BytesReceived;

                var local = (br - lastBr) / elapsed;
                lastBr = br;

                // Keep last 20, ~2 seconds
                reads = new [] { local }.Concat (reads).Take (20);

                if (i % 10 == 0) { // ~1 second
                    var bSec = reads.Sum () / reads.Count ();
                    var kbs = (bSec * 8) / 1024;
                    Console.WriteLine ("Kb/s ~ " + kbs);
                    //return $"{kbs} Kb/s";
                }

            }
            return string.Empty;
        }

    }
}