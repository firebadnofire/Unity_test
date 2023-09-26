using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController cha;
    Vector3 move_speed;
    float gravity = -9.8f;
    float jump_speed = 0.5f;

    void Start()
    {
        cha = GetComponent<CharacterController>();
    }

    void Update()
    {
        move_speed = new Vector3(Input.GetAxis("Horizontal"), move_speed.y + gravity * Time.deltaTime, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move_speed.y = jump_speed;
        }
        cha.Move(move_speed);


    }
}