using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameVariables : MonoBehaviour
{
    // Variables
    private double current_game_ticks = 0; // counts the game ticks since start. Each tick currently means that a long time period elapses (maybe a season, or half a year)
    [SerializeField]private GameObject map; // The plane where the game takes place
    // used to iterate over all abilities (maybe make this a list?)
    private Dictionary<GameObject, List<AbilityBaseClass>> individuals_lexicon = new Dictionary<GameObject, List<AbilityBaseClass>> ();



    /// <summary>
    /// This function is called when game starts.
    /// </summary>
    public void initialize ()
    {
        
    }

    /// <summary>
    /// Find all gameobjects that are individuals capable of evolution, and their abilities. (Maybe make Characters register themselves instead?)
    /// </summary>
    private void updateIndividuumLexicon ()
    {
        // Find all Gameobjects of Interest
        List<GameObject> all_individuals = new List<GameObject> ();
        all_individuals.AddRange (GameObject.FindGameObjectsWithTag ("Evolving" ));

        foreach (GameObject individuum in all_individuals )
        {
            // if not already in lexicon, add. (Characters only get new abilities when they are instantiated / born)
            if ( !individuals_lexicon.ContainsKey (individuum) )
            {
                individuals_lexicon.Add (individuum, new List<AbilityBaseClass> (individuum.GetComponents<AbilityBaseClass> () ));
            }
        }
    }

    /// <summary>
    /// Calls the function Tick () on all ability scripts. Every derivative class of AbilityBaseClass implements Tick () to make changes to the game.
    /// </summary>
    public void gameTick ( int times = 1 )
    {
        updateIndividuumLexicon ();
        Debug.Log ("The Lexicon has " + individuals_lexicon.Count + " entries.");

        for ( int a = 0; a < times; a++ )
        {
            // Iterate over all values of the lexicon (all abilities) and call tick, executing whatever the ability does, once.
            foreach ( List<AbilityBaseClass> abilities in individuals_lexicon.Values )
                foreach ( AbilityBaseClass ability in abilities )
                    ability.Tick ();
        }

        current_game_ticks++;
    }

    /// <summary>
    /// Instantiates two prefabs with the script Character and some Ability scripts attached. The genomes of those two closely resemble
    /// eachother, so that they can reproduce. They are placed near to eachother.
    /// </summary>
    public void spawnTwoCells ()
    {
        Vector3 spawn_point = RandomPointOnPlane (map );

        // make them similar
        Genome genome1 = new Genome ();
        Genome genome2 = genome1;
        genome1.mutate ();
        genome2.mutate ();

        // initialise a basic individual Character
        GameObject character = (GameObject)Instantiate(Resources.Load("Cell" ), spawn_point, new Quaternion (0f, 0f, 0f, 1f ));
        Character character_script_reference = character.GetComponent<Character> ();
        character_script_reference.initialize (genome1,
            new List<TraitTypes> () { TraitTypes.GrowRate,TraitTypes.MaxSize, TraitTypes.OffspringCount },
            new List<AbilityTypes> () { AbilityTypes.Grow });
        individuals_lexicon.Add (character, new List<AbilityBaseClass> (character.GetComponents<AbilityBaseClass> ()));

        character = (GameObject)Instantiate(Resources.Load("Cell" ), spawn_point + new Vector3 (Random.value * 5f, Random.value * 5f, 0f), new Quaternion (0f, 0f, 0f, 1f ));
        character_script_reference = character.GetComponent<Character> ();
        character_script_reference.initialize (genome2,
            new List<TraitTypes> () { TraitTypes.GrowRate, TraitTypes.MaxSize, TraitTypes.OffspringCount },
            new List<AbilityTypes> () { AbilityTypes.Grow });
        individuals_lexicon.Add (character, new List<AbilityBaseClass> (character.GetComponents<AbilityBaseClass> ()));

    }

    /// <summary>
    /// Gives a point on a plane that extends in x/y - direction (red/green axis in unity)
    /// </summary>
    private Vector3 RandomPointOnPlane (GameObject plane_with_mesh_collider )
    {
        // get mesh bounds
        Mesh planeMesh = plane_with_mesh_collider.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;
        bounds.size *= 0.95f; // clear borders

        // compute x and y bounds in world space
        float minX = plane_with_mesh_collider.transform.position.x - plane_with_mesh_collider.transform.localScale.x * bounds.size.x * 0.5f;
        float minY = plane_with_mesh_collider.transform.position.z - plane_with_mesh_collider.transform.localScale.z * bounds.size.z * 0.5f;

        // make a new vector on a plane which lies in x and y direction (this would be nice if things did clip to the plane)
        Vector3 newVec = new Vector3(Random.Range (minX, -minX),
                                    Random.Range (minY, -minY),
                                    plane_with_mesh_collider.transform.position.y - 0.2f
                                    );

        return newVec;
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
