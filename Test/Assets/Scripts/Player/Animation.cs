using UnityEngine;

public class Animation : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Walk(int x, int y)
    {
        anim.SetInteger("X", x);
        anim.SetInteger("Y", y);
    }
    public void Attack()
    {
        anim.SetInteger("DIR", Player.attackDir);
        Player.attackDir = 0;
    }
    public void Hit(bool hit)
    {
        anim.SetBool("ISHIT", hit);
    }

    public void Death(bool isDead)
    {
        anim.SetBool("ISDEAD", isDead);
    }
}
