using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Contracts.Interfaces;

namespace Data.Models.Strava
{

    /// <summary>
    /// A pair of latitude/longitude coordinates, represented as an array of 2 floating point numbers.
    /// </summary>
    [DataContract]
    public class LatLng : List<float?>, ILatLng
    {

        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LatLng {\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public new string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
