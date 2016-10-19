using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Controlador : MonoBehaviour {

	//Declara um Singleton de Classe Controlador
    public static Controlador Singleton;

	//Cria uma nova instancia da Classe PlayerData
    PlayerData data = new PlayerData();
	
	//Variavel de gerenciamento de Pause
    public bool paused;
	
	//Declara clipes de Audio para serem tocados pelo Audio Source
    public AudioClip clique;
	
	//Menus
    public Canvas pauseMenu;
	public Canvas optionsMenu;
    public Canvas quitMenu;
    public Canvas helpMenu;
	
	//Declaração dos recursos
    public int pontos;
    public int vidas;
	
	//Estatísticas da partida
	public int pontosGanhos;
	public int vidasPerdidas;
    
	//Atribuição dos textos da HUD
	public Text pontos_txt;
	public Text vidas_txt;

    // Use this for initialization
    void Awake() {
		
		//verifica se há um Singleton e caso não haja, se torna um
        if (Singleton == null) {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
		
		//Se já houver um Singleton, se destrói
        else if (Singleton != this) {
            Destroy(this.gameObject);
        }
        paused = false;
    }

    void Start() {
		
        //inicializando os recursos
        pontos = 0;
		vidas = 3;
		
		//inicializando os menus
        pauseMenu = pauseMenu.GetComponent<Canvas>();
        optionsMenu = optionsMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        helpMenu = helpMenu.GetComponent<Canvas>();

		pauseMenu.enabled = false;
		optionsMenu.enabled = false;
		quitMenu.enabled = false;
        helpMenu.enabled = false;

		//inicializando as estatisticas
		pontosGanhos = 0;
		vidasPerdidas = 0;
    }

    // Update is called once per frame
    void FixedUpdate() {
        
		//imprimindo na tela os recursos
        pontos_txt.text = pontos.ToString();
        vidas_txt.text = vidas.ToString();
		
		//Ativa CheatCode ao pressionar F12
        if (Input.GetKey(KeyCode.F12)) { 
            //CheatCode, gera 1000 pontos e 1 vida extra
            pontos += 1000;
			vidas += 1;
            Debug.Log("CheatCode Detected!");
        }

        //Abre/fecha menu de Pause ao pressionar Esc
        if (Input.GetKey(KeyCode.Escape)) {
            if (paused) {
                Time.timeScale = 1;
                paused = false;
                pauseMenu.enabled = false;
				optionsMenu.enabled = false;
				quitMenu.enabled = false;
				helpMenu.enabled = false;
            }

            if(!paused) {
                Time.timeScale = 0;
                paused = true;
                pauseMenu.enabled = true;
				optionsMenu.enabled = false;
				quitMenu.enabled = false;
				helpMenu.enabled = false;
            }
        }
    }
	
	public void BotaoFecharMenu() {
		
		//Fecha o Menu de Pause
		Time.timeScale = 1;
		paused = false;
		pauseMenu.enabled = false;
		optionsMenu.enabled = false;
		quitMenu.enabled = false;
		helpMenu.enabled = false;
	}
	
    public void BotaoQuit() {
		
		//Abre janela com opçao para encerrar o jogo
        pauseMenu.enabled = false;
		optionsMenu.enabled = false;
		quitMenu.enabled = true;
		helpMenu.enabled = false;
    }
	
	public void ConfirmarQuit() {
		if(quitMenu.enabled) {
			Application.Quit();
			//Encerra o jogo
		}
	}

	public void BotaoVoltar() {
		//Desativa todos os menus, voltando para o Menu Principal
        pauseMenu.enabled = true;
		optionsMenu.enabled = false;
		quitMenu.enabled = false;
        helpMenu.enabled = false;
    }
	
	public void BotaoAjuda() {
		//Ativa o menu de Help
        pauseMenu.enabled = false;
		optionsMenu.enabled = false;
		quitMenu.enabled = false;
        helpMenu.enabled = true;
    }
	
    public void Save() {
		
		//Cria arquivo texto com criptografia binária
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		//Salva recursos da partida
        data.pontos = pontos;
        data.vidas = vidas;

		//Salva estatisticas da partida
		data.pontosGanhos = pontosGanhos;
		data.vidasPerdidas = vidasPerdidas;
		
		//Salva configurações de Audio
		data.MusicVolume = ControladorAudio.Singleton.Music.value;
		data.SFXVolume = ControladorAudio.Singleton.SFX.value;

        bf.Serialize(file, data);
        file.Close();
    }

    
	public void Load() {
		
		//verifica se existe arquivo de save
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
			
			BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            
			//Carrega recursos de partida salva
            pontos = data.pontos;
			vidas = data.vidas;
			
			//Carrega estatisticas de partida salva
			pontosGanhos = data.pontosGanhos;
			vidasPerdidas = data.vidasPerdidas;
			
			//Carrega configurações de Audio
			ControladorAudio.Singleton.Music.value = data.MusicVolume;
			ControladorAudio.Singleton.SFX.value = data.SFXVolume;

            file.Close();
        }
    }
}

//Classe PlayerData para salvar e carregar partidas e estatisticas
[Serializable]
public class PlayerData
{
    //Recursos do Jogo
    public int pontos;
    public int vidas;
	
	//Estatísticas da partida
	public int pontosGanhos;
	public int vidasPerdidas;
	
	//Configurações do Audio
	public float MusicVolume;
	public float SFXVolume;
}