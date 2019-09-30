using System;
using GoogleMaps.LocationServices;

namespace AlignMissions
{
    public interface IGeo
    {
        (MissionEntity mission, Double minimalDistance) FindClosestMission(string address);
    }
}