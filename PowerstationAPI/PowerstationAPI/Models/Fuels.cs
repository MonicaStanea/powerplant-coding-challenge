using Newtonsoft.Json;

/// <summary>
/// Informations about fuels used in the powerplants
/// </summary>
public class Fuels
{
    /// <summary>
    /// The gas price in euro/MWh
    /// </summary>
    public double Gas { get; set; }

    /// <summary>
    /// The kerosine price in euro/MWh
    /// </summary>
    public double Kerosine { get; set; }

    /// <summary>
    /// The Co2 emission price in euro/ton
    /// </summary>
    public int Co2 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Wind { get; set; }
}
