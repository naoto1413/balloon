using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundMover : MonoBehaviour
{
	private const float maxLength = 1f;
	private const string propName = "_MainTex";

	[SerializeField]
	private Vector3 offsetSpeed;

	private Material material;

	private void Start()
	{
		if (GetComponent<Image>() is Image i)
		{
			material = i.material;
		}
	}

	private void Update()
	{
		if (material)
		{
			// xとyの値が0 ～ 1でリピートするようにする
			var x = Mathf.Repeat(Time.time * offsetSpeed.x, maxLength);
			var y = Mathf.Repeat(Time.time * offsetSpeed.y, maxLength);
			var offset = new Vector2(x, y);
			material.SetTextureOffset(propName, offset);
		}
	}

	private void OnDestroy()
	{
		// ゲームをやめた後にマテリアルのOffsetを戻しておく
		if (material)
		{
			material.SetTextureOffset(propName, Vector2.zero);
		}
	}
}
