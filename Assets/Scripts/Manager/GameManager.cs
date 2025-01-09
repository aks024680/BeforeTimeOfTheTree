using Fungus;
using UnityEngine;
namespace BeforeTimeOfTheTree
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField, Header("遊戲管理器")] protected Flowchart fungusGM;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject SayCanvas;
        [SerializeField] private GameObject dialog;
        [Space]
        private RectTransform rectTransform;
        private Canvas d2Panel;
        public string fungusString;
        public Vector3 offset;   // 偏移量，用於微調對話框位置
        public Transform target; // NPC 的 HeadAnchor
        public GameObject dialoguePanel;  // 對話框面板
        private bool isPlayerInRange = false;
        void Start()
        {
            player.GetComponent<Player>().enabled = false;

            fungusGM.SendFungusMessage("Start");
            
            // 確保對話框在初始時隱藏
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }
            // 初始化對話框狀態
            int savedState = PlayerPrefs.GetInt("NPC_Dialogue_Active", 0);
            dialoguePanel.SetActive(savedState == 1);
            d2Panel = dialog.GetComponent<Canvas>();
            rectTransform = dialog.GetComponent<RectTransform>();

            //if (d2Panel.renderMode != RenderMode.ScreenSpaceOverlay)
            //{
            //    Debug.LogError("Canvas 必須設置為 Screen Space - Overlay 模式！");
            //}
            SayCanvas.SetActive(false);
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

        void OnTriggerEnter2D(Collider2D other)
        {
            
            SayCanvas.SetActive(true);
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = true;
                if (dialoguePanel != null)
                {
                    dialoguePanel.SetActive(true); // 顯示對話框
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            
            SayCanvas.SetActive(false);
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = false;
                if (dialoguePanel != null)
                {
                    dialoguePanel.SetActive(false); // 隱藏對話框
                }
            }
        }
        void OnApplicationQuit()
        {
            // 保存對話框狀態
            PlayerPrefs.SetInt("NPC_Dialogue_Active", dialoguePanel.activeSelf ? 1 : 0);
        }
        protected virtual void BasicInput()
        {
            fungusGM.SendFungusMessage("BasicPlay");
        }
    }
}
