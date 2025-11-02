using Contracts.Interfaces;
using Data.BusinessLogic;
using Data.Models.Strava;
using Newtonsoft.Json;

namespace StravaDotNet.Components.Services
{
    public class SavingService(IUnitOfWork unitOfWork, IStravaService stravaService) : ISavingService
    {
        private readonly DataSaver dataSaver = new(unitOfWork);

        public async Task SaveActivities(string activitiesJson, string? token)
        {
            List<DetailedActivity>? activities = JsonConvert.DeserializeObject<List<DetailedActivity>>(activitiesJson);

            if (activities == null || activities.Count == 0)
            {
                return;
            }

            foreach (DetailedActivity activity in activities)
            {
                string? activityString = await stravaService.GetActivityById(activity.Id, token);
                if (activityString == null)
                {
                    continue;
                }

                DetailedActivity? detailedActivity = JsonConvert.DeserializeObject<DetailedActivity>(activityString);

                if (detailedActivity == null)
                {
                    continue;
                }
                try
                {
                    await dataSaver.SaveActivity(detailedActivity, detailedActivity.Athlete.Id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}