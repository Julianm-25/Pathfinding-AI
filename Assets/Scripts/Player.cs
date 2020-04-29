using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    public GameObject player;
    public float speed = 7f;
    #endregion
    void Update()
    {
        //checks player position every update
        Vector3 position = player.transform.position;
        //moves up on W press
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, (speed * Time.deltaTime), 0);
        }
        //moves down on S press
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, (-speed * Time.deltaTime), 0);
        }
        //moves left on A press
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate((-speed * Time.deltaTime),0 , 0);
        }
        //moves right on D press
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate((speed * Time.deltaTime), 0, 0);
        }
    }
}
