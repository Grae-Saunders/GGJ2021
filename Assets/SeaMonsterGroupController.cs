using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMonsterGroupController : MonoBehaviour
{
    public SeaMonsterController north1;
    public SeaMonsterController north2;
    public SeaMonsterController south1;
    public SeaMonsterController south2;
    public SeaMonsterController east1;
    public SeaMonsterController east2;
    public SeaMonsterController west1;
    public SeaMonsterController west2;
    void Start()
    {
        var startPos = Random.Range(0f, 1f);
        var altStart = startPos > 0.5 ? startPos -0.5f : startPos + 0.5f;
        north1.StartLurking(startPos);
        north2.StartLurking(altStart);
        south1.StartLurking(startPos);
        south2.StartLurking(altStart);
        east1.StartLurking(startPos);
        east2.StartLurking(altStart);
        west1.StartLurking(startPos);
        west2.StartLurking(altStart);




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
