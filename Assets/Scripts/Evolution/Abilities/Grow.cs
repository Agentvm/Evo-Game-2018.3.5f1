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

    // References
    GrowLeaves _canopyReference = null;

    // Traits
    TraitField _maxSizeTrait;
    TraitField _growRateTrait;
    TraitField _lightRequirementTrait;

    // Variables
    private float _size = 1f;
    float _maxSize;
    float _growthPerTick; // means game tick
    private float _lightRequirement;

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
    override public void InitializeAbility ()
    {
        base.InitializeAbility ();

        // Get Traits
        _maxSizeTrait = new TraitField (TraitTypes.MaxSize, this._character);
        _growRateTrait = new TraitField (TraitTypes.GrowRate, this._character);
        _lightRequirementTrait = new TraitField (TraitTypes.LightRequirement, this._character);

        _canopyReference = this.GetComponent<GrowLeaves> ();

        // Calculate the values that remain fixed
        // MaxSize
        float relative_max_size = _maxSizeTrait.Intensity / _maxSizeTrait.MaxIntensity;
        _maxSize = Mathf.Pow (2, (float)relative_max_size + 1f); // rises exponentially: 2 to the power of 1f - 2f

        // GrowRate
        // min rate = 400^1 / 400^2 = 0,0025 --> 100 years to full growth | max rate = 400^2 / 400^2 = 1 --> 1 Season to full growth | mean rate = 400^1.5 / 400^2 = 0.05 --> 5 Years to full growth
        float relative_rate_of_growth = _growRateTrait.Intensity / _growRateTrait.MaxIntensity;
        float normalized_rate_of_growth  =  Mathf.Pow (400f, 1f + (float)relative_rate_of_growth) / (float)(400f*400f);
        _growthPerTick = normalized_rate_of_growth * _maxSize;

        // LightRequirement
        _lightRequirement = _lightRequirementTrait.Intensity / _lightRequirementTrait.MaxIntensity;
        _lightRequirement *= _growthPerTick;

        // Scale GameObject initially
        SetSize ();
    }


    /// <summary>
    /// Make Changes to the game. One Tick is one unit of time, let's say one season.
    /// </summary>
    override public void Tick ()
    {
        if ( _size < _maxSize && _character.SubtractEnergy (_lightRequirement) )
        {
            // Change the size in script and for the GameObject
            _size = Mathf.Min (_maxSize, _size + _growthPerTick);
            SetSize ();
        }
    }


    // Scale the GameObject to the current size, if size is not set according to GrowLeaves.CurrentLeavesArea
    void SetSize ()
    {
        if ( _canopyReference == null )
            _character?.SetSize (_size);
        else
            _character?.SetElevation (_size);
    }


    // helper function that does not really belong here
    private string PrintGenome ( Genome genome )
    {
        string str = "";

        foreach ( bool b in genome.GenomeString )
        {
            if ( b ) str += "1";
            else str += 0;
        }

        return str;
    }

}
