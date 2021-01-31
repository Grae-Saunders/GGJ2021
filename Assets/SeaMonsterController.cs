using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMonsterController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 startPosition;
    Vector3 adjustedStartPosition;
    public Vector3 endPosition;
    Vector3 adjustedEndPosition;

    public float submergeDepth;
    public float travelSpeed;
    public float normalDepth;

    public float submergeDistance;

    public void StartLurking(float startPos)
    {
        adjustedEndPosition = new Vector3(endPosition.x, submergeDepth, endPosition.z);
        adjustedStartPosition = new Vector3(startPosition.x, submergeDepth, startPosition.z);
        
        transform.position = Vector3.Lerp(startPosition,endPosition,startPos);
    }


    void Update()
    {
        var newPostion = Vector3.MoveTowards(transform.position, endPosition, travelSpeed / 100);
        var distanceToEnd = Vector3.Distance(transform.position, adjustedEndPosition);
        var distanceToStart = Vector3.Distance(transform.position, adjustedStartPosition);
        if (distanceToStart < submergeDistance)
        {
            var newDepth = Mathf.Lerp(submergeDepth, normalDepth, distanceToStart / submergeDistance);
            newPostion.y = newDepth;
        }
        else if (distanceToEnd < submergeDistance)
        {
            var newDepth = Mathf.Lerp(normalDepth, submergeDepth, (submergeDistance - distanceToEnd) / submergeDistance);
            newPostion.y = newDepth;
        }
        else
        {
            newPostion.y = normalDepth;
        }
        transform.position = newPostion;

        if (Vector3.Distance(transform.position, adjustedEndPosition) < 0.1)
        {
            ResetMonster();
        }
    }
    private void ResetMonster()
    {
        var resetPos = adjustedStartPosition;
        resetPos.y = submergeDepth;
        transform.position = resetPos;

    }
}
