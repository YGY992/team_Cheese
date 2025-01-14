using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;

    private GameObject enemy;
    private Animator enemyEffects;  // Animator 컴포넌트

    public void destroyEnemy()
    {
        Destroy(gameObject);
        // Item Drop
        LootBag[] lootBags = GetComponents<LootBag>();
        foreach (LootBag lootBag in lootBags)
        {
            if (lootBag != null)
            {
                lootBag.InstantiateLoot(transform.position);
            }
        }
    }

    public IEnumerator PlayDeathAnimationAndDestroy()
    {
        enemyEffects.Play(enemy.name + "Die");
        yield return new WaitForSeconds(enemyEffects.GetCurrentAnimatorStateInfo(0).length);
        // 애니메이션 재생 후 오브젝트 삭제
        destroyEnemy();
    }

    void Update()
    {
        if (hp == 0)
        {
            StartCoroutine(PlayDeathAnimationAndDestroy());
        }
    }

    void Start()
    {
        enemy = this.gameObject;
        enemyEffects = GetComponent<Animator>();
    }
}
