using Evangeloper.PollutionDataCollector.Models;

namespace Evangeloper.PollutionDataCollector.Business
{
    public interface IPollutionCollector
    {
        PollutionData Collect();
    }
}