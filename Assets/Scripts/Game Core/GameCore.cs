using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{

    public static GameCore Instance = null; // Static instance of GameManager which allows it to be accessed by any other script.
    private GameVariables _gameVariables;   // Store a reference to our game variables.

    public GameVariables GameVariables
    {
        get { return _gameVariables; }
    }

    // Awake is always called before any Start functions
    void Awake ()
    {
        // check there is only one instance of this and that it is not destroyed on load
        if ( Instance == null )
            Instance = this;
        else if ( Instance != this )
            Destroy (gameObject);
        DontDestroyOnLoad (gameObject);

        // Initialize game variables
        _gameVariables = this.GetComponent<GameVariables> ();
        _gameVariables.Initialize ();
    }

    /// <summary>
    /// Helper Function for random dice events
    /// </summary>
    static public int roll_dice ( int times = 1, int faces = 100 )
    {
        int result = 0;

        for (int i = 0; i < times; i++ )
        {
            result += (int)Mathf.Round (Random.Range (1, faces));
        }

        return result;
    }

    /// <summary>
    /// Calls the function Tick () on all ability scripts. Every derivative class of AbilityBaseClass implements Tick () to make changes to the game.
    /// </summary>
    public void gameTick ( int times = 1 )
    {
        _gameVariables.UpdateIndividuumLexicon ();
        //Debug.Log ("The Lexicon has " + individuals_lexicon.Count + " entries.");

        for ( int a = 0; a < times; a++ )
        {
            // Iterate over all values of the lexicon (all abilities) and call tick, executing whatever the ability does, once.
            foreach ( List<AbilityBaseClass> abilities in _gameVariables.IndividualsLexicon.Values )
                foreach ( AbilityBaseClass ability in abilities )
                    ability.Tick ();
        }

        _gameVariables.IncreaseTicks();
    }

    /// <summary>
    /// Instantiates two prefabs with the script Character and some Ability scripts attached. The genomes of those two closely resemble
    /// eachother, so that they can reproduce. They are placed near to eachother.
    /// </summary>
    public void spawnTwoCells ()
    {
        Vector3 spawn_point = RandomPointOnPlane (_gameVariables.Map );

        // make them similar
        Genome genome1 = new Genome ();
        Genome genome2 = genome1;
        genome1.mutate ();
        genome2.mutate ();

        // initialise a basic individual Character
        GameObject character = (GameObject)Instantiate(Resources.Load("Cell" ), spawn_point, new Quaternion (0f, 0f, 0f, 1f ));
        Character character_script_reference = character.GetComponent<Character> ();
        character_script_reference.Birth (genome1,
            new List<TraitTypes> () { TraitTypes.GrowRate, TraitTypes.MaxSize, TraitTypes.OffspringCount, TraitTypes.LightRequirement, TraitTypes.LeavesDensity },
            new List<AbilityTypes> () { AbilityTypes.Grow, AbilityTypes.CollectSunlight, AbilityTypes.GrowLeaves });
        _gameVariables.IndividualsLexicon.Add (character, new List<AbilityBaseClass> (character.GetComponents<AbilityBaseClass> ()));

        character = (GameObject)Instantiate (Resources.Load ("Cell"), spawn_point + new Vector3 (Random.value * 5f, Random.value * 5f, 0f), new Quaternion (0f, 0f, 0f, 1f));
        character_script_reference = character.GetComponent<Character> ();
        character_script_reference.Birth (genome2,
            new List<TraitTypes> () { TraitTypes.GrowRate, TraitTypes.MaxSize, TraitTypes.OffspringCount, TraitTypes.LightRequirement, TraitTypes.LeavesDensity },
            new List<AbilityTypes> () { AbilityTypes.Grow, AbilityTypes.CollectSunlight, AbilityTypes.GrowLeaves });
        _gameVariables.IndividualsLexicon.Add (character, new List<AbilityBaseClass> (character.GetComponents<AbilityBaseClass> ()));

    }

    /// <summary>
    /// Gives a point on a plane that extends in x/y - direction (red/green axis in unity)
    /// </summary>
    private Vector3 RandomPointOnPlane ( GameObject plane_with_mesh_collider )
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

}
