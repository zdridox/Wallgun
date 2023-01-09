using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] float flowSpeed;
    Renderer rend;
    [SerializeField] Transform spawn;
    [SerializeField] AudioClip DieSound;
    public Ammo Ammo;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveThis = Time.time * flowSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, moveThis));
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(collision.gameObject);
        if(collision.transform.tag == "Player")
        {
            collision.transform.position = spawn.transform.position;
            SoundManager.SMInstance.PlaySound(DieSound);
        } else
        {
            Destroy(collision.gameObject);
        }
        Ammo.RealAmmo = Ammo.AmmoSetup;
    }
}
