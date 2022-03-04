using System.Collections;
using UnityEngine;

public class AnimationDelay : MonoBehaviour
{
    [SerializeField, Range(5,100)]
    private int delay = 5;
    [SerializeField]
    private string animationName;
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
            yield return new WaitForSeconds(delay);
            GetComponent<SpriteRenderer>().enabled = true;
            m_Animator.Play(animationName);
         }
     }
}
