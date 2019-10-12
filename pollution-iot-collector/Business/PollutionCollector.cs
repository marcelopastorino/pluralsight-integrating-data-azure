using System;
using Evangeloper.PollutionDataCollector.Helpers;
using Evangeloper.PollutionDataCollector.Models;

namespace Evangeloper.PollutionDataCollector.Business
{
    public class PollutionCollector : IPollutionCollector
    {
        // Collect polution data
        public PollutionData Collect()
        {
            return new PollutionData() {
                ReadingId = Randomizer.Guid(),
                ReadingDateTime = DateTime.Now,
                LocationId = Randomizer.Number(1000, 2000),
                PollutionLevel = Randomizer.Number(1, 2)
            };
        }
    }
}