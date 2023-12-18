using UnityEngine;

public class EnemiesAnim : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public void Walk()
    {
        anim.SetInteger("DIR", Enemie.moveDir);
    }
    public void Attack(bool attack)
    {
        anim.SetBool("ISATT", attack);
        Player.attackDir = 0;
    }

    public void Death(bool isDead)
    {
        anim.SetBool("ISDEAD", isDead);
    }
}
