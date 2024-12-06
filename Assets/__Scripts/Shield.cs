using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Inscribed")] public float rotationsPerSecond = 0.1f;

    [Header("Dynamic")] public int levelShown;

    private Material _mat;

    private void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        var curLevel = Mathf.FloorToInt(Hero.S.shieldLevel);
        if (levelShown != curLevel)
        {
            levelShown = curLevel;
            _mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }

        var rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
