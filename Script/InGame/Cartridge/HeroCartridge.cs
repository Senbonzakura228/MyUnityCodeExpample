using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCartridge : MonoBehaviour
{
    [SerializeField] protected Renderer render;
    [SerializeField] protected ContactExplosionExpanse contactExplosion;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float destroySpeed;
    protected float damage;

    protected virtual void Start()
    {
        Invoke("DestroyCartridge", destroySpeed);
    }

    protected virtual void FixedUpdate()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider2D)
    {
        var enemy = TryDetectEnemy(collider2D);
        if (!enemy) return;
        DoDamage(enemy);
    }

    public virtual void SetParams(HeroCartridgeInstantiateParams instantiateParams)
    {
        damage = instantiateParams.damage;
        render.sortingLayerName = instantiateParams.sortingLayerName;
        render.sortingOrder = 2;
    }

    protected virtual void DestroyCartridge()
    {
        if (!gameObject) return;
        Destroy(gameObject);
    }

    protected virtual UnitLogic TryDetectEnemy(Collider2D collider2D)
    {
        var enemy = collider2D.GetComponent<UnitLogic>();
        if (!enemy) return null;
        return enemy.GetComponent<SpriteRenderer>().sortingLayerName != render.sortingLayerName ? null : enemy;
    }

    protected virtual void CreateContactExplosion()
    {
        Vector3 position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y,
            gameObject.transform.position.z);
        Instantiate(contactExplosion, position, gameObject.transform.rotation);
    }

    protected virtual void DoDamage(UnitLogic enemy)
    {
        CreateContactExplosion();
        enemy.TakeDamage(damage);
        DestroyCartridge();
    }
}