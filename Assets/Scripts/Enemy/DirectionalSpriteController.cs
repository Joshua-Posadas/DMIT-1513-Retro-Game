using UnityEngine;

public class DirectionalSpriteController : MonoBehaviour
{
    public EnemyDirectionalSprites sprites;

    [Header("References")]
    public Transform player;
    public Transform playerCamera;
    public Transform spritePivot;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = spritePivot.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player == null) return;
        Vector3 toPlayer = player.position - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, toPlayer, Vector3.up);

        if (angle > -45f && angle <= 45f)
            sr.sprite = sprites.front;

        else if (angle > 45f && angle <= 135f)
            sr.sprite = sprites.right;

        else if (angle <= -45f && angle > -135f)
            sr.sprite = sprites.left;

        else
            sr.sprite = sprites.back;
    }

    private void LateUpdate()
    {
        Vector3 lookDirection = playerCamera.position - spritePivot.position;
        lookDirection.y = 0;
        spritePivot.rotation = Quaternion.LookRotation(lookDirection);
    }
}