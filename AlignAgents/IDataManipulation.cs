using System;
using System.Collections.Generic;

namespace AlignMissions
{
    public interface IDataManipulation
    {
        Dictionary<string, int> IsolationsPerCount();

        string MostIsolatedCountry();

        MissionEntity ClosestMission();
    }
}