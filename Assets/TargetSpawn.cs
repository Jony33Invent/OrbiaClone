using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawn : MonoBehaviour
{

	[SerializeField] int targetNum;
	[SerializeField] GameObject targetPrefab;
	[SerializeField] float rangeX=4f;
	[SerializeField] float rangeYmin=6f;
	[SerializeField] float rangeYmax=6f;
	float lastY=0;
	public void Spawn(){
		Vector3 randomPos=new Vector3(Random.Range(-rangeX,rangeX),Random.Range(rangeYmin,rangeYmax)+lastY,0f);
		Instantiate(targetPrefab,randomPos,Quaternion.identity,transform);
		lastY=randomPos.y;
	}
	public void SpawnTargets(){
		lastY=0;
        for(int i=0;i<targetNum;i++){
        	Spawn();
        }
	}
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
