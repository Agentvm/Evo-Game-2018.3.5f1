using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    protected string name = "";
    protected System.Type script_type = null;
    protected List<Trait> traits = new List<Trait> ();

    public string Name { get => name; }
    public System.Type ScriptType { get => script_type; }
    public List<Trait> Traits { get => traits; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="ability_name">Must exactly match the Type of the actual Script doing the Computations that gets attached to a sCharacter.</param>
    /// <param name="requested_traits">Traits, that are vital for this ability</param>
    public Ability ( string ability_name, List<string> requested_traits )
    {
        name = ability_name;
        if ( System.Type.GetType (ability_name) == null ) Debug.LogError ("No MonoBehaviour Script named " + ability_name + " has been found.");
        script_type = System.Type.GetType (ability_name);
        traits = traits = GameCore.Instance.GameVariables.getTraits (requested_traits);
    }
}
