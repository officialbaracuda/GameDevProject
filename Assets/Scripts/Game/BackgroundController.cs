using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private float speedFactor = 65;
    private float[] parallaxScales;

    private Transform cam;
    private Vector3 previousCameraPosition;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCameraPosition = cam.position;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = speedFactor / backgrounds[i].position.z  ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCameraPosition.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, Time.deltaTime);
        }

        previousCameraPosition = cam.position;
    }
}
