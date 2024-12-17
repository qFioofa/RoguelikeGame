using System.Collections;
using UnityEngine;

public class BulletTrailEffect : MonoBehaviour
{
    public GameObject BulletTrail;

    public void CreateBulletTrail(Vector3 startPoint, Vector3 endPoint)
    {
        GameObject bullet = Instantiate(BulletTrail, startPoint, Quaternion.identity);
        TrailRenderer trail = bullet.GetComponent<TrailRenderer>();
        if (trail != null)
        {
            StartCoroutine(SpawnTrail(trail, startPoint, endPoint));
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 start, Vector3 end)
    {
        float time = 0;
        float duration = trail.time;

        while (time < duration)
        {
            trail.transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        trail.transform.position = end;
        Destroy(trail.gameObject, trail.time);
    }
}
