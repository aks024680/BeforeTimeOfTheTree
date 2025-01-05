using Fungus;
using UnityEngine;
namespace BeforeTimeOfTheTree
{

    public class StartSayController : MonoBehaviour
    {
        [SerializeField, Header("遊戲管理器")] protected Flowchart fungusGM;
        public string fungusString;
        [SerializeField] private GameObject player;
        private void Awake()
        {
            this.GetComponent<Player>().enabled = false;
        }
        private void Start()
        {
            

            fungusGM.SendFungusMessage("Start");
            
        }
        private void Update()
        {
            if (fungusGM.GetBooleanVariable(fungusString))
                this.GetComponent<Player>().enabled = true;
            
        }
    }
}
