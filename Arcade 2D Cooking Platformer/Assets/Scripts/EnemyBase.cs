using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] public int health = 0;
    [SerializeField] private int maxHealth;

    [SerializeField] private int indexEnemyTriggerList; 
    [SerializeField] private int maxCount;

    [SerializeField] private Animator _animator;

    [SerializeField] private AIPath aipath;


    private void Awake()
    {
        _animator.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {       

        //disable the enemy
        GetComponent<Collider2D>().enabled = true;

        if (GameObjectActiveManger.instance.GetEnemyTriggerList() != null)
        {
            if (GameObjectActiveManger.instance.GetEnemyTriggerList().Count == maxCount)
            {
                if (GameObjectActiveManger.instance.GetEnemyTriggerList()[indexEnemyTriggerList] == true)
                {
                    Destroy(gameObject);
                }
            }
        }

        //set the health to max health of the enemy
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().playAudio("slash");
        //The enemy take the damage from the monsters.
        health -= damage;

        StartCoroutine(DamageIndicator());
        StartCoroutine(DamageIndicator());

        //If the enemy dies
        if (health <= 0)
        {            
            Die();
        }
        
    }

    private IEnumerator DamageIndicator()
    {
        aipath.canMove = false;
        //take damage frame
        _animator.SetTrigger("takeDamage");

        //Wait a second or 2
        yield return new WaitForSeconds(1f);
        aipath.canMove = true;

    }

    private void Die()
    {
        //disable the enemy
        GetComponent<Collider2D>().enabled = false;
        //TODO prevent enemy from doing more attacks

        if (GameObjectActiveManger.instance.GetEnemyTriggerList() != null)
        {
            if (GameObjectActiveManger.instance.GetEnemyTriggerList().Count == maxCount)
            {
                GameObjectActiveManger.instance.GetEnemyTriggerList()[indexEnemyTriggerList] = true;
            }
        }       

        StopCoroutine(FlickeringDie());
        StartCoroutine(FlickeringDie());

        StopCoroutine(playDeathAnimation());
        StartCoroutine(playDeathAnimation());  
              
    }

    private IEnumerator playDeathAnimation()
    {
        //play animation
        _animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }


    private IEnumerator FlickeringDie()
    {   //Turn the enemy red

        for (int i = 0; i < 3; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(.1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.2f);
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

    }

}
