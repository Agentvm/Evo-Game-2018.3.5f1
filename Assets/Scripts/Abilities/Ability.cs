using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected string ability_name = "";
    protected MonoBehaviour script = null;
    protected List<Trait> traits = new List<Trait> ();

    public string Name { get => ability_name; }
    public MonoBehaviour Script { get => script; }
    public List<Trait> Traits { get => traits; }

    void Start ()
    {
        ability_name = "";
        script = null;
        traits = new List<Trait> ();
    }

    /*public Ability (string given_name, MonoBehaviour ability_script, List<Trait> traits_of_ability )
    {
        ability_name = given_name;
        script = ability_script;
        traits = traits_of_ability;
    }*/
}
