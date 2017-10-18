using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Dialogue : MonoBehaviour {

	private Text textComponent;
	public string[] DialogueString;

	public float SecondsBetweenCharacters = 0.05f;
	public float CharacterRateMultplier = .01f;

	private bool isStringRevealed = false;
	private bool isDialoguePlaying = false;
	private bool isEndDialogue = false;

	public GameObject ContinueIcon;
	public GameObject StopIcon;

	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text> ();
		textComponent.text = "";

		HideIcon ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("P1X")) {
			if(!isDialoguePlaying)
			{

				isDialoguePlaying = true;
				StartCoroutine(StartDialogue());
			}
		}
	}

	private IEnumerator StartDialogue()
	{
		int dialogueLength = DialogueString.Length;
		int currentDialogueIndex = 0;

		while (currentDialogueIndex < dialogueLength || !isStringRevealed) {
			if(!isStringRevealed)
			{
				isStringRevealed = true;
				StartCoroutine(DisplayString(DialogueString[currentDialogueIndex++]));

				if(currentDialogueIndex >= dialogueLength)
				{
					isEndDialogue = true;
				}
			}

			yield return 0;
		}

		while (true) {
			if(Input.GetButtonDown ("P1X"))
				break;
			yield return 0;
		}

		HideIcon ();
		isEndDialogue = false;
		isDialoguePlaying = false;;
	}
	private IEnumerator DisplayString(string stringToDisplay)
	{
		int stringLength = stringToDisplay.Length;
		int currentCharacterIndex = 0;

		textComponent.text = "";

		HideIcon ();
		while(currentCharacterIndex < stringLength)
		{
			textComponent.text += stringToDisplay[currentCharacterIndex];
			currentCharacterIndex++;

			if(currentCharacterIndex < stringLength)
			{
				if(Input.GetButtonDown ("P1X"))
				{
					yield return new WaitForSeconds(SecondsBetweenCharacters * CharacterRateMultplier);
				}
				else{
					yield return new WaitForSeconds(SecondsBetweenCharacters);
				}

			}
			else
			{
				break;
			}

		}
		ShowIcon ();
		while (true) {
			if(Input.GetButtonDown ("P1X"))
				break;
			yield return 0;
		}
		HideIcon ();
		isStringRevealed = false;

		textComponent.text = "";
	}

	private void HideIcon()
	{
		ContinueIcon.SetActive (false);
		StopIcon.SetActive (false);

	}

	private void ShowIcon()
	{
		if (isEndDialogue) {
			StopIcon.SetActive (true);
			return;
		}

		ContinueIcon.SetActive (true);
	}
}
