using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero S { get; private set; }

    [Header("Inscribed")] public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Header("Dynamic")] [Range(0, 4)] [SerializeField]
    public float _shieldLevel = 1;
    [Tooltip("this field holds a reference to the last triggering game object")]
    private GameObject lastTriggerGo = null;
    public float shieldLevel
    {
        get{return(_shieldLevel);}
        private set{
            _shieldLevel = Mathf.Min(value,4);
            if(value<0)
            {
                Destroy(this.gameObject);
                Main.HERO_DIED();
            }
        }
    }
    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }

    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigid = projGO.GetComponent<Rigidbody>();
        rigid.velocity = Vector3.up * projectileSpeed;
    }

    private void Update()
    {
        var hAxis = Input.GetAxis("Horizontal");
        var vAxis = Input.GetAxis("Vertical");

        var pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(vAxis * pitchMult, hAxis * rollMult, 0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //Debug.Log("Shield Trigger hit by: "+ go.name);
        if(go == lastTriggerGo) return;
        lastTriggerGo = go;
        Enemy enemy = go.GetComponent<Enemy>();
        if(enemy != null)
        {
            shieldLevel--;
            Destroy(go);
        }
        else{
            Debug.Log("Shield Trigger hit by a non Enemy: "+ go.name);
        }
    }
}
