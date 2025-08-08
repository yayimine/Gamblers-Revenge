using UnityEngine;

public class AfterImageFade : MonoBehaviour
{
    public float lifeTime = 0.2f;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, lifeTime); // auto destroy after lifetime
    }
}