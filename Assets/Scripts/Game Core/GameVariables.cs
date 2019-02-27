using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameVariables : MonoBehaviour
{
    private double game_ticks = 0;
    private List<Trait> available_traits;
    private List<Ability> available_abilities;

    public List<Trait> AvailableTraits { get => available_traits; }
    public List<Ability> AvailableAbilities { get => available_abilities; }

    public delegate int IntensityCheck ( List<bool> genome_segment );


    public void initialize ()
    {
        // List of all Traits In-Game
        available_traits.Add (new Trait ("MaxSize", 8, IntensityFunctionCollection.MaxSize ));
        available_traits.Add (new Trait ("GrowRate", 10, IntensityFunctionCollection.GrowRate ));
        available_traits.Add (new Trait ("LeavesDensity", 7, IntensityFunctionCollection.LeavesDensity )); //
        available_traits.Add (new Trait ("LightRequirement", 16, IntensityFunctionCollection.LightRequirement ));
        available_traits.Add (new Trait ("OffspringCount", 10, IntensityFunctionCollection.OffspringCount ));

        available_abilities.Add (new Ability ("Grow", new List<string> () { "MaxSize", "GrowRate" }));
        available_abilities.Add (new Ability ("GrowLeaves", new List<string> () { "MaxSize", "GrowRate", "LeavesDensity" }));
        available_abilities.Add (new Ability ("ThrowShadow", new List<string> () { "MaxSize", "LeavesDensity" }));
        available_abilities.Add (new Ability (
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
