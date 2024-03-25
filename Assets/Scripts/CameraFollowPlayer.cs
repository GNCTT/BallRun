using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Vector2 boundX;
    [SerializeField] private Vector2 boundZ;

    [SerializeField] private Vector3 offset = new Vector3(1f, 1f, 1f);

    private void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        var newPos = player.transform.position + offset;
        newPos.x = Mathf.Clamp(newPos.x, boundX.x, boundX.y);
        newPos.z = Mathf.Clamp(newPos.z, boundZ.x, boundZ.y);
        this.transform.position = newPos;
    }
}
