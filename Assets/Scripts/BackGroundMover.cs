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
			// x��y�̒l��0 �` 1�Ń��s�[�g����悤�ɂ���
			var x = Mathf.Repeat(Time.time * offsetSpeed.x, maxLength);
			var y = Mathf.Repeat(Time.time * offsetSpeed.y, maxLength);
			var offset = new Vector2(x, y);
			material.SetTextureOffset(propName, offset);
		}
	}

	private void OnDestroy()
	{
		// �Q�[������߂���Ƀ}�e���A����Offset��߂��Ă���
		if (material)
		{
			material.SetTextureOffset(propName, Vector2.zero);
		}
	}
}
