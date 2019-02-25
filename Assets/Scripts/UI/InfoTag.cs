using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoTag : MonoBehaviour {

    [SerializeField] private Text info_text; // info about object under cursor

    GameObject hit;

    void OnEnable ()
    {
        disableDisplay ();
    }

    // Update is called once per frame
    void Update ()
    {
        // get the object the mouse is pointing on
        hit = Camera.main.MouseObject (); // extension method, see script at top level

        if ( hit != null)
        {
            // get the reference to the script attached to the ingredient the mouse is pointing on
            //Planet script_reference = hit.transform.gameObject.GetComponent<Planet> ();

            if ( hit.tag == "Player Ship" )
            {

                info_text.text = "Player Ship"
                                + "\nName: "// + script_reference.name
                                + "\nPosition: "// + script_reference.transform.position
                                + "\nDescription: ";// + script_reference.Description;
                display ();
            }
            else if ( hit.transform.tag == "Planet" )
            {
                // get the reference to the script attached to the ingredient the mouse is pointing on
                //Planet script_reference = hit.transform.gameObject.GetComponent<Planet> ();

                // fill text
                info_text.text = "Planet, Class "// + script_reference.Type
                                + "\nName: " //+ script_reference.name
                                + "\nPosition: "// + script_reference.transform.position
                                + "\nDescription: ";// + script_reference.Description;

                display ();
            }
            // raycast didn't hit a valid object
            else disableDisplay ();

        }
        // raycast didn't hit anything
        else disableDisplay ();

    }

    // enables text and sets it to mouse position, applying offset
    void display ()
    {
        // compute offset (based on empirical values)
        float x_offset = info_text.rectTransform.rect.width /2 + 20;
        float y_offset = info_text.rectTransform.rect.height /2;

        // ensure the text panel doesn't flow out of screen
        Vector3 screen_space = Camera.main.WorldToViewportPoint (hit.transform.position);
        if ( screen_space.x > 0.5f )
            x_offset *= -1;
        if ( screen_space.y > 0.5f )
            y_offset *= -1;

        // apply changes to text transform
        transform.position = Input.mousePosition + new Vector3 (x_offset, y_offset, 0f);
        info_text.enabled = true;
    }

    void disableDisplay ()
    {
        info_text.text = "";
        info_text.enabled = false;
    }

}
