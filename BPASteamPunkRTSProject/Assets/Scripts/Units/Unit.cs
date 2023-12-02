using UnityEngine;

public class Unit : MonoBehaviour
{
    public int[] position = new int[] { 0, 0, 0 };
    public Vector3 positionAsVector3;
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        positionAsVector3 = new Vector3(position[0], position[1], position[2]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
