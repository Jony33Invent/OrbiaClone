using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimento : MonoBehaviour
{
	List<Transform> posAlvo=new List<Transform>();
    [SerializeField] Transform targetSpawner;
    TargetSpawn spawn;
    [SerializeField] GameObject linePrefab;
    [SerializeField] Transform lineSpawn;
	int currAlvo=1;
	bool parado=true;
    [SerializeField] float speed;

    public void SpawnLines(){
        /*
        line.positionCount=posAlvo.Count+1;
        line.SetPosition(0,transform.position);
        for(int i=1;i<=posAlvo.Count;i++){
            line.SetPosition(i,posAlvo[i-1].position);
        }*/
        for(int i=0;i<posAlvo.Count-1;i++){
            GameObject lineObj=Instantiate(linePrefab,new Vector3(0,0,0),Quaternion.identity,lineSpawn);
            LineRenderer line=lineObj.GetComponent<LineRenderer>();
            line.positionCount=2;
            Vector3 a=posAlvo[i].position;
            Vector3 b=posAlvo[i+1].position;
            Vector3 p1=Vector3.Normalize(b-a)*1f+a;
            Vector3 p2=Vector3.Normalize(a-b)*1f+b;
            line.SetPosition(0,p1);
            line.SetPosition(1,p2);
        }

    }
    void RestartGame(){
        
        currAlvo=1;
        transform.position=new Vector3(0,0,0);
        for(int i=1;i<posAlvo.Count;i++){
            posAlvo[i].gameObject.GetComponent<TargetPoint>().EnableEnemies(true);
        }
        /*
        foreach(Transform line in lineSpawn){
            Destroy(line.gameObject);
        }
        for(int i=1;i<posAlvo.Count;i++){
            Destroy(posAlvo[i].gameObject);
        }

        StartGame();*/
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ResetLast(){
        parado=true;
        transform.position=posAlvo[currAlvo-1].position;
    }
    void StartGame(){
            posAlvo.Clear();
            posAlvo=new List<Transform>();
            spawn.SpawnTargets();
            posAlvo.Add(transform);
            foreach(Transform target in targetSpawner){
                posAlvo.Add(target);
            }
            SpawnLines();
    }
    // Start is called before the first frame update
    void Start()
    {
        spawn=targetSpawner.GetComponent<TargetSpawn>();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
    	if(parado){
    		//detecta mouse
	        if ( Input.GetMouseButtonDown(0)){
	        	parado=false;
	        }
    	}else{
    		//move
    		Vector2 target=posAlvo[currAlvo].position;
        	transform.position = Vector2.MoveTowards(transform.position, target, speed*Time.deltaTime);
        	if(Vector2.Distance(transform.position,target)<0.1f){

                posAlvo[currAlvo].gameObject.GetComponent<TargetPoint>().EnableEnemies(false);
        		parado=true;
        		currAlvo++;
                if(currAlvo>=posAlvo.Count){
                    RestartGame();
                   // currAlvo=posAlvo.Count-1;
                }
        	}
    	}
    }       
     void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy")){
            parado=true;
            RestartGame();
        }
    }
}
