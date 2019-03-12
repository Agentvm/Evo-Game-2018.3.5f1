using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/RequireComponent.html
[RequireComponent (typeof (Character))]
public class AbilityBaseClass : MonoBehaviour
{
    //protected string ability_name = "";
    //protected System.Type script_type = null;
    protected Character character;
    //protected Dictionary<string, int> Intensity = new Dictionary<string, int> { };
    protected Dictionary<string, TraitManifestation> TraitManifestation = new Dictionary<string, TraitManifestation> { }; // for trait acess

    public string Name { get => (this.GetType ()).ToString (); }
    //public System.Type ScriptType { get => script_type; }
    //public List<Trait> Traits { get => traits; }

    protected virtual void Start ()
    {
        character = GetComponent<Character> ();
        StartCoroutine (waitForInitialization ());
        //Intensity = character.Genome.getTraitIntensities (new List<string> () { "abc", "def" });
    }

    protected IEnumerator waitForInitialization ()
    {
        yield return new WaitUntil (() => character.Initialized == true);

        // if character has been initialized, get the traits you need
        evaluateIntensity ();
    }

    public virtual void evaluateIntensity ()
    {
        TraitManifestation = character.Genome.getTraitManifestations (new List<string> () { });

        // calculate the values that remain fixed
        // ...
    }

    public virtual void Tick ()
    { }


    /*
    /// <summary>
    /// Constructor
    /// </summary>
    // <param name="ability_name">Must exactly match the Type of the actual Script doing the Computations that gets attached to a sCharacter.</param>
    /// <param name="requested_traits">Traits, that are vital for this ability</param>
    public AbilityBaseClass ( List<string> requested_traits )
    {
        //ability_name = (this.GetType ()).ToString ();
        //if ( System.Type.GetType (ability_name) == null ) Debug.LogError ("No MonoBehaviour Script named " + ability_name + " has been found.");
        //script_type = System.Type.GetType (ability_name);
        traits = GameCore.Instance.GameVariables.getTraits (requested_traits);
    }*/

    //public void addTraits ( Trait new_trait )
    //{
    //    traits.Add (new_trait);
    //}

    //public void addTraits ( List<Trait> new_traits )
    //{
    //    traits.AddRange (new_traits);
    //}
}
