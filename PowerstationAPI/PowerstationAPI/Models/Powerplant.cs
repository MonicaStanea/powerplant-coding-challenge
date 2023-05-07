using Newtonsoft.Json;

/// <summary>
/// The class with the powerplant properties
/// </summary>
public class Powerplant
{
    /// <summary>
    /// The name of the powerplant
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The type of the powerplant
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// The efficiency of the powerplant
    /// </summary>
    public double Efficiency { get; set; }

    /// <summary>
    /// The minimum power generated when the powerplant is turned on
    /// </summary>
    public int Pmin { get; set; }

    /// <summary>
    /// The maximum power generated when the powerplant is turned on
    /// </summary>
    public int Pmax { get; set; }

    /// <summary>
    /// The price to produce a kWh
    /// </summary>
    internal double PriceOfKwh { get; set; }

    /// <summary>
    /// Pmin x Efficiency
    /// </summary>
    internal double PminReal { get; set; }

    /// <summary>
    /// Pmax x Efficiency
    /// </summary>
    internal double PmaxReal { get; set; }
}