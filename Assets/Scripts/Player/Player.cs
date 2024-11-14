using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public PlayerMovement playerMovement;
    public Animator animator;
    private Weapon currentWeapon;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = transform.Find("Engine/EngineEffect").GetComponent<Animator>();
    }


    void Update()
    {
    }

    void FixedUpdate()
    {
        if (playerMovement != null)
        {
            playerMovement.Move();

        }
    }



    void LateUpdate()
    {
        if (animator != null && playerMovement != null)
        {
            bool isMoving = playerMovement.IsMoving();
            // Debug.Log("IsMoving: " + isMoving);
            animator.SetBool("IsMoving", isMoving);
        }
    }
}