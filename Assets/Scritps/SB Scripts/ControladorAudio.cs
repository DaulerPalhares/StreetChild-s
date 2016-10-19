using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorAudio : MonoBehaviour {

	public static ControladorAudio Singleton;

	public float volumeSFX;
	public float volumeMusic;
	public float auxSFX;
	public float auxMusic;

	public Slider SFX;
	public Slider Music;
	
	public bool Mudo;


	void Awake(){

		Singleton = this;
		volumeSFX = 1;
		volumeMusic = 1;
		auxSFX = 1;
		auxMusic = 1;
		Mudo = false;
	}

	void FixedUpdate(){
		
		volumeSFX = SFX.value;
		volumeMusic = Music.value;
	}
	
	public void BotaoMudo(){
				
		if(Mudo){
			Music.value = auxMusic;
			SFX.value = auxSFX;
			Mudo = false;
		}
		
		if(!Mudo){
			auxMusic = Music.value;
			Music.value = 0;
			auxSFX = SFX.value;
			SFX.value = 0;
			Mudo = true;
		}
	}

}