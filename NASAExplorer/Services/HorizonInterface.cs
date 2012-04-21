using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASAExplorer.Services
{
    public struct Coord {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class HorizonInterface
    {
        private const string HOST = "horizons.jpl.nasa.gov";
        private const int PORT = 6775;

        private const string CENTER = "500@10";

        private DateTime Now { get; set; }
        private DateTime Next { get; set; }

        public TelnetConnection Conn { get; set; }

        private Dictionary<string, string> _cmds { get; set; }

        public HorizonInterface()
        {
            Conn = new TelnetConnection(HOST, PORT);
            Now = DateTime.Now;
            Next = Now.AddHours(1);

            _cmds = new Dictionary<string, string>();
            _cmds.Add("<cr>:", "E");
            _cmds.Add("[o,e,v,?] :", "v");
            _cmds.Add("[ <id>,coord,geo ] :", String.Format("{0}\n", CENTER));
            _cmds.Add("[ y/n ] -->", "y\n");
            _cmds.Add("[eclip, frame, body ] :", "eclip\n");
            _cmds.Add("] :", String.Format("{0}\n", Now.ToString()));
            _cmds.Add("] :", String.Format("{0}\n", Next.ToString()));
            _cmds.Add("? ] :", "1h\n");
            _cmds.Add("?] :", "n\n");
            _cmds.Add("[J2000, B1950] :", "J2000\n");
            _cmds.Add("LT+S ]  :", "1\n");
            _cmds.Add("3=KM-D] :'", "2\n");
            _cmds.Add("YES, NO ] :'", "YES\n");
            _cmds.Add("YES, NO ] :'", "NO\n");
            _cmds.Add("[ 1-6, ?  ] :'", "1\n");
        }

        public Coord GetCoordinates(int Id) {
            Coord loc = new Coord();
            int index = 0;

            loc.X = -1;
            loc.Y = -1;
            loc.Z = -1;

            if (Conn.IsConnected && Conn.Read() == "Horizon>")
            {
                Console.Write("win");
            }

            return loc;
        }

        public void Write(string cmd)
        {

        }
    }
}