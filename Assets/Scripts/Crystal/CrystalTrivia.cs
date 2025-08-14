using UnityEngine;

public class CrystalTrivia : MonoBehaviour
{
    public TriviaManager triviaManager;
    public Transform teleportPoint;
    private Transform playerTransform;
    private bool triggered = false;   // למנוע פתיחה כפולה של השאלה
    private bool collected = false;   // למנוע איסוף כפול לאחר תשובה נכונה
    private Collider crystalCollider;

    void Awake()
    {
        // חשוב: כל קריסטל בסצנה נרשם כדי ש-GameManager ידע כמה נשאר
        GameManager.RegisterCrystal();
    }

    void Start()
    {
        crystalCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;                 // כבר נאסף – לא לאפשר אינטראקציה
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            if (crystalCollider != null)
                crystalCollider.enabled = false; // מונעים כניסה חוזרת בזמן הטריוויה

            playerTransform = other.transform;
            triviaManager.player = playerTransform;
            triviaManager.teleportPoint = teleportPoint;

            // בהצגת השאלה מעבירים שני callbacks:
            // 1) תשובה נכונה -> אוספים את הקריסטל
            // 2) תשובה שגויה -> מחזירים לשחקן לנסות שוב
            triviaManager.ShowQuestion(OnCorrectAnswer, TeleportPlayer);
        }
    }

    private void OnCorrectAnswer()
    {
        if (collected) return; // הגנה כפולה (במקרה של קריאה כפולה מה-UI)
        collected = true;

        Debug.Log("Player answered correctly. Crystal collected!");

        // מעלה את המונה המצטבר שמוצג ב-UI
        GameManager.AddCrystals(1);

        // מפחית "כמה נשאר בסצנה"; אם זה היה האחרון – המנהל יעביר סצנה
        GameManager.UnregisterCrystal();

        // אפשר או להשמיד או לכבות לצמיתות. עדיף להשמיד כדי שלא יחזור בטעות.
        Destroy(gameObject);
        // אם בכל זאת רוצים להשאיר באובייקט אך להיעלם מהעולם:
        // gameObject.SetActive(false);
    }

    private void TeleportPlayer()
    {
        Debug.Log("TeleportPlayer called!");
        if (playerTransform != null)
        {
            var controller = playerTransform.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;
            playerTransform.position = teleportPoint.position;
            if (controller != null) controller.enabled = true;
        }

        // מאפשר לנסות שוב את הטריוויה
        triviaManager.ResetQuestion();

        if (!collected && crystalCollider != null)
            crystalCollider.enabled = true; // להחזיר אפשרות כניסה רק אם לא נאסף

        triggered = false; // מוכן לניסיון נוסף
    }
}