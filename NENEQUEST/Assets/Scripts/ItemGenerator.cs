using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;

	public GameObject[] objItem;
	Coroutine generateItemC;

	public void Initialize ()
	{
		ClearGeneratedObject ();
		generateItemC = StartCoroutine (GenerateItem ());
	}

	public void Terminate ()
	{
		StopCoroutine (generateItemC);
		ClearGeneratedObject ();
	}

	void ClearGeneratedObject ()
	{
		foreach (Transform t in transform)
			Destroy (t.gameObject);
	}

	IEnumerator GenerateItem ()
	{
		yield return  new WaitForSeconds (cfg.ITEM_0_FIRST_DELAY);
		while (true) {
			CreateItem (cfg.ITEM_0_IDX, GetRandomPos ());
			yield return  new WaitForSeconds (Random.Range (15, 20));
		}
	}

	void CreateItem (int i, Vector3 position)
	{
		GameObject item = Instantiate (objItem [i]);
		item.transform.parent = transform;
		item.transform.position = position;
		Item0Action itemAction = item.GetComponent<Item0Action> ();
		itemAction.cfg = cfg;
		itemAction.Initialize ();
	}

	float GetRandomZ ()
	{
		return Random.Range (0f, cfg.FIELD_LEN_Z);
	}

	Vector3 AddNoiseZVector (Vector3 v, float min, float max)
	{
		v.z += Random.Range (min, max);
		v.z = Mathf.Clamp (v.z, 0, cfg.FIELD_LEN_Z);
		return v;
	}

	Vector3 GetRandomPos ()
	{
		return new Vector3 (cfg.FIELD_LEN_X, 0, GetRandomZ ());
	}
}
