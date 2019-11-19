using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpCoroutine : MonoBehaviour {
    protected Vector3 target_position;
    protected Quaternion target_rotation;
    protected float initial_distance_to_target; // is recorded to smooth the slerp movement depending on travelled distance
    protected float initial_angle_to_target; // is recorded to smooth the slerp movement depending on rotated angle
    [SerializeField] protected float translational_movement_factor = 0.7f; // basic translation modificator
    [SerializeField] protected float rotational_movement_factor = 0.005f; // basic rotation modificator

    [SerializeField] protected float remaining_distance_factor; // modifies the linear speed change induced analogous to the remaining distance
    [SerializeField] protected float remaining_angle_factor; // modifies the linear speed change induced analogous to the remaining angle

    float remaining_distance_inversion_factor = 1.0f;
    float remaining_angle_inversion_factor = 1.0f;
    [SerializeField] protected bool invert_distance_effect = false; // can be used to invert the effects of remaining_distance_factor
    [SerializeField] protected bool invert_angle_effect = false; // can be used to invert the effects of remaining_angle_factor

    public Vector3 TargetPosition
    {
        get { return target_position; }
    }

    public Quaternion TargetRotation
    {
        get { return target_rotation; }
    }

    void Awake ()
    {
        setTarget (gameObject.transform); // set initial target to current transform of the gameobject this is attached to
        StopCoroutine ("SlerpPositionRotation");
    }

    public void stopCoroutine ()
    {
        StopCoroutine ("SlerpPositionRotation");
    }

    public void startCoroutine ()
    {
        StartCoroutine ("SlerpPositionRotation");
    }
    
    public void setTarget ( Transform t )
    {
        target_position = t.position;
        target_rotation = t.rotation;
        initial_distance_to_target = Vector3.Distance (this.transform.position, target_position);
        initial_angle_to_target = Quaternion.Angle (this.transform.rotation, target_rotation);

        StopCoroutine ("SlerpPositionRotation");
        StartCoroutine ("SlerpPositionRotation");
    }

    public void setTarget ( Vector3 pos, Quaternion rot )
    {
        target_position = pos;
        target_rotation = rot;
        initial_distance_to_target = Vector3.Distance (this.transform.position, target_position);
        initial_angle_to_target = Quaternion.Angle (this.transform.rotation, target_rotation);

        StopCoroutine ("SlerpPositionRotation");
        StartCoroutine ("SlerpPositionRotation");
    }

    /// <summary>
    /// Coroutine behaviour using Slerp functions to alter the transform of the gameobject this script is attached to according to target set.
    /// translational_movement_factor = 0.7f; // basic translation modificator
    /// rotational_movement_factor = 0.005f; // basic rotation modificator
    /// invert_distance_effect: If activated, transformation speed will increase at the beginning of movement, else it will increase near the end.
    /// invert_angle_effect: If activated, rotation speed will increase at the beginning of movement, else it will increase near the end.
    /// remaining_distance_factor; // modifies the linear speed change induced analogous to the remaining distance
    /// remaining_angle_factor; // modifies the linear speed change induced analogous to the remaining angle
    /// </summary>
    /// <returns></returns>
    IEnumerator SlerpPositionRotation ( )
    {
        while ( this.transform.position != target_position || this.transform.rotation != target_rotation)
        {
            // compute remaining distance and angle to target transform
            float remaining_dist = Vector3.Distance (this.transform.position, target_position );
            float remaining_angle = Quaternion.Angle (this.transform.rotation, target_rotation );

            // invert the distance effects, if necessary
            if ( invert_distance_effect ) remaining_distance_inversion_factor = -1.0f;
            else remaining_distance_inversion_factor = 0f;
            if ( invert_angle_effect ) remaining_angle_inversion_factor = -1.0f;
            else remaining_angle_inversion_factor = 0f;


            // apply the translation relative to Time.deltaTime, applying 2 kinds of smoothing factors
            this.transform.position = Vector3.Lerp (this.transform.position, target_position,
                (translational_movement_factor + remaining_distance_factor * Mathf.Abs (remaining_distance_inversion_factor + (remaining_dist / initial_distance_to_target ) )) * Time.deltaTime);
            this.transform.rotation = Quaternion.Slerp (this.transform.rotation, target_rotation,
                (rotational_movement_factor + remaining_angle_factor * Mathf.Abs (remaining_angle_inversion_factor + (remaining_angle / initial_angle_to_target ) )) * Time.time);

            yield return null;
        }
    }
}
