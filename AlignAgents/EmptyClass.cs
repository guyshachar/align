using System.IO;
using System.Net;
using System.Text;
using GoogleMaps.LocationServices;
using Newtonsoft.Json;

namespace AlignMissions
{
    public class Geo : IGeo
    {
        private const string API_KEY = "AIzaSyD1y8s3JO7_28Qrg7oXJky0WyWRn4ZQKvg";
        private const char DISTANCE_UNIT = 'K';

        private static readonly GoogleLocationService locationService = new GoogleLocationService();
        private IInMemoryDb InMemoryDb { get; }

        public Geo(IInMemoryDb inMemoryDb)
        {
            InMemoryDb = inMemoryDb;
        }

        public MissionEntity FindClosestMission(string address)
        {
            try
            {
                var targetLocationEntity = JsonConvert.DeserializeObject<TargetLocationEntity>(address);

                var originLatLong = GetLatLong(address);

                (double minimalDistance, MissionEntity missionEntity) closestMission = (Double.MaxValue, null);

                foreach (var missionEntity in InMemoryDb.EnumerateMissions())
                {
                    var targetLatLong = GetLatLong($"{missionEntity.Address},{missionEntity.Country}");

                    var distance = CalcDistance(originLatLong.lat, originLatLong.lng, targetLatLong.lat, targetLatLong.lng, 'K');
                    if (distance < closestMission.minimalDistance)
                    {
                        closestMission = (distance, missionEntity);
                    }
                }

                return closestMission.missionEntity;
            }
            catch (Exception ex)
            { }

            return null;
        }

        private static (double lat, double lng) GetLatLong(string address)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={API_KEY}";
            var request = WebRequest.Create(url);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    dynamic jsonResponse = JsonConvert.DeserializeObject(reader.ReadToEnd());
                    var lat = jsonResponse.results[0].geometry.location.lat;
                    var lng = jsonResponse.results[0].geometry.location.lng;

                    return (lat, lng);
                }
            }
        }

        private static double CalcDistance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            if (lat1 == lat2 && lon1 == lon2)
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

    }
}
var originLatLong = GetLatLong(address);

(double minimalDistance, MissionEntity missionEntity) closestMission = (Double.MaxValue, null);

                foreach (var missionEntity in InMemoryDb.EnumerateMissions())
                {
                    var targetLatLong = GetLatLong($"{missionEntity.Address},{missionEntity.Country}");

var distance = CalcDistance(originLatLong.lat, originLatLong.lng, targetLatLong.lat, targetLatLong.lng, 'K');
                    if (distance<closestMission.minimalDistance)
                    {
                        closestMission = (distance, missionEntity);
                    }
                }

                return closestMission.missionEntity;
            }
            catch (Exception ex)
            { }

            return null;
        }

        private static (double lat, double lng) GetLatLong(string address)
{
    var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={API_KEY}";
    var request = WebRequest.Create(url);

    using (var response = (HttpWebResponse)request.GetResponse())
    {
        using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        {
            dynamic jsonResponse = JsonConvert.DeserializeObject(reader.ReadToEnd());
            var lat = jsonResponse.results[0].geometry.location.lat;
            var lng = jsonResponse.results[0].geometry.location.lng;

            return (lat, lng);
        }
    }
}

private static double CalcDistance(double lat1, double lon1, double lat2, double lon2, char unit)
{
    if (lat1 == lat2 && lon1 == lon2)
    {
        return 0;
    }
    else
    {
        double theta = lon1 - lon2;
        double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);
        dist = dist * 60 * 1.1515;
        if (unit == 'K')
        {
            dist = dist * 1.609344;
        }
        else if (unit == 'N')
        {
            dist = dist * 0.8684;
        }
        return (dist);
    }
}

//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//::  This function converts decimal degrees to radians             :::
//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
private static double deg2rad(double deg)
{
    return (deg * Math.PI / 180.0);
}

//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//::  This function converts radians to decimal degrees             :::
//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
private static double rad2deg(double rad)
{
    return (rad / Math.PI * 180.0);
}

    }
}