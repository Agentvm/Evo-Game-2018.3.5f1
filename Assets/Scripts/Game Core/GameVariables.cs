using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameVariables : MonoBehaviour
{
    private double game_ticks = 0;
    private List<Trait> available_traits;
    private IDictionary<AbilityBaseClass, List<Trait>> abilities_and_traits;
    private IntensityFunctionCollection intensity_function_collection;

    public List<Trait> AvailableTraits { get => available_traits; }
    public IDictionary<AbilityBaseClass, List<Trait>> AbilitiesAndTraits { get => abilities_and_traits;}
    public IntensityFunctionCollection Intensity_function_collection { get => intensity_function_collection;}
    

    public delegate int IntensityCheck ( List<bool> genome_segment );
    public IntensityCheck IntensityStatus;

    public void initialize ()
    {
        intensity_function_collection = new IntensityFunctionCollection ();

        // List of all Traits In-Game

        // static works here!
        Debug.Log (GameCore.roll_dice () );

        //1. Solution: Delegates
        IntensityStatus += intensity_function_collection.MaxSize;

        //2. Solution: Normal Class & instantiate
        intensity_function_collection = new IntensityFunctionCollection ();

        //3. Find a way to use the same delegate blueprint in GameVariables and Trait

        //available_traits.Add (new Trait ("MaxSize", 8, intensity_function_collection.MaxSize)); // Instance
        //available_traits.Add (new Trait ("MaxSize", 8, IntensityStatus.Method)); // Delegate
        available_traits.Add (new Trait ("GrowRate", 8, IntensityFunctionCollection.GrowRate)); // static class


        //available_traits.Add (new Trait ("GrowRate", 10, IntensityFunctionCollection.GrowRate ));
        //available_traits.Add (new Trait ("LeavesDensity", 7, IntensityFunctionCollection.LeavesDensity )); //
        //available_traits.Add (new Trait ("LightRequirement", 16, IntensityFunctionCollection.LightRequirement ));
        //available_traits.Add (new Trait ("OffspringCount", 10, IntensityFunctionCollection.OffspringCount ));

        //available_abilities.Add (new Ability ("Grow", new List<string> () { "MaxSize", "GrowRate" }));
        //available_abilities.Add (new Ability ("GrowLeaves", new List<string> () { "MaxSize", "GrowRate", "LeavesDensity" }));
        //available_abilities.Add (new Ability ("CollectLight"

        
    }

    void print_list (List<bool> list)
    {
        print ("List of Length " + list.Count);
        foreach ( bool b in list )
        {
            if ( b ) print ("True");
            else print ("False");
        }
    }

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
