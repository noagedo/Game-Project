using UnityEngine;

public class WolfSimpleAI : MonoBehaviour
{
    private Animator animator;

    public AudioSource howlAudio;  // שדה להוספת האודיו בממשק

    private float timer = 0f;
    private float howlDuration = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isHowling", false);

        if (howlAudio != null)
            howlAudio.Stop();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= howlDuration && timer < howlDuration + 3f)
        {
            animator.SetBool("isHowling", true);

            if (howlAudio != null && !howlAudio.isPlaying)
                howlAudio.Play();
        }
        else if (timer >= howlDuration + 3f)
        {
            animator.SetBool("isHowling", false);

            if (howlAudio != null && howlAudio.isPlaying)
                howlAudio.Stop();

            timer = 0f;
        }
    }
}
