using System;
using GoogleMaps.LocationServices;

namespace AlignMissions
{
    public interface IGeo
    {
        MapPoint EntityToMapPopint(MissionEntity missionEntity);

        string DistanceBetweenAddresses(MapPoint fromAddress, MapPoint toAddress);

        MissionEntity FindClosestMission(string address);
    }
}