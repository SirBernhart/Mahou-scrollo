using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // External references
    [SerializeField] private EntityMovement playerMovement;
    [SerializeField] private TransformationController transformationController;
    [SerializeField] private MeleeAttack meleeAttack;
    [SerializeField] private RangedAttack rangedAttack;
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private Health playerHealth;

    // Internal variables
    private float moveDirectionX;

    // Update is called once per frame
    void Update()
    {
        moveDirectionX = 0f;

        // Player character actions
        if (playerHealth.GetCanAct())
        {
            moveDirectionX = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                playerMovement.Jump();
            }
            if (Input.GetButtonUp("Jump"))
            {
                playerMovement.CutJumpHeight();
            }

            if (Input.GetButtonDown("Transform"))
            {
                //playerMovement.StopiAllMovement();
                transformationController.Transform();
            }

            if (Input.GetButtonDown("LightAttack"))
            {
                if (transformationController.GetIsInNormalForm())
                {
                    meleeAttack.Attack(ActionType.lightMelee);
                }
                else
                {
                    rangedAttack.Attack(ActionType.lightRanged);
                }
            }
            if (Input.GetButtonDown("HeavyAttack"))
            {
                if (transformationController.GetIsInNormalForm())
                {
                    meleeAttack.Attack(ActionType.heavyMelee);
                }
                else
                {
                    rangedAttack.Attack(ActionType.heavyRanged);
                }
            }
        }

        playerMovement.MoveInDirection(moveDirectionX);

        // Interface controls
        if (Input.GetButtonDown("Cancel"))
        {
            screenManager.SetPauseMenuActive();
        }
    }
}
