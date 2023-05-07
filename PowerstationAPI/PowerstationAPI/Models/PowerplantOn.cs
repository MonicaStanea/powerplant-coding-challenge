namespace PowerstationAPI.Models
{
    /// <summary>
    /// The class containing the properties for a powerplant that will be returned
    /// </summary>
    public class PowerplantOn
    {
        /// <summary>
        /// the name or the powerplant
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the power that needs to generate
        /// </summary>
        public double P { get; set; }
    }
}
