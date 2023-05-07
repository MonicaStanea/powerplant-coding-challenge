using PowerstationAPI.Models;
using System;

internal class PowerCalculator
{
    const string gasfired = "gasfired";
    const string turbojet = "turbojet";
    const string windturbine = "windturbine";
    private static List<PowerplantOn> PowerPlantOnList;

    internal static IResult ProcessPowerplants(Payload requestBody)
    {
        if (requestBody == null)//validations // not implemented
        {
            return Results.BadRequest("Wrong request");
        }

        List<Powerplant> powerplantList = SetPowerplantCalculationsValues(requestBody);

        var powerNeeded = requestBody.Load;

        List<PowerplantOn> powerplantOnList = OptimizePowerplantsForLoad(powerNeeded, powerplantList);

        return Results.Ok(powerplantOnList);

    }

    private static List<PowerplantOn> OptimizePowerplantsForLoad(int powerNeeded, List<Powerplant> powerplantList)
    {
        PowerPlantOnList = new List<PowerplantOn>();
        bool missionAccomplished = false;
        int index = 0;
        double CurrentPower = 0;

        while (index < powerplantList.Count && !missionAccomplished)
        {
            if (CurrentPower + powerplantList[index].PmaxReal <= powerNeeded)
            {
                CurrentPower += powerplantList[index].PmaxReal;
                AddPowerplantOnToList(powerplantList[index].Name, powerplantList[index].PmaxReal);
                index++;
                if (CurrentPower == powerNeeded)
                { 
                    missionAccomplished = true;
                }
            }
            else //CurrentPower + powerplantList[index].PmaxReal > powerNeeded
            if (powerNeeded - CurrentPower >= powerplantList[index].PminReal)
            {
                //Start the powerplant but not at full capacity
                AddPowerplantOnToList(powerplantList[index].Name, powerNeeded - CurrentPower);

                index++;
                missionAccomplished = true;
            }
            else //powerNeeded - CurrentPower < powerplantList[index].PminReal
            //1.search if another powerplant(s) already added could run at a diminished power by the surplus of power, and adjust them
            //2.search for another PP at the same price that could have a PminReal smaller, to not waste power
            {
                //1.
                var powerOnIndex = index - 1; //index of last powerplant added
                var excessOfPower = powerplantList[index].PminReal + CurrentPower - powerNeeded;
                while (powerOnIndex >= 0 && !missionAccomplished)
                {
                    if (powerplantList[powerOnIndex].PmaxReal - excessOfPower >= powerplantList[powerOnIndex].PminReal)
                    {
                        //the powerpland already added can produce less power

                        //Add current powerplant at minimum power
                        AddPowerplantOnToList(powerplantList[index].Name, powerplantList[index].PminReal);
                        index++;

                        //adjust power of already added
                        PowerPlantOnList[powerOnIndex].P = powerplantList[powerOnIndex].PmaxReal - excessOfPower;
                        missionAccomplished = true;
                    }
                    else //powerplantList[powerOnIndex].PmaxReal - excessOfPower < powerplantList[powerOnIndex].PminReal
                    {
                        //modify the power of the powerplant already added to produce at minimum and adjust the excess of power with the difference
                        PowerPlantOnList[powerOnIndex].P = powerplantList[powerOnIndex].PminReal;
                        excessOfPower -= powerplantList[powerOnIndex].PmaxReal - powerplantList[powerOnIndex].PminReal;

                        //go to next powerplant in line to see if can adjust
                        powerOnIndex--;
                    }
                }
            }
        }

        if (missionAccomplished && index < powerplantList.Count)
        {
            for (int i = index; i < powerplantList.Count; i++)
            {
                //Add the rest of Powerplants at 0 power (will not be turned on)
                AddPowerplantOnToList(powerplantList[i].Name, 0);
            }
        }

        return PowerPlantOnList;
    }

    private static void AddPowerplantOnToList(string name, double power)
    {
        PowerPlantOnList.Add(new PowerplantOn
        {
            Name = name,
            P = power
        });
    }

    /// <summary>
    /// calculates the prices for kwh for each powerplant and the power accprding to efficiency
    /// </summary>
    /// <param name="requestBody"></param>
    /// <returns>the list of powerplants ordered by price to produce one kwh</returns>
    private static List<Powerplant> SetPowerplantCalculationsValues(Payload requestBody)
    {
        var powerplantList = requestBody.Powerplants;

        foreach (var powerplant in powerplantList)
        {
            if (powerplant.Type == windturbine)
            {
                powerplant.PminReal = powerplant.Pmin * requestBody.Fuels.Wind / 100;
                powerplant.PmaxReal = powerplant.Pmax * requestBody.Fuels.Wind / 100;
            }
            else
            {
                powerplant.PminReal = powerplant.Pmin;
                powerplant.PmaxReal = powerplant.Pmax;

                if (powerplant.Type == gasfired)
                {
                    powerplant.PriceOfKwh = requestBody.Fuels.Gas / powerplant.Efficiency;
                }
                else// if (powerplant.Type == turbojet)
                {
                    powerplant.PriceOfKwh = requestBody.Fuels.Kerosine / powerplant.Efficiency;
                }
            }
        }

        return powerplantList.OrderBy(p => p.PriceOfKwh).ToList();
    }

}
