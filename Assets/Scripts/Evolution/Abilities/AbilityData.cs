using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put Abilities here to register them
public enum AbilityTypes { Grow, GrowLeaves, CollectSunlight, ReceiveShadows };

// It takes care of the initialization of the basic data structures (Abilities that Characters have or develop).
public static class AbilityData
{
    private static List<System.Type> availableAbilityTypes = new List<System.Type>();


    static AbilityData ()
    {
        // Add Abilities
        foreach (AbilityTypes abilityType in System.Enum.GetValues (typeof (AbilityTypes)))
            availableAbilityTypes.Add (System.Type.GetType(abilityType.ToString()));
    }

    
    public static System.Type getAbilityType ( AbilityTypes ability_type )
    {
        return availableAbilityTypes[(int)ability_type];
    }
    
}

