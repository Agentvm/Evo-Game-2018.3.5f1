using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntensityFunctionCollection
{
    public int MaxSizeIntensity ( List<bool> genome_segment )
    {
        return CountTrueValues (genome_segment );
    }



    // Helper Functions

    int CountTrueValues ( List<bool> bool_list )
    {
        int nb = 0;
        foreach ( bool b in bool_list )
        {
            if ( b ) nb++;
        }

        return nb;
    }

    int CountFalseValues ( List<bool> bool_list )
    {
        int nb = 0;
        foreach ( bool b in bool_list )
        {
            if ( !b ) nb++;
        }

        return nb;
    }

    List<int> ReturnTrueIndices ( List<bool> bool_list )
    {
        List<int> indices = new List<int> ();
        for ( int i = 0; i < bool_list.Count; i++ )
        {
            if ( bool_list[i] ) indices.Add (i);
        }

        return indices;
    }

    List<int> ReturnFalseIndices ( List<bool> bool_list )
    {
        List<int> indices = new List<int> ();
        for ( int i = 0; i < bool_list.Count; i++ )
        {
            if ( !bool_list[i] ) indices.Add (i);
        }

        return indices;
    }
}
