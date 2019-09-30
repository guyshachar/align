using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AlignMissions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static string FindClosestMission(string address)
        {
            GoogleLocationService locationService = new GoogleLocationService();

            try
            {
                string originLocationEntity = "3 Derb Lferrane, Marrakech, Morocco";

                string targetLocationEntity = "27 Derb Lferrane, Marrakech, Morocco";

                var latLong = locationService.GetLatLongFromAddress(origin);

                (string minimalDistance, MissionEntity missionEntity) closestMission = ("XXX", null);

                foreach (var missionEntity in InMemoryDb.EnumerateMissions())
                {
                    var entityMapPoint = EntityToMapPopint(missionEntity);
                    var distance = DistanceBetweenAddresses(entityMapPoint, latLong);
                    //if (distance < closestMission.minimalDistance)
                    //{
                    //    closestMission = (distance, missionEntity);
                    //}
                }

                return closestMission.missionEntity;
            }
            catch (Excep
    }
        }
