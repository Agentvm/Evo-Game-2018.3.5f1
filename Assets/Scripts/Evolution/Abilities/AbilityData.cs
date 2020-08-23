using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AbilityTypes { Grow };

// It takes care of the initialization of the basic data structures (Traits that Characters have or develop).
public static class AbilityData
{
    private static System.Type[] available_ability_types = new System.Type[1];


    static AbilityData ()
    {

        // Add Abilities
        available_ability_types[(int)AbilityTypes.Grow] = typeof (Grow);


        // Check that every entry of enum has been set
        for ( int i = 0; i < AbilityTypes.GetNames (typeof (AbilityTypes)).Length; i++ ) // number of enum variants
        {
            if ( available_ability_types[i] == null )
                Debug.LogError ("The Ability " + ((AbilityTypes)i).ToString () + " at position available_ability_types[" + i + "] has not been set correctly.");
        }

        // Check that enum and array match each other
        for ( int i = 0; i < AbilityTypes.GetNames (typeof (AbilityTypes)).Length; i++ )
        {
            if ( available_ability_types[i].Name != ((AbilityTypes)i).ToString () )
                Debug.LogError ("Initialization of Traits failed. available_traits[" + i + "].Name = " + available_ability_types[i].Name + ", when it should be " + ((AbilityTypes)i).ToString ());
        }

    }

    
    public static System.Type getAbilityType ( AbilityTypes ability_type )
    {
        return available_ability_types[(int)ability_type];
    }
    
}

//available_abilities.Add (new Ability ("Grow", new List<string> () { "MaxSize", "GrowRate" }));
//available_abilities.Add (new Ability ("GrowLeaves", new List<string> () { "MaxSize", "GrowRate", "LeavesDensity" }));
//available_abilities.Add (new Ability ("CollectLight"

