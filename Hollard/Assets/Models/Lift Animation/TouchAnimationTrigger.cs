using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TouchAnimationTrigger : MonoBehaviour
{
    public Animator liftAnimator;
    public AnimationClip liftAnimation;

    private void OnTriggerEnter(Collider other)
    {
        XRBaseControllerInteractor controllerInteractor = other.GetComponent<XRBaseControllerInteractor>();

        if (controllerInteractor != null)
        {
            if (liftAnimator != null && liftAnimation != null)
            {
                liftAnimator.Play(liftAnimation.name);
            }
            else
            {
                Debug.LogError("Lift Animator or Animation Clip is not assigned.");
            }
        }
    }
}
