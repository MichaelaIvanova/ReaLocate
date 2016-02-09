using Geocoding.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GoogleMapsMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            GoogleGeocoder geocoder = new GoogleGeocoder();
            List<GoogleAddress> addresses = geocoder.Geocode("Metodi Andonov 107, 1797 Sofia, Bulgaria").ToList();

            var coordinates = addresses[0].Coordinates;
            var address = @addresses[0].FormattedAddress;

            var c = new Coordinate() { Address = address, Lat= coordinates.Latitude, Long = coordinates.Longitude };

            return View(c);
        }

        public ActionResult Contact()
        {

            return View();
        }
    }

    public class Coordinate
    {
        public double Long { get; set; }

        public double Lat { get; set; }

        public string Address { get; set; }
    }
}
