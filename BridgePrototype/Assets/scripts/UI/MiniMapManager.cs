using UnityEngine;
using System.Collections;

public class MiniMapManager : MonoBehaviour {

	public RenderTexture miniMapTexture;
	public Material miniMapMaterial;

	public RenderTexture miniMapTexture2;
	public Material miniMapMaterial2;

	public Texture frameTexture;
	public Texture frameTexture2;

	private float offset;
	void Awake()
	{
		offset = 10;

	}

	void OnGUI()
	{
		if (Event.current.type == EventType.Repaint) {
			Graphics.DrawTexture(new Rect(offset, offset, Screen.width * .15f, Screen.width * .15f), miniMapTexture, miniMapMaterial);
			Graphics.DrawTexture(new Rect(offset, offset, Screen.width * .15f, Screen.width * .15f), frameTexture2);
			Graphics.DrawTexture(new Rect(Screen.width - offset - Screen.width * .15f, offset, Screen.width * .15f, Screen.width * .15f), miniMapTexture2, miniMapMaterial2);
			Graphics.DrawTexture(new Rect(Screen.width - offset - Screen.width * .15f, offset, Screen.width * .15f, Screen.width * .15f), frameTexture);
		}
	}
}
