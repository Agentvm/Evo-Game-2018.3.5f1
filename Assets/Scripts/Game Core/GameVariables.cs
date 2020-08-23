using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameVariables : MonoBehaviour
{
    // Variables
    private double _currentGameTicks = 0; // counts the game ticks since start. Each tick currently means that a long time period elapses (maybe a season, or half a year)
    [SerializeField]private GameObject _map; // The plane where the game takes place
    // used to iterate over all abilities (maybe make this a list?)
    private Dictionary<GameObject, List<AbilityBaseClass>> _individualsLexicon = new Dictionary<GameObject, List<AbilityBaseClass>> ();

    public Dictionary<GameObject, List<AbilityBaseClass>> IndividualsLexicon { get => _individualsLexicon;}
    public double CurrentGameTicks { get => _currentGameTicks; }
    public GameObject Map { get => _map; }



    /// <summary>
    /// This function is called when game starts.
    /// </summary>
    public void Initialize ()
    {
        
    }

    public void IncreaseTicks ()
    {
        _currentGameTicks++;
    }

    /// <summary>
    /// Find all gameobjects that are individuals capable of evolution, and their abilities. (Maybe make Characters register themselves instead?)
    /// </summary>
    public void UpdateIndividuumLexicon ()
    {
        // Find all Gameobjects of Interest
        List<GameObject> all_individuals = new List<GameObject> ();
        all_individuals.AddRange (GameObject.FindGameObjectsWithTag ("Evolving"));

        foreach ( GameObject individuum in all_individuals )
        {
            // if not already in lexicon, add. (Characters only get new abilities when they are instantiated / born)
            if ( !_individualsLexicon.ContainsKey (individuum) )
            {
                _individualsLexicon.Add (individuum, new List<AbilityBaseClass> (individuum.GetComponents<AbilityBaseClass> ()));
            }
        }
    }


    private void PrintDict<T> (Dictionary <T, T> dict)
    {
        foreach ( var key in dict.Keys )
            Debug.Log (key + ", " + dict[key]);
    }

    /*
    /// <summary>
    /// Parses strings to traits. Use like so: list = getTraits (new List<string> () {"Trait1", "Trait2"})
    /// </summary>
    private List<Trait> getTraits ( List<string> requested_traits )
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

            if ( !found ) Debug.LogWarning ("The Trait " + requested_name + " could not be found."); // this does never fire. Debug please

        }
        return returned_traits;
    }*/

}
