using UnityEngine;
namespace BeforeTimeOfTheTree
{

    public class StartSayController : SayGameInput
    {
        [SerializeField] private string fungusString;
        [SerializeField] private GameObject player;
        private void Start()
        {
            fungusGM.SendFungusMessage("Start");
            if (fungusGM.GetBooleanVariable(fungusString))
                player.GetComponent<Player>().enabled = false;
        }
        private void Update()
        {
            if (fungusGM.GetBooleanVariable(fungusString))
                player.GetComponent<Player>().enabled = true;
        }
    }
}
