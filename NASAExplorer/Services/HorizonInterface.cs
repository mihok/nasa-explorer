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

        public TelnetConnection Conn { get; set; }

        public HorizonInterface()
        {
            Conn = new TelnetConnection(HOST, PORT);
        }

        public Coord GetCoordinates(int Id) {
            Coord loc = new Coord();
            loc.X = -1;
            loc.Y = -1;
            loc.Z = -1;

            if (Conn.IsConnected && Conn.Read() == "Horizon>")
            {
                Conn.Write("");
            }

            return loc;
        }

        public void Write(string cmd)
        {

        }
    }
}