using UnityEngine;
namespace BeforeTimeOfTheTree
{


    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private Player player => GetComponentInParent<Player>();
        private void AnimationTrigger()
        {
            player.AnimationTrigger();
        }
    }
}