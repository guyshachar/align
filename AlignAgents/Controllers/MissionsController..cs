using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AlignMissions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private ILoad Load { get; }
        private IInMemoryDb InMemoryDb { get; }
        private IGeo Geo { get; }

        public MissionsController(ILoad load, IInMemoryDb inMemoryDb, IGeo geo)
        {
            Load = load;
            InMemoryDb = inMemoryDb;
            Geo = geo;
        }

        // GET api/values
        [HttpGet]
        [Route("/countries-by-isolation")]
        public ActionResult<string> GetMostIsolatedCountry()
        {
            return Ok(InMemoryDb.GetMostIsolatedCountry());
        }

        // POST api/values
        [HttpPost]
        [Route("/missions")]
        public ActionResult<string> LoadMissions([FromBody] string value)
        {
            var count = Load.LoadMissions(value);

            return Ok($"Total of {count} missions loaded");
        }

        // POST api/values
        [HttpPost]
        [Route("/find-closest")]
        public ActionResult<string> FindClosestMission([FromBody] string value)
        {
            var closestMission = Geo.FindClosestMission(value);

            return Ok($"Closest mission to {value} is {closestMission}");
        }
    }
}
