using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlignMissions
{
    public class Load : ILoad
    {
        private IInMemoryDb InMemoryDb { get; }

        public Load(IInMemoryDb inMemoryDb)
        {
            InMemoryDb = inMemoryDb;
        }

        public int LoadMissions(string missions)
        {
            try
            {
                var missionsList = JsonConvert.DeserializeObject<List<MissionEntity>>(missions);
                InMemoryDb.LoadEntities(missionsList);

                return missionsList.Count;
            }
            catch (Exception ex)
            {
                var mission = JsonConvert.DeserializeObject<MissionEntity>(missions);
                InMemoryDb.LoadEntities(new List<MissionEntity> { mission });

                return 1;
            }
        }
    }
}