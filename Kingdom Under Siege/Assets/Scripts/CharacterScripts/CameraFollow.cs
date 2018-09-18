using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    //Cameras target which is the player
    private Transform target;
    //Max and min value of the camera
    private float xMax, xMin, yMin, YMax;
    //Referance to the ground tilemap
    [SerializeField]
    private Tilemap tilemap;
    //Player reference
    private Player player;

    // Use this for initialization
	void Start ()
    {
        //creates reference to the player
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //references to the play script
        player = target.GetComponent<Player>();
        //calcutes max and min postion
        Vector3 minTile = tilemap.CellToWorld(tilemap.cellBounds.min);
        Vector3 maxTile = tilemap.CellToWorld(tilemap.cellBounds.max);
        //sets camera limits
        SetLimits(minTile, maxTile);
        //sets the limit of the player
        player.SetLimits(minTile, maxTile);
	}

    private void LateUpdate()
    {
        //make the camera stop on the edge of the world
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, YMax), -10);
    }

    //sets the limits for the camera so it dosent go over the edge
    private void SetLimits(Vector3 minTile, Vector3 maxTile)
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        xMin = minTile.x + width / 2;
        xMax = maxTile.x - width / 2;

        yMin = minTile.y + height / 2;
        YMax = maxTile.y - height / 2;

    }

}

