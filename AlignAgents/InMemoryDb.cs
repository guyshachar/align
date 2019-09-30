using System;
using System.Collections.Generic;

namespace AlignMissions
{
    public class InMemoryDb : IInMemoryDb
    {
        private const string NO_MISSIONS_LOADED = "No Missions Loaded";

        private List<MissionEntity> MissionsList { get; }
        private Dictionary<string, string> IsolatedAgents { get; }
        private List<string> NonIsolatedAgents { get; }
        private Dictionary<string, int> IsolationsPerCountry { get; }
        private string MostIsolatedCountry { get; set; }

        public InMemoryDb()
        {
            MissionsList = new List<MissionEntity>();
            IsolatedAgents = new Dictionary<string, string>();
            NonIsolatedAgents = new List<string>();
            IsolationsPerCountry = new Dictionary<string, int>();
        }

        public void LoadEntities(List<MissionEntity> entities)
        {
            MissionsList.AddRange(entities);

            IsolatedAgents.Clear();
            IsolationsPerCountry.Clear();
            MostIsolatedCountry = null;

            foreach (var entity in entities)
            {
                ParseEntity(entity);
            }

            // Calculate IsolationsPerCountry
            foreach ((string agent, string country) in IsolatedAgents)
            {
                if (!IsolationsPerCountry.ContainsKey(country))
                    IsolationsPerCountry.Add(country, 0);
                IsolationsPerCountry[country]++;

                if (string.IsNullOrWhiteSpace(MostIsolatedCountry) || IsolationsPerCountry[country] > IsolationsPerCountry[MostIsolatedCountry])
                    MostIsolatedCountry = country;
            }
        }

        private void ParseEntity(MissionEntity entity)
        {
            if (NonIsolatedAgents.Contains(entity.Agent))
                return;

            if (!IsolatedAgents.ContainsKey(entity.Agent))
                IsolatedAgents.Add(entity.Agent, entity.Country);
            else
            {
                IsolatedAgents.Remove(entity.Agent);
                NonIsolatedAgents.Add(entity.Agent);
            }
        }

        public bool IsAgentIsolated(string agent)
        {
            return IsolatedAgents.ContainsKey(agent);
        }

        public string GetMostIsolatedCountry()
        {
            return MostIsolatedCountry ?? NO_MISSIONS_LOADED;
        }

        public IEnumerable<MissionEntity> EnumerateMissions()
        {
            foreach (var entity in MissionsList)
                yield return entity;
        }
    }
}