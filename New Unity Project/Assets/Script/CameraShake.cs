using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	public Transform kamera;
	public float shake = 0.7f;
	 Vector3 posawal;
	public float durasi;
	private float waktu;
	private CameraShake self;
    private void Start()
    {
		posawal = new Vector3(0f, 0f, -10f);
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
		else {
			kamera.localPosition = posawal;
			self.enabled = false; }
	}
}