using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;
    Vector2 movement;
    Bag bag;
    AudioSource audioSource;
    public static int health = 10;
    public static int shield = 10;
    // float movex;
    [SerializeField]private float speed = 10f;
    [SerializeField]private float jumpSpeed = 10f;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bag = GetComponent<Bag>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //空格跳跃

        
        if (rigidbody2D != null)
        {
            if (!GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}

            if(Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
            }            
        }
        rigidbody2D.velocity = new Vector2(movement.x * speed,rigidbody2D.velocity.y);

        if(GameManager.Instance.currentPortal != null) 
        {
            GameManager.Instance.currentPortal.GetComponent<IUseable>().Use();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<ICollectable>() != null)
        {
            other.GetComponent<ICollectable>()?.Collect();
            bag.UpdateItem(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Portal")
        {
            // GameManager.Instance.canTeleport = true;
            GameManager.Instance.currentPortal = other.gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Portal")
        {
            // GameManager.Instance.canTeleport = false;
            GameManager.Instance.currentPortal = null;
        }
    }

    void PlayAudio(PotionSO potionSO)
    {
        audioSource.clip = potionSO.audioClip;
        audioSource.Play();
    }

    private void OnEnable() {
        PotionSO.UsePotion += PlayAudio;
    }

    private void OnDisable() {
        PotionSO.UsePotion -= PlayAudio;
    }
}
