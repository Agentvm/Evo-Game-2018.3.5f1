using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Character))]
public class Grow : AbilityBaseClass
{
    // Inherits:
    /*
    // Variables
    protected Character character; // reference to the Character script attached to the same GameObject
    protected Dictionary<string, TraitManifestation> TraitManifestation = new Dictionary<string, TraitManifestation> { }; // for Trait access

    // Properties
    public string Name { get => (this.GetType ()).ToString (); }
    */

    // Variables
    private float size = 1f;
    float max_size;
    float growth_per_tick; // means game tick

    /*
    // Start is called before the first frame update
    override protected void Start ()
    {
        // this gets character reference and starts coroutine that calls evaluateIntensity as soon as character is fully initialized (all traits set)
        base.Start ();
    }*/


    /// <summary>
    /// Is called as soon as character.TraitsInitialized is set to True.
    /// Retrieves all Traits this Ability is based on, as well as their Intensities (which will not change), then calculates all constant values that follow of these.
    /// </summary>
    override public void evaluateIntensity ()
    {
        //TraitManifestations = character.Genome.getTraitManifestations (new List<string> () { "MaxSize", "GrowRate" });

        // get Intensities and lenghts
        float max_size_intensity = this.character.Genome.GetManifestation (TraitTypes.MaxSize ).Intensity;
        float max_size_length = this.character.Genome.GetManifestation (TraitTypes.MaxSize ).Length;
        float grow_rate_intensity = this.character.Genome.GetManifestation (TraitTypes.GrowRate ).Intensity;
        float grow_rate_length = this.character.Genome.GetManifestation (TraitTypes.GrowRate ).Length;

        // calculate the values that remain fixed
        // MaxSize
        float relative_max_size = max_size_intensity / max_size_length;
        max_size = Mathf.Pow (2, (float)relative_max_size + 1f); // rises exponentially: 2 to the power of 1f - 2f

        // GrowRate
        float relative_rate_of_growth = grow_rate_intensity / grow_rate_length;
        // min rate = 400^1 / 400^2 = 0,0025 --> 100 years to full growth | max rate = 400^2 / 400^2 = 1 --> 1 Season to full growth | mean rate = 400^1.5 / 400^2 = 0.05 --> 5 Years to full growth
        float normalized_rate_of_growth  =  Mathf.Pow (400f, 1f + (float)relative_rate_of_growth) / (float)(400f*400f);
        growth_per_tick = normalized_rate_of_growth * max_size;

        Debug.Log ("Genome = " + printGenome (character.Genome ));
        Debug.Log ("max_size_intensity = " + max_size_intensity );
        Debug.Log ("grow_rate_intensity = " + grow_rate_intensity );

        // make babies bigger
        //size = 0.1f * max_size;
        this.transform.localScale = new Vector3 (1 + size, 1 + size, 1 + size); // should min_size be 1 ?
    }


    // helper function that does not really belong here
    private string printGenome (Genome genome )
    {
        string str = "";

        foreach (bool b in genome.GenomeString)
        {
            if ( b ) str += "1";
            else str += 0;
        }

        return str;
    }


    /// <summary>
    /// Make Changes to the game. One Tick is one unit of time, let's say one season.
    /// </summary>
    override public void Tick ()
    {
        if (size < max_size )
        {
            // sprites are sized 1, 1, 1
            size = Mathf.Min (max_size, size + growth_per_tick );
            this.transform.localScale = new Vector3 (1 + size, 1 + size, 1 + size ); // scale the GameObject // should min_size be 1 ?
        }
        
    }

}
