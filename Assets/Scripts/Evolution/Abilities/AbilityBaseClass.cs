﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Base class for Character Abilities.
 * Abilities are attached to a GameObject (an Individuum, e.g. an animal or plant) to execute all stuff directly related to changes in game.
 * Examples of this are Eating, Growing, Mating.
 * 
 */
 // https://docs.unity3d.com/ScriptReference/RequireComponent.html
[RequireComponent (typeof (Character))]
public class AbilityBaseClass : MonoBehaviour
{
    // Variables
    protected Character character; // reference to the Character script attached to the same GameObject
    //protected Dictionary<string, TraitManifestation> TraitManifestations = new Dictionary<string, TraitManifestation> { }; // for Trait access
     

    // Properties
    public string Name { get => (this.GetType ()).ToString (); }


    protected /*virtual*/ void Start ()
    {
        character = GetComponent<Character> (); // get reference
        StartCoroutine (waitForInitialization ()); // Start a waiting coroutine
    }


    /// <summary>
    /// Waits until the character that this Ability belongs to has initialized all his Traits.
    /// </summary>
    protected IEnumerator waitForInitialization ()
    {
        yield return new WaitUntil (() => character.TraitsInitialized == true);

        // if character has been initialized, get the traits you need
        evaluateIntensity ();
    }


    /// <summary>
    /// Initialization method of this Class (Because Constructors don't work for MonoBehaviours). Retrieves all Traits this Ability is based on, then calculates all constant values that follow of these.
    /// </summary>
    public virtual void evaluateIntensity ()
    {
        //TraitManifestations = character.Genome.getTraitManifestations (new List<string> () { });

        // calculate the values that remain fixed
        // ...
    }


    /// <summary>
    /// Implements actions that are executed each game step and change this Individuum (GameObject) or other Individuals
    /// </summary>
    public virtual void Tick ()
    { }

}
