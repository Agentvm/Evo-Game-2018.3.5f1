using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameVariables : MonoBehaviour
{
    private double game_ticks = 0;
    public GameObject map;
    private List<Trait> available_traits = new List<Trait> ();
    private Dictionary<GameObject, List<AbilityBaseClass>> individuals_lexicon = new Dictionary<GameObject, List<AbilityBaseClass>> ();
    //private Dictionary<AbilityBaseClass, List<Trait>> abilities_and_traits = new Dictionary<AbilityBaseClass, List<Trait>>;
    public List<Trait> AvailableTraits { get => available_traits; }
    //public IDictionary<AbilityBaseClass, List<Trait>> AbilitiesAndTraits { get => abilities_and_traits;}

    public void initialize ()
    {
        // add traits
        //available_traits.Add (new Trait ("GrowRate", 8, new Trait.IntensityCheck(IntensityFunctionCollection.GrowRate )));
        available_traits.Add (new Trait ("MaxSize", 8, IntensityFunctionCollection.MaxSize)); // static class
        available_traits.Add (new Trait ("GrowRate", 10, IntensityFunctionCollection.GrowRate));
        available_traits.Add (new Trait ("LeavesDensity", 7, IntensityFunctionCollection.LeavesDensity)); //
        available_traits.Add (new Trait ("LightRequirement", 16, IntensityFunctionCollection.LightRequirement));
        available_traits.Add (new Trait ("OffspringCount", 10, IntensityFunctionCollection.OffspringCount));

        //available_abilities.Add (new Ability ("Grow", new List<string> () { "MaxSize", "GrowRate" }));
        //available_abilities.Add (new Ability ("GrowLeaves", new List<string> () { "MaxSize", "GrowRate", "LeavesDensity" }));
        //available_abilities.Add (new Ability ("CollectLight"

    }

    // find all gameobjects that are individuals capable of evolution, and their abilities
    private void updateIndividuumLexicon ()
    {
        // Find all Gameobjects of Interest
        List<GameObject> all_individuals = new List<GameObject> ();
        all_individuals.AddRange (GameObject.FindGameObjectsWithTag ("Evolving" ));

        foreach (GameObject individuum in all_individuals )
        {
            if ( !individuals_lexicon.ContainsKey (individuum) )
            {
                individuals_lexicon.Add (individuum, new List<AbilityBaseClass> (individuum.GetComponents<AbilityBaseClass> () ));
            }
        }
    }

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
    }

    public void spawnTwoCells ()
    {
        Vector3 spawn_point = RandomPointOnPlane (map );

        // make tehm similar
        Genome genome1 = new Genome ();
        Genome genome2 = genome1;
        genome1.mutate ();
        genome2.mutate ();

        // initialise a basic individual Character
        GameObject character = (GameObject)Instantiate(Resources.Load("Cell" ), spawn_point, new Quaternion (0f, 0f, 0f, 1f ));
        Character character_script_reference = character.GetComponent<Character> ();
        character_script_reference.initialize (genome1, getTraits (new List<string> () { "GrowRate", "MaxSize", "OffspringCount"/*, "MaxAge" */}));
        individuals_lexicon.Add (character, new List<AbilityBaseClass> (character.GetComponents<AbilityBaseClass> ()));

        character = (GameObject)Instantiate(Resources.Load("Cell" ), spawn_point + new Vector3 (Random.value * 5f, Random.value * 5f, 0f), new Quaternion (0f, 0f, 0f, 1f ));
        character_script_reference = character.GetComponent<Character> ();
        character_script_reference.initialize (genome2, getTraits (new List<string> () { "GrowRate", "MaxSize", "OffspringCount"/*, "MaxAge" */}));
        individuals_lexicon.Add (character, new List<AbilityBaseClass> (character.GetComponents<AbilityBaseClass> ()));

        //List<Character> adams_and_eves = new List<Character> ();
        //foreach ( GameObject obj in GameObject.FindGameObjectsWithTag ("Origin") ) // ToDo: don't use Origin Tag, use Instantiate
        //{
        //    if ( obj.GetComponent<Character> () == null )
        //        obj.AddComponent<Character> ();
        //    Character character_script = obj.GetComponent<Character> ();
        //    character_script.initialize (new Genome (), getTraits (new List<string> () { "GrowRate", "MaxSize", "OffspringCount"/*, "MaxAge" */} ));
        //    adams_and_eves.Add (character_script);
        //}
    }

    public Vector3 RandomPointOnPlane (GameObject plane_with_mesh_collider )
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

    /*{
        genome = mutated_parent_genome;
        if (new_traits != null)
        {
            foreach (Trait new_trait in new_traits)
                Genome.addTrait(new_trait); // random trait positions in genome
        }

        genome.updateIntensities(); // check if Traits are currently active
        
        foreach (Trait trait in genome.Traits )
        {



            // Add Behaviour Script to gameobject
            //System.Type ability_type = System.Type.GetType(trait.Name );
            //gameObject.AddComponent (ability_type);
            // good to know
            // ((MySpellScript)GetComponent(mType)).Fire();
        }

    }*/

    //void print_list (List<bool> list)
    //{
    //    print ("List of Length " + list.Count);
    //    foreach ( bool b in list )
    //    {
    //        if ( b ) print ("True");
    //        else print ("False");
    //    }
    //}

    public List<Trait> getTraits ( List<string> requested_traits )
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

            if ( !found ) Debug.LogWarning ("The Trait " + requested_name + " could not be found.");

        }
        return returned_traits;
    }

}
