using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Class that contains "genetic" information
//                   ________________________________________________
// Genome String     |1010100010101011010100110100100000100001000010|
// Genes 1, 0
//
// Genome.Traits > TraitManifestation > ManifestationSegment
public class Genome {
    
    // Variables
    private List<bool> genome_string;
    private List<TraitManifestation> trait_manifestations = new List<TraitManifestation> ();
    //private List<int> past_mutations = new List<int> (); // rather fits in species
    private float mutation_scale = 1.0f; // how likely is this genome altered when mutating

    
    // Properties
    public List<bool> GenomeString { get => genome_string;/* set => genome_string = value;*/ }
    public int Length {get => GenomeString.Count;}
    //public List<Trait> Traits { get => getTraits (); } // currently not necessary
    //public List<int> PastMutations { get => past_mutations;/* set => genome_string = value;*/ }
    public float MutationScale { get => mutation_scale; set => mutation_scale = value; }


    /// <summary>
    /// Constructor for a randomized genome.
    /// </summary>
    public Genome (int length = 30)
    {
        // Fill Genome with random values
        genome_string = new List<bool> (30);
        for ( int i = 0; i < length; i++ ) genome_string.Add (Random.value > 0.5f);
    }    
    
    
    // Functions

    /// <summary>
    /// Update the Intensity of every Trait in this genome (to see if, and to which degree it is active)
    /// </summary>
    public void updateIntensities ()
    {
        foreach ( TraitManifestation manifestation in trait_manifestations )
        {
            manifestation.updateIntensityStatus (genome_string);
        }
    }

    
    /// <summary>
    /// Alters the genome_string. This is set to occur before birth. 
    /// </summary>
    public void mutate ()
    {
        for (int i = 0; i < genome_string.Count; i++)
        {
            // Flipping bit with Chance of ((1/GenomeLength) * MutationScale) | (Formula is inverted)
            if (Random.Range(1, Mathf.Ceil (genome_string.Count * (1/MutationScale))) == 1 )
            {
                genome_string[i] = !genome_string[i];
                //if (!PastMutations.Contains (i))
                //    PastMutations.Add (i);
            }
        }
    }
    

    /// <summary>
    /// Add a Trait to this Genome.
    /// </summary>
    public void manifestTrait ( Trait trait )
    {
        // if trait is already in this genome, abort
        foreach (TraitManifestation manifestation in trait_manifestations) if (manifestation.Name == trait.Name) return;
        
        // the trait is added at a random position in the genome
        int pos = Random.Range (0, GenomeString.Count);
        trait_manifestations.Add (new TraitManifestation (trait, pos, genome_string) );
    }


    /// <summary>
    /// Add a Trait to this Genome at a certain position.
    /// </summary>
    public void addTrait ( Trait trait, int position_in_genome )
    {
        // if trait is already in this genome, abort
        foreach ( TraitManifestation manifestation in trait_manifestations ) if ( manifestation.Name == trait.Name ) return;

        trait_manifestations.Add (new TraitManifestation (trait, position_in_genome, genome_string ));
    }

    public TraitManifestation GetManifestation (TraitTypes requested_trait_type)
    {
        bool found = false;
        foreach ( TraitManifestation manifestation in this.trait_manifestations )
        {
            // TraitManifestations obviously have the same name as the Trait they correspond to
            if ( requested_trait_type.ToString () == manifestation.Name )
                return manifestation;
        }

        if ( !found ) Debug.LogWarning ("The Trait " + requested_trait_type.ToString () + " is not manifested in " + this.GetType ().ToString () + ".");
        return null;
    }

    /*
    /// <summary>
    /// Returns a Dictionary of Names and respective TraitManifestations. With these, all trait Attributes are accessible (Intensity, Lenght and Name of the Trait)
    /// </summary>
    public Dictionary<string, TraitManifestation> getTraitManifestations ( List<Trait> requested_traits )
    {
        Dictionary<string, TraitManifestation> trait_names_and_manifestations = new Dictionary<string, TraitManifestation> { };

        foreach ( Trait requested_trait in requested_traits )
        {
            bool found = false;
            foreach ( TraitManifestation manifestation in trait_manifestations )
            {
                if ( manifestation.Name == requested_trait.Name )
                {
                    trait_names_and_manifestations.Add (manifestation.Name, manifestation);
                    found = true;
                    break; // the inner foreach
                }
            }

            if ( !found ) Debug.LogWarning ("The Trait " + requested_trait.Name + " could not be found in " + this.GetType ().ToString () + ".");
        }

        return trait_names_and_manifestations;
    }

    */
    /*
    /// <summary>
    /// Adds a Trait relative to another Trait that has aready been added. The latter is specified by string.
    /// </summary>
    public void addTrait ( Trait trait, string linked_trait, int link_relative_position = 0 )
    {
        // später
        if ( position_in_genome < 0 ) trait.GenomePosition = Random.Range (0, GenomeString.Count); // genomes are circular
        traits.Add (trait);
    }
    
    /// <summary>
    /// Add a bunch of correlated traits which can't be separated. These should depend on eachother like rate of growth and need for nutrition.
    /// </summary>
    /// <param name="traits_and_their_relative_positions">A Dictionary which contains all the traits to be added to the genom alongside their respective positions to eachother. The first integer describes the offset of the whole complex int the genome. Pass -1 for random placement.</param>
    public void addTraitStructure ( IDictionary<Trait, int> traits_and_their_relative_positions )
    {
        // ToDo: each Traits needs its corresponding position
        //if ( traits_and_their_relative_positions. = -1 ) Random.Range (0, genome.Count); // genomes are circular

        foreach ( Trait t in traits_and_their_relative_positions.Keys )
        {
            t.Genome = this;
            traits.Add (t);
        }
    }
    */


    // currently not necessary
    //private List<Trait> getTraits ()
    //{
    //    List<Trait> all_traits = new List<Trait> ();
    //    foreach ( TraitManifestation manifestation in trait_manifestations )
    //    {
    //        all_traits.Add (manifestation.Trait);
    //    }

    //    return all_traits;
    //}

    // currently not necessary
    //public Dictionary<string, int> getTraitIntensities ( List<string> requested_trait_names_raw )
    //{
    //    List<string> requested_trait_names = requested_trait_names_raw.Distinct ().ToList (); // remove duplicates

    //    Dictionary<string, int> trait_names_and_intensities = new Dictionary<string, int> { };
    //    foreach ( string requested_trait_name in requested_trait_names )
    //    {
    //        bool found = false;
    //        foreach ( TraitManifestation manifestation in trait_manifestations )
    //        {
    //            if ( manifestation.Name == requested_trait_name )
    //            {
    //                trait_names_and_intensities.Add (manifestation.Name, manifestation.Intensity);
    //                found = true;
    //                break; // the inner foreach
    //            }
    //        }

    //        if ( !found ) Debug.LogWarning ("The Trait " + requested_trait_name + " could not be found in " + this.GetType ().ToString () + ".");
    //    }

    //    return trait_names_and_intensities;
    //}
}
