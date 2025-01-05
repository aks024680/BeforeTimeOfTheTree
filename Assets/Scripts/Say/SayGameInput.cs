using Fungus;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace BeforeTimeOfTheTree
{

    public class SayGameInput : MonoBehaviour
    {
        [SerializeField,Header("遊戲管理器")] protected Flowchart fungusGM;
        [SerializeField]private GameObject SayCanvas;
        
        private void Start()
        {
            
            

            SayCanvas.SetActive(false);
        }
        private void Update()
        {

            


            if (Input.GetKeyDown(KeyCode.E))
            {
                BasicInput();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SayCanvas.SetActive(true);
            
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            SayCanvas.SetActive(false);
            
        }
        protected virtual void BasicInput()
        {
            fungusGM.SendFungusMessage("遊戲基礎操作");
        }
    }
}
