﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameVariables : MonoBehaviour
{
    private double game_ticks = 0;
    private List<Trait> available_traits = new List<Trait> ();
    private IDictionary<AbilityBaseClass, List<Trait>> abilities_and_traits; // !! this is not instantiated
    

    public List<Trait> AvailableTraits { get => available_traits; }
    //public IDictionary<AbilityBaseClass, List<Trait>> AbilitiesAndTraits { get => abilities_and_traits;}
    
    public void initialize ()
    {
        // add traits
        //available_traits.Add (new Trait ("GrowRate", 8, new Trait.IntensityCheck(IntensityFunctionCollection.GrowRate )));
        available_traits.Add (new Trait ("MaxSize", 8, IntensityFunctionCollection.MaxSize)); // static class
        available_traits.Add (new Trait ("GrowRate", 10, IntensityFunctionCollection.GrowRate ));
        available_traits.Add (new Trait ("LeavesDensity", 7, IntensityFunctionCollection.LeavesDensity)); //
        available_traits.Add (new Trait ("LightRequirement", 16, IntensityFunctionCollection.LightRequirement));
        available_traits.Add (new Trait ("OffspringCount", 10, IntensityFunctionCollection.OffspringCount));

        //available_abilities.Add (new Ability ("Grow", new List<string> () { "MaxSize", "GrowRate" }));
        //available_abilities.Add (new Ability ("GrowLeaves", new List<string> () { "MaxSize", "GrowRate", "LeavesDensity" }));
        //available_abilities.Add (new Ability ("CollectLight"

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("Origin"))
        {
            // initialise the first few individual Characters
        }

    }

    //void print_list (List<bool> list)
    //{
    //    print ("List of Length " + list.Count);
    //    foreach ( bool b in list )
    //    {
    //        if ( b ) print ("True");
    //        else print ("False");
    //    }
    //}

    public List<Trait> getTraits ( List<string> requested_traits )
    {
        List<Trait> returned_traits = new List<Trait> ();

        foreach (string requested_name in requested_traits)
        {
            bool found = false;
            foreach (Trait trait in available_traits)
            {
                if (trait.Name == requested_name)
                {
                    returned_traits.Add (trait);
                    found = true;
                    break; // the inner foreach
                }
            }

            if ( !found ) Debug.LogWarning ("The Trait " + requested_name + " could not be found.");

        }
        return returned_traits;
    }

}
