using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    public float maxDistanceRay = 100f;
    public static RaycastController instance;
    public Transform gunFlashTarget;
    public float fireRate = 1.6f;
    private bool nextShot = true;
    private string objName = "";
    public AudioClip fire;

    AudioSource audio;
    public AudioClip[] clips;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnNewBird());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake(){
        if(instance == null){
            instance = this;
        }
        audio = GetComponent<AudioSource>();
    }

    public void playSound(int sound){
        audio.clip = clips[sound];
        audio.Play();
    }

    private IEnumerator spawnNewBird(){
        yield return new WaitForSeconds(3f);

        GameObject newBird = Instantiate(Resources.Load("Bird_Asset", typeof(GameObject))) as GameObject;

        newBird.transform.parent = GameObject.Find("ImageTarget").transform;
        
        newBird.transform.localScale = new Vector3(10f, 10f, 10f);

        Vector3 temp;
        temp.x = Random.Range(-1.8f, 1.7f);
        temp.y = Random.Range(0.28f, 0.7f);
        temp.z = Random.Range(-1.43f, 1.64f);
        newBird.transform.position = new Vector3(temp.x, temp.y, temp.z);
    }

    public void Fire(){
        audio.clip = fire;
        audio.Play();
        if(nextShot){
            StartCoroutine(takeShot());
            nextShot = false;
        }
    }

    private IEnumerator takeShot(){
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        GameController.instance.TembakPerRonde--;

        int layer_mask = LayerMask.GetMask("birdLayer");
        if(Physics.Raycast(ray,out hit, maxDistanceRay, layer_mask)){
            objName = hit.collider.gameObject.name;
            Vector3 birdPosition = hit.collider.gameObject.transform.position;

            if(objName == "Bird_Asset(Clone)"){
                Destroy(hit.collider.gameObject);

                StartCoroutine(spawnNewBird());
                GameController.instance.TembakPerRonde = 3;
                GameController.instance.playerScore++;
                GameController.instance.roundScore++;
            } else {
                print("Tembakan tidak tepat");
            }
        }

        GameObject gunFlash = Instantiate(Resources.Load("gunFlashSmoke", typeof(GameObject))) as GameObject;
        gunFlash.transform.position = gunFlashTarget.position;

        yield return new WaitForSeconds(fireRate);
        
        nextShot = true;

        GameObject[] smokeGroup = GameObject.FindGameObjectsWithTag("GunSmoke");
        foreach (GameObject smoke in smokeGroup){
            Destroy(smoke.gameObject);
        }
    }
}
