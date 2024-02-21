using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public float jumpForce = 10;
    public float gravityModifier = 1f;
    public bool isOnGround = true;
    public bool gameOver;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }
    // Make the space work as a jump button


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            //start dirt effect back up
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle")) 
       {
            //game over on barrel collision
            gameOver = true;
            //make the death animation play on barrel collision
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            //game over text in debug log when barrel collision
            UnityEngine.Debug.Log("Game Over");
            //make things go boom
            explosionParticle.Play();
            //stop dirt effect
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
       }
    }

}



//MAKE THE SOUND EFFECTS HEARD