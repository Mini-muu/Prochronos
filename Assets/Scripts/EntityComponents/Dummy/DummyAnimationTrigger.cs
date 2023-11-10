using UnityEngine;

public class DummyAnimationTrigger : MonoBehaviour
{
    private Dummy dummy => GetComponentInParent<Dummy>();

    private void AnimationTrigger()
    {
        dummy.AnimationTrigger();
    }
}
