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

    // Traits
    TraitField _maxSizeTrait;
    TraitField _growRateTrait;

    // Variables
    private float _size = 1f;
    float _maxSize;
    float _growthPerTick; // means game tick

    public float GrowthPerTick { get => _growthPerTick; }
    public float MaxSize { get => _maxSize; }

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
    override public void initializeAbility ()
    {
        _maxSizeTrait = new TraitField (TraitTypes.MaxSize, this._character);
        _growRateTrait = new TraitField (TraitTypes.GrowRate, this._character);

        //TraitManifestations = character.Genome.getTraitManifestations (new List<string> () { "MaxSize", "GrowRate" });

        // calculate the values that remain fixed
        // MaxSize
        float relative_max_size = _maxSizeTrait.Intensity / _maxSizeTrait.Length;
        _maxSize = Mathf.Pow (2, (float)relative_max_size + 1f); // rises exponentially: 2 to the power of 1f - 2f

        // GrowRate
        // min rate = 400^1 / 400^2 = 0,0025 --> 100 years to full growth | max rate = 400^2 / 400^2 = 1 --> 1 Season to full growth | mean rate = 400^1.5 / 400^2 = 0.05 --> 5 Years to full growth
        float relative_rate_of_growth = _growRateTrait.Intensity / _growRateTrait.Length;
        float normalized_rate_of_growth  =  Mathf.Pow (400f, 1f + (float)relative_rate_of_growth) / (float)(400f*400f);
        _growthPerTick = normalized_rate_of_growth * _maxSize;

        // make babies bigger
        //size = 0.1f * max_size;
        this.transform.localScale = new Vector3 (1 + _size, 1 + _size, 1 + _size); // should min_size be 1 ?
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
        if (_size < _maxSize )
        {
            // sprites are sized 1, 1, 1
            _size = Mathf.Min (_maxSize, _size + _growthPerTick );
            this.transform.localScale = new Vector3 (1 + _size, 1 + _size, 1 + _size ); // scale the GameObject // should min_size be 1 ?
            Debug.Log ("CurrentSize: " + _size);
        }
        
    }

}
