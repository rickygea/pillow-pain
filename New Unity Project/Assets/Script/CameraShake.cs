using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	public Transform kamera;
	public float shake = 0.7f;
	[HideInInspector]
	public Vector3 posawal;
	public float durasi;
	private float waktu;
	private CameraShake self;
    private void Start()
    {
		self = this.GetComponent<CameraShake>();
	}

    void OnEnable()
	{
		waktu = durasi;
	}

	void FixedUpdate()
	{
		if (waktu > 0f)
		{
			waktu -= Time.deltaTime;
			kamera.localPosition = posawal + Random.insideUnitSphere * shake;
		}
	}
}