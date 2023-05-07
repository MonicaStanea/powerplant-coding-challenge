namespace PowerstationAPI.Models
{
    /// <summary>
    /// Schema for the json object needs to be sent in the body of the request
    /// </summary>
    public class Payload
    {
        /// <summary>
        /// load
        /// </summary>
        public int Load { get; set; }

        /// <summary>
        /// fuels
        /// </summary>
        public Fuels Fuels { get; set; }

        /// <summary>
        /// powerplants
        /// </summary>
        public List<Powerplant> Powerplants { get; set; }
    }
}
