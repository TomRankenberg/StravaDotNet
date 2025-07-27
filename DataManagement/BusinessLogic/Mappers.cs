using Contracts.DTOs;
using Contracts.Interfaces;
using Data.Models.Strava;

namespace DataManagement.BusinessLogic
{
    public static class Mappers
    {
        public static ActivityDTO MapToActivityDto(DetailedActivity activity)
        {
            return new ActivityDTO
            {
                Id = activity.Id,
                Name = activity.Name,
                Distance = activity.Distance,
                MovingTime = activity.MovingTime,
                ElapsedTime = activity.ElapsedTime,
                TotalElevationGain = activity.TotalElevationGain,
                Type = activity.Type,
                StartDate = activity.StartDate,
                StartLatlng = ConvertToDto(activity.StartLatlng),
                EndLatlng = ConvertToDto(activity.EndLatlng),
                MapId = activity.MapId,
                ElevationGain = activity.TotalElevationGain,
            };
        }

        public static List<ActivityDTO> MapToActivityDtos(List<IDetailedActivity> activities)
        {
            List<ActivityDTO> activityDtos = [];
            foreach (var activity in activities)
            {
                if (activity is DetailedActivity detailedActivity)
                {
                    activityDtos.Add(MapToActivityDto(detailedActivity));
                }
            }
            return activityDtos;
        }

        public static LatLng ConvertToLatLng(LatLngDTO dto)
        {
            var result = new LatLng();
            result.AddRange(dto);
            return result;
        }

        public static LatLngDTO ConvertToDto(LatLng latLng)
        {
            var dto = new LatLngDTO();
            dto.AddRange(latLng);
            return dto;
        }
    }
}