using System;
using GoogleMaps.LocationServices;
using Newtonsoft.Json;

namespace AlignMissions
{
    public class Geo : IGeo
    {
        private static readonly GoogleLocationService locationService = new GoogleLocationService();
        private IInMemoryDb InMemoryDb { get; }

        public Geo(IInMemoryDb inMemoryDb)
        {
            InMemoryDb = inMemoryDb;
        }

        public MapPoint EntityToMapPopint(MissionEntity missionEntity)
        {
            var mapPoint = locationService.GetLatLongFromAddress($"{missionEntity.Address},{missionEntity.Country}");
            return mapPoint;
        }

        public string DistanceBetweenAddresses(MapPoint fromAddress, MapPoint toAddress)
        {
            var directions = locationService.GetDirections(fromAddress.Latitude, toAddress.Longitude);
            return directions.Distance;
        }

        public MissionEntity FindClosestMission(string address)
        {
            try
            {
                var targetLocationEntity = JsonConvert.DeserializeObject<TargetLocationEntity>(address);

                var latLong = locationService.GetLatLongFromAddress(targetLocationEntity.TargetLocation);

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
            catch (Exception ex)
            { }

            return null;
        }
    }
}