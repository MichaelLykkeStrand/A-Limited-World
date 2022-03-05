using System.Collections;
using UnityEngine;

public class AnimationDelay : MonoBehaviour
{
    [SerializeField, Min(1)] private int minDelay = 5;
    [SerializeField, Min(10)] private int maxDelay = 50;

    public System.Action onAnimationPlay;
    private Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        m_Animator = GetComponent<Animator>();
        StartCoroutine(PlayAnimation());
    }

     IEnumerator PlayAnimation()
     {
         while (true)
         {
            int random = Random.Range(minDelay,maxDelay);
            yield return new WaitForSeconds(random);
            GetComponent<SpriteRenderer>().enabled = true;
            onAnimationPlay?.Invoke();
            m_Animator.Play("");
         }
     }
}
