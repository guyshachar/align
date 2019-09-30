using System;
using System.Collections.Generic;

namespace AlignMissions
{
    public interface IInMemoryDb
    {
        void LoadEntities(List<MissionEntity> entities);
        bool IsAgentIsolated(string agent);
        string GetMostIsolatedCountry();
        IEnumerable<MissionEntity> EnumerateMissions();
    }
}
