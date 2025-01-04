using Fungus;
using UnityEngine;

namespace BeforeTimeOfTheTree
{

    public class SayGameInput : MonoBehaviour
    {
        [SerializeField,Header("遊戲管理器")] private Flowchart fungusGM;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            BasicInput();
        }
        protected virtual void BasicInput()
        {
            fungusGM.SendFungusMessage("遊戲基礎操作");
        }
    }
}
