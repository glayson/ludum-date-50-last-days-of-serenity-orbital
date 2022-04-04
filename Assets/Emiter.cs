using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emiter : MonoBehaviour
{
    public List<Meteor> meteors;
    public Vector3 center = Vector3.zero;
    public float excludingArea = 10f;
    public float meteorBelt = 100f;
    public float frequency = 0.5f;
    public float maxForce = 3f;
    public float interval = 1;

    public bool createPods = false;
    public float podFrequency = 1f;
    public float lastEmission;

    public SerenityHealth serenity;



    public SimpleMotor pod;

    // Start is called before the first frame update
    void Start()
    {
        lastEmission = Time.time;
        
        for (float x = -meteorBelt; x < meteorBelt; x +=interval)
        {
            for (float y = -meteorBelt; y < meteorBelt; y += interval)
            {
                for (float z = -meteorBelt; z < meteorBelt; z += interval)
                {
                    bool create = Random.Range(0f,1f) < frequency;
                    if (create)
                    {

                        Vector3 pos = new Vector3(Random.Range(x, x+ interval), Random.Range(y, y + interval), Random.Range(z, z + interval));
                        float dis = Vector3.Distance(center, pos);
                        if(dis>excludingArea && dis < meteorBelt)
                        {
                            Meteor temp = Instantiate(meteors[Random.Range(0, meteors.Count)]);
                            Vector3 force = new Vector3(
                                Random.Range(-maxForce, maxForce),
                                Random.Range(-maxForce, maxForce),
                                Random.Range(-maxForce, maxForce)
                                );
                            temp.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                            temp.transform.localScale = new Vector3(
                                temp.transform.localScale.x * Random.Range(.5f, 4f),
                                temp.transform.localScale.x * Random.Range(.5f, 4f),
                                temp.transform.localScale.x * Random.Range(.5f, 4f)
                                );
                            temp.transform.position = pos;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(
            createPods
            &&
            serenity.health>0
            && Time.time>lastEmission+(1f/podFrequency)
           )
        {
            EmmitPods();
        }
    }


    public void EmmitPods()
    {
        SimpleMotor tempod = Instantiate(pod);
        pod.transform.position = center;
        Vector3 directon = new Vector3(
                                Random.Range(-maxForce, maxForce),
                                Random.Range(-maxForce, maxForce),
                                Random.Range(-maxForce, maxForce)
                                ).normalized * meteorBelt;
        pod.target = directon;
        pod.serenity = serenity;
        serenity.PodCreated();
        lastEmission = Time.time;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, excludingArea);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center, meteorBelt);
    }
}
