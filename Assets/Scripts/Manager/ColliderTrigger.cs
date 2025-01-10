using UnityEngine;
namespace BeforeTimeOfTheTree
{

    public class ColliderTrigger : MonoBehaviour
    {
        public GameObject dialoguePanel;  // 對話框面板
        private bool isPlayerInRange = false;
        [SerializeField] private GameObject SayCanvas;

        private void Start()
        {
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }
            // 初始化對話框狀態
            int savedState = PlayerPrefs.GetInt("NPC_Dialogue_Active", 0);
            dialoguePanel.SetActive(savedState == 1);
            SayCanvas.SetActive(false);
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
    }
}
