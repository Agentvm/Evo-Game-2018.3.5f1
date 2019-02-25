using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    [SerializeField]
    private GameCore game_core; // game manager prefab to instantiate


    void Awake ()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if ( GameCore.instance == null )

            //Instantiate gameManager prefab
            Instantiate (game_core);
    }
}