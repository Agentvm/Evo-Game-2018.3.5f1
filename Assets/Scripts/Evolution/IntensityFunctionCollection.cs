using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntensityFunctionCollection
{
    // Intensity Functions // Traits

    /// <summary>
    /// Determines Maximum Size of a Character (e.g. Creature or Plant)
    /// </summary>
    public static int MaxSize ( List<bool> genome_segment )
    {
        return CountTrueValues (genome_segment );
    }
    
    /// <summary>
    /// Determines rate of growth
    /// </summary>
    public static int GrowRate ( List<bool> genome_segment )
    {
        return CountTrueValues (genome_segment);
    }

    /// <summary>
    /// Determines how much leaves are produced (+ how much light can be consumed and blocked). 
    /// </summary>
    public static int LeavesDensity ( List<bool> genome_segment )
    {
        return CountTrueValues (genome_segment);
    }

    /// <summary>
    /// Determines how much light is needed to stay alive and grow (overlaps heavily with growing-related Traits, genome-wise.)
    /// </summary>
    public static int LightRequirement ( List<bool> genome_segment )
    {
        return CountTrueValues (genome_segment);
    }

    /// <summary>
    /// Determines how many childs, seeds or offshoot are produced.
    /// </summary>
    public static int OffspringCount ( List<bool> genome_segment )
    {
        return CountTrueValues (genome_segment);
    }


    // Helper Functions //

    static int CountTrueValues ( List<bool> bool_list )
    {
        int nb = 0;
        foreach ( bool b in bool_list )
        {
            if ( b ) nb++;
        }

        return nb;
    }

    static int CountFalseValues ( List<bool> bool_list )
    {
        int nb = 0;
        foreach ( bool b in bool_list )
        {
            if ( !b ) nb++;
        }

        return nb;
    }

    static List<int> ReturnTrueIndices ( List<bool> bool_list )
    {
        List<int> indices = new List<int> ();
        for ( int i = 0; i < bool_list.Count; i++ )
        {
            if ( bool_list[i] ) indices.Add (i);
        }

        return indices;
    }

    static List<int> ReturnFalseIndices ( List<bool> bool_list )
    {
        List<int> indices = new List<int> ();
        for ( int i = 0; i < bool_list.Count; i++ )
        {
            if ( !bool_list[i] ) indices.Add (i);
        }

        return indices;
    }
}
