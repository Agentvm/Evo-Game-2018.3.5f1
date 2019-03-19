﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitTypes { MaxSize, GrowRate, LeavesDensity, LightRequirement, OffspringCount };

// It takes care of the initialization of the basic data structures (Traits that Characters have or develop).
public static class TraitData
{
    //private List<Trait> available_traits = new List<Trait> ();
    private static Trait[] available_traits = new Trait[5];//new List<Trait> ();

    // Properties
    //public List<Trait> AvailableTraits { get => available_traits; }

    static TraitData ()
    {
        // add Traits, take functions of static class IntensityFunctionCollection to assign Trait IntensityFunction delegates
        // Determines Maximum Size of a Character (e.g. Creature or Plant)
        available_traits[(int)TraitTypes.MaxSize] = new Trait (nameof (TraitTypes.MaxSize), 8, IntensityFunctionCollection.CountTrueValues);
        // Determines rate of growth
        available_traits[(int)TraitTypes.GrowRate] = new Trait (nameof (TraitTypes.GrowRate), 10, IntensityFunctionCollection.CountTrueValues);
        // Determines how much leaves are produced (+ how much light can be consumed and blocked).
        available_traits[(int)TraitTypes.LeavesDensity] = new Trait (nameof (TraitTypes.LeavesDensity), 7, IntensityFunctionCollection.CountTrueValues);
        // Determines how much light is needed to stay alive and grow (overlaps heavily with growing-related Traits, genome-wise.)
        available_traits[(int)TraitTypes.LightRequirement] = new Trait (nameof (TraitTypes.LightRequirement), 16, IntensityFunctionCollection.CountTrueValues);
        // Basic value for determination of kids or saplings count
        available_traits[(int)TraitTypes.OffspringCount] = new Trait (nameof (TraitTypes.OffspringCount), 10, IntensityFunctionCollection.CountTrueValues);

        // check that every entry of enum has been set
        for ( int i = 0; i < TraitTypes.GetNames (typeof (TraitTypes)).Length; i++ )
        {
            if ( available_traits[i] == null ) Debug.LogError ("The Trait " + ((TraitTypes)i).ToString () + " at position available_traits[" + i + "] has not been set correctly.");
        }

        // check that enum and array match each other
        for ( int i = 0; i < available_traits.Length; i++ )
        {
            if ( available_traits[i].Name != ((TraitTypes)i).ToString () )
            {
                Debug.LogError ("Initialization of Traits failed. available_traits[" + i + "].Name = " + available_traits[i].Name + ", when it should be " + ((TraitTypes)i).ToString ());
            }
        }

        /* deprecated list
        // add Traits, take functions of static class IntensityFunctionCollection to assign Trait IntensityFunction delegates
        // Determines Maximum Size of a Character (e.g. Creature or Plant)
        available_traits.Add (new Trait (nameof (TraitType.MaxSize ), 8, IntensityFunctionCollection.CountTrueValues ));
        // Determines rate of growth
        available_traits.Add (new Trait (nameof (TraitType.GrowRate ), 10, IntensityFunctionCollection.CountTrueValues));
        // Determines how much leaves are produced (+ how much light can be consumed and blocked).
        available_traits.Add (new Trait (nameof (TraitType.LeavesDensity ), 7, IntensityFunctionCollection.CountTrueValues));
        // Determines how much light is needed to stay alive and grow (overlaps heavily with growing-related Traits, genome-wise.)
        available_traits.Add (new Trait (nameof (TraitType.LightRequirement ), 16, IntensityFunctionCollection.CountTrueValues));
        // Basic value for determination of kids or saplings count
        available_traits.Add (new Trait (nameof (TraitType.OffspringCount ), 10, IntensityFunctionCollection.CountTrueValues));
        */
    }

    public static Trait getTrait (TraitTypes trait_type )
    {
        return available_traits[(int)trait_type];
    }

}

//available_abilities.Add (new Ability ("Grow", new List<string> () { "MaxSize", "GrowRate" }));
//available_abilities.Add (new Ability ("GrowLeaves", new List<string> () { "MaxSize", "GrowRate", "LeavesDensity" }));
//available_abilities.Add (new Ability ("CollectLight"
