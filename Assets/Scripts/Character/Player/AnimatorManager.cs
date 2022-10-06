using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public static AnimatorManager Instance;

    public Animator animator;
    int horizontal;
    int vertical;

    public float slowFactor;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }
    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
        animator.speed = 1 - slowFactor/2;
    }
    public void UpdateAnimatorValue(float horizontalMovement, float verticalMovement, bool isSprintng)
    {
        //Animation snapping
        float snappedHorizontal;
        float snappedVertical;
        #region Snapped Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            snappedHorizontal = 0.5f;
        else if (horizontalMovement > 0.55f)
            snappedHorizontal = 1f;
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            snappedHorizontal = -0.5f;
        else if (horizontalMovement < -0.55f)
            snappedHorizontal = -1f;
        else
            snappedHorizontal = 0f;
        #endregion
        #region Snapped Vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
            snappedVertical = 0.5f;
        else if (verticalMovement > 0.55f)
            snappedVertical = 1f;
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
            snappedVertical = -0.5f;
        else if (verticalMovement < -0.55f)
            snappedVertical = -1f;
        else
            snappedVertical = 0f;
        #endregion

        if (isSprintng)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
