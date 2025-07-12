using UnityEngine;
using UnityEngine.UI;

public class MuteToggleController : MonoBehaviour
{
	[Header("UI References")]
	[SerializeField] private Toggle toggle;
	[SerializeField] private Image targetImage;
	[SerializeField] private Sprite mutedSprite;
	[SerializeField] private Sprite unmutedSprite;
	[SerializeField] private GameObject adioManager;

	private const string PREF_MUTE = "Mute";
	private bool isMuted = true;

	private void Awake()
	{
		isMuted = PlayerPrefs.GetInt(PREF_MUTE, 0) == 1;
		toggle.SetIsOnWithoutNotify(!isMuted);
		UpdateIcon(!isMuted);
		toggle.onValueChanged.AddListener(OnToggleChanged);

		// Sync AudioManager
		adioManager.GetComponent<AudioVolumeManager>()._isMute = isMuted;
	}

	private void Update()
	{
		var audioScript = adioManager.GetComponent<AudioVolumeManager>();

		if (isMuted != audioScript._isMute)
		{
			isMuted = audioScript._isMute;
			toggle.SetIsOnWithoutNotify(!isMuted);
			UpdateIcon(!isMuted);
		}
	}

	public void OnToggleChanged(bool isOn)
	{
		isMuted = !isOn;

		PlayerPrefs.SetInt(PREF_MUTE, isMuted ? 1 : 0);
		UpdateIcon(isOn);

		var audioScript = adioManager.GetComponent<AudioVolumeManager>();
		audioScript._isMute = isMuted;
	}

	private void UpdateIcon(bool isMuted)
	{
		if (targetImage != null)
			targetImage.sprite = isMuted ? mutedSprite : unmutedSprite;
	}


	private void OnDestroy()
	{
		toggle.onValueChanged.RemoveListener(OnToggleChanged);
	}
}
