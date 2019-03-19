using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AbilityTypes { Grow };

// It takes care of the initialization of the basic data structures (Traits that Characters have or develop).
public static class AbilityData
{
    //private List<Trait> available_traits = new List<Trait> ();
    private static System.Type[] available_ability_types = new System.Type[1];//new List<Trait> ();

    // Properties
    //public List<Trait> AvailableTraits { get => available_traits; }

    static AbilityData ()
    {

        // add Abilities
        available_ability_types[(int)AbilityTypes.Grow] = typeof (Grow);

        /* ToDo: implement Ability checks
        // check that every entry of enum has been set
        for ( int i = 0; i < TraitType.GetNames (typeof (TraitType)).Length; i++ )
        {
            if ( available_traits[i] == null ) Debug.LogError ("The Trait " + ((TraitType)i).ToString () + " at position available_traits[" + i + "] has not been set correctly.");
        }

        // check that enum and array match each other
        for ( int i = 0; i < available_traits.Length; i++ )
        {
            if ( available_traits[i].Name != ((TraitType)i).ToString () )
            {
                Debug.LogError ("Initialization of Traits failed. available_traits[" + i + "].Name = " + available_traits[i].Name + ", when it should be " + ((TraitType)i).ToString ());
            }
        }*/

    }

    
    public static System.Type getAbilityType ( AbilityTypes ability_type )
    {
        return available_ability_types[(int)ability_type];
    }
    
}

//available_abilities.Add (new Ability ("Grow", new List<string> () { "MaxSize", "GrowRate" }));
//available_abilities.Add (new Ability ("GrowLeaves", new List<string> () { "MaxSize", "GrowRate", "LeavesDensity" }));
//available_abilities.Add (new Ability ("CollectLight"

