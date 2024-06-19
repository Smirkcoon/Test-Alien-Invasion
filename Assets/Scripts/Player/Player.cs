using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttack), typeof(PlayerAnimation))]
public class Player : MonoBehaviour
{
    public static Player Instance;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerAnimation playerAnimation;

    private void Awake()
    {
        Instance = this;
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        playerMovement.HandleMovement();
        playerAnimation.HandleRotation();
        playerAttack.HandleAttack();
    }
}
