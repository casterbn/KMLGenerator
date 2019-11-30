using System;
using System.IO;
using System.Text;

namespace KMLGenerator
{
    class Program
    {
        //const double MINLAT = -90;
        const double MAXLAT = 90;
        //const double MINLONG = -180;
        const double MAXLONG = 180;

        const string KMLOPENTAGS = @"<?xml version='1.0' encoding='UTF-8'?><kml xmlns='http://www.opengis.net/kml/2.2'><Document>";
        const string KMLCLOSETAGS = @"</Document></kml>";

        static void Main(string[] args)
        {
            int numPlacemarks = 99;
            string filePath = "test.kml";

            if (args != null && args.Length > 0)
            {
                filePath = args[0];
            }
            else
            {
                //do something meaningful
            }

            if (args != null && args.Length > 1)
            {
                Int32.TryParse(args[1], out numPlacemarks);
            }

            var rand = new Random();

            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                outputFile.WriteLine(KMLOPENTAGS);

                double randLat = 0;
                double randLong = 0;

                for (int i = 0; i < numPlacemarks; i++)
                {
                    randLat = rand.NextDouble() * ((rand.NextDouble() >= 0.5) ? -1 : 1) * MAXLAT;
                    randLong = rand.NextDouble() * ((rand.NextDouble() >= 0.5) ? -1 : 1) * MAXLONG;

                    outputFile.WriteLine(CreateKmlPlacemark("Point:" + i, randLat, randLong));
                }

                outputFile.WriteLine(KMLCLOSETAGS);
            }
        }

        static string CreateKmlPlacemark(string name, double latitude, double longitude)
        {
            StringBuilder newPlacemark = new StringBuilder();

            newPlacemark.Append(@"<Placemark>");
            newPlacemark.Append(@"<name>" + name + @"</name>");
            newPlacemark.Append(@"<Point>");
            newPlacemark.Append(@"<coordinates>" + longitude.ToString() + "," + latitude.ToString() + @"</coordinates>");
            newPlacemark.Append(@"</Point>");
            newPlacemark.Append(@"</Placemark>");

            return newPlacemark.ToString();
        }

    }
}
