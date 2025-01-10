using Fungus;
using UnityEngine;
namespace BeforeTimeOfTheTree
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField, Header("遊戲管理器")] protected Flowchart fungusGM;
        [SerializeField] private GameObject player;
        
        [SerializeField] private GameObject dialog;
        //[SerializeField] private GameObject conTrigger;
        [Space]
        private RectTransform rectTransform;
        private Canvas d2Panel;
        public string fungusString;
        public Vector3 offset;   // 偏移量，用於微調對話框位置
        public Transform target; // NPC 的 HeadAnchor
        
        void Start()
        {
            player.GetComponent<Player>().enabled = false;

            fungusGM.SendFungusMessage("Start");
            
            // 確保對話框在初始時隱藏
            
            d2Panel = dialog.GetComponent<Canvas>();
            rectTransform = dialog.GetComponent<RectTransform>();

            //if (d2Panel.renderMode != RenderMode.ScreenSpaceOverlay)
            //{
            //    Debug.LogError("Canvas 必須設置為 Screen Space - Overlay 模式！");
            //}
            
        }
        void Update()
        {
            if (fungusGM.GetBooleanVariable(fungusString))
                player.GetComponent<Player>().enabled = true;

            if (target != null && d2Panel != null)
            {
                // 將世界座標轉換為螢幕座標
                Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);

                // 更新對話框的 UI 位置
                rectTransform.position = screenPos;


            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                BasicInput();

            }
        }

        
        
        protected virtual void BasicInput()
        {
            fungusGM.SendFungusMessage("BasicPlay");
        }
    }
}
