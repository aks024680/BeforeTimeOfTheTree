using Fungus;
using UnityEngine;
namespace BeforeTimeOfTheTree
{

    public class StartSayController : MonoBehaviour
    {
        [SerializeField, Header("遊戲管理器")] protected Flowchart fungusGM;
        [SerializeField] private string fungusString;
        [SerializeField] private GameObject player;
        private void Awake()
        {
            this.GetComponent<Player>().enabled = false;
        }
        private void Start()
        {
            

            fungusGM.SendFungusMessage("Start");
            Debug.Log(fungusString);
        }
        private void Update()
        {
            if (fungusGM.GetBooleanVariable(fungusString))
                this.GetComponent<Player>().enabled = true;
            Debug.Log(fungusString);
        }
    }
}
