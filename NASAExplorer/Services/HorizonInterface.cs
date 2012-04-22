using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;

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

        public DateTime Now { get; set; }
        public DateTime Next { get; set; }

        public TelnetConnection Conn { get; set; }

        private Dictionary<int, KeyValuePair<string, string>> _cmds { get; set; }

        public HorizonInterface()
        {
            Conn = new TelnetConnection(HOST, PORT);
            Now = DateTime.Now;
            Next = Now.AddHours(1);

            _cmds = new Dictionary<int, KeyValuePair<string, string>>();
            _cmds.Add(0, new KeyValuePair<string, string>("<cr>:", "E"));
            _cmds.Add(1, new KeyValuePair<string, string>("[o,e,v,?] :", "v"));
            _cmds.Add(2, new KeyValuePair<string, string>("[ <id>,coord,geo ] :", String.Format("{0}", CENTER)));
            _cmds.Add(3, new KeyValuePair<string, string>("[ y/n ] -->", "y"));
            _cmds.Add(4, new KeyValuePair<string, string>("[eclip, frame, body ] :", "eclip"));
            _cmds.Add(5, new KeyValuePair<string, string>("] :", String.Format("{0}", Now.ToString("yyyy-MMM-dd hh:mm"))));
            _cmds.Add(6, new KeyValuePair<string, string>("] :", String.Format("{0}", Next.ToString("yyyy-MMM-dd hh:mm"))));
            _cmds.Add(7, new KeyValuePair<string, string>("? ] :", "1h"));
            _cmds.Add(8, new KeyValuePair<string, string>("?] :", "n"));
            _cmds.Add(9, new KeyValuePair<string, string>("[J2000, B1950] :", "J2000"));
            _cmds.Add(10, new KeyValuePair<string, string>("LT+S ]  :", "1"));
            _cmds.Add(11, new KeyValuePair<string, string>("3=KM-D] :'", "2"));
            _cmds.Add(12, new KeyValuePair<string, string>("YES, NO ] :'", "YES"));
            _cmds.Add(13, new KeyValuePair<string, string>("YES, NO ] :'", "NO"));
            _cmds.Add(14, new KeyValuePair<string, string>("[ 1-6, ?  ] :'", "1"));
        }

        public HorizonInterface(DateTime start, DateTime end)
        {
            Conn = new TelnetConnection(HOST, PORT);
            Now = start;
            Next = end;

            _cmds = new Dictionary<int, KeyValuePair<string, string>>();
            _cmds.Add(0, new KeyValuePair<string, string>("<cr>:", "E"));
            _cmds.Add(1, new KeyValuePair<string, string>("[o,e,v,?] :", "v"));
            _cmds.Add(2, new KeyValuePair<string, string>("[ <id>,coord,geo ] :", String.Format("{0}", CENTER)));
            _cmds.Add(3, new KeyValuePair<string, string>("[ y/n ] -->", "y"));
            _cmds.Add(4, new KeyValuePair<string, string>("[eclip, frame, body ] :", "eclip"));
            _cmds.Add(5, new KeyValuePair<string, string>("] :", String.Format("{0}", Now.ToString("yyyy-MMM-dd hh:mm"))));
            _cmds.Add(6, new KeyValuePair<string, string>("] :", String.Format("{0}", Next.ToString("yyyy-MMM-dd hh:mm"))));
            _cmds.Add(7, new KeyValuePair<string, string>("? ] :", "1h"));
            _cmds.Add(8, new KeyValuePair<string, string>("?] :", "n"));
            _cmds.Add(9, new KeyValuePair<string, string>("[J2000, B1950] :", "J2000"));
            _cmds.Add(10, new KeyValuePair<string, string>("LT+S ]  :", "1"));
            _cmds.Add(11, new KeyValuePair<string, string>("3=KM-D] :'", "2"));
            _cmds.Add(12, new KeyValuePair<string, string>("YES, NO ] :'", "YES"));
            _cmds.Add(13, new KeyValuePair<string, string>("YES, NO ] :'", "NO"));
            _cmds.Add(14, new KeyValuePair<string, string>("[ 1-6, ?  ] :'", "1"));
        }

        public List<Coord> GetCoordinates(int Id) {
            List<Coord> loc = new List<Coord>();
            string buffer = "";
            List<string> stopString = new List<string>();
            
            stopString.Add("$$SOE");
            stopString.Add("$$EOE");

            buffer = Conn.ReadUntil(">");
            Conn.Write(String.Format("{0}\n", Id));

            for (int i = 0; i <= _cmds.Count - 1; i++)
            {
                buffer += Conn.Read();//(_cmds[i].Key, 100);
                Conn.WriteLine(_cmds[i].Value);
            }

            buffer += Conn.Read();
            buffer = buffer.Split(stopString.ToArray(), StringSplitOptions.RemoveEmptyEntries)[1];

            foreach (string set in Regex.Split(buffer.Trim(), "\r\n"))
            {
                string[] element = set.Split(',');
                if (element.Length > 0)
                {
                    Coord temp = new Coord();
                    
                    temp.X = double.Parse(element[2]);
                    temp.Y = double.Parse(element[3]);
                    temp.Z = double.Parse(element[4]);

                    loc.Add(temp);
                }
            }
            /*
            TcpClient client = new TcpClient(HOST, PORT);
            TelnetStream conn = new TelnetStream(client.GetStream());

            conn.SetRemoteMode(TelnetOption.Echo, false);
            Expector expect = new Expector(conn);

            expect.Expect("Horizons>");
            expect.SendLine(String.Format("{0}\n", Id));

            for (var i = 0; i <= 14; i++)
            {
                expect.Expect(_cmds[i].Key);
                expect.SendLine(_cmds[i].Value);
            }
            */
            return loc;
        }

        public void Write(string cmd)
        {

        }
    }
}