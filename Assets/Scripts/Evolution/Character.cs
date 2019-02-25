using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for animals, plants, etc that are subject to evolution
public class Character : MonoBehaviour {

    private Genome genome;

    // Properties
    public Genome Genome
    {
        get { return genome; }
        //set { genome = value; }
    }

    public Character ( Genome mutated_parent_genome, List<Trait> new_traits = null )
    {
        genome = mutated_parent_genome;
        if (new_traits != null)
        {
            foreach (Trait new_trait in new_traits)
                Genome.addTrait (new_trait); // random trait positions in genome
        }
        
        genome.updateIntensities (); // check if Traits are currently active
        
        foreach ( Trait trait in genome.Traits )
        {



            // Add Behaviour Script to gameobject
            //System.Type ability_type = System.Type.GetType(trait.Name );
            //gameObject.AddComponent (ability_type);
            // good to know
            // ((MySpellScript)GetComponent(mType)).Fire();
        }

    }
}
