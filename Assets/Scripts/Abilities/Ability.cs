using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    protected string name = "";
    protected Ability script = null;
    protected List<Trait> traits = new List<Trait> ();

    public string Name { get => name; }
    public Ability Script { get => script; }
    public List<Trait> Traits { get => traits; }


    public Ability ( Ability ability_script, List<Trait> traits_of_ability )
    {
        name = this.GetType ().ToString ();
        script = ability_script;
        traits = traits_of_ability;
    }

    public Ability ( string given_name, Ability ability_script, List<string> requested_traits )
    {
        name = given_name;
        script = ability_script;
        traits = traits = GameCore.instance.GameVariables.getTraits (requested_traits);
    }
}
