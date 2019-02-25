using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public static class ExtensionMethods {

    /// <summary>
    /// Finds the Object the Mouse is currently pointing on
    /// </summary>
    /// <returns>The Gameobject hit by the ray</returns>
    public static GameObject MouseObject ( this Camera cam )
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        if ( Physics.Raycast (ray, out hit, 1000.0f) )
        {
            // Check if the mouse was clicked over a UI element
            if ( !EventSystem.current.IsPointerOverGameObject () )
                return hit.transform.gameObject;
        }

        return null;
    }

    /// <summary>
    /// Tells if a Gameobject is a Pefab
    /// </summary>  
    /// <returns>True if a_object is a Prefab</returns>
    public static bool IsPrefab ( this GameObject a_object )
    {
        return a_object.scene.rootCount == 0;
    }

}
