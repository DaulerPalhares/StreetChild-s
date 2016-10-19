using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    //Declarar páginas de Menu do tipo Canvas
    //public Canvas quitMenu; //Não implementado
    public Canvas optionsMenu;
    public Canvas creditsMenu;
    public Canvas helpMenu;
	public Canvas quitMenu;

	//Declara clipes de Audio para serem tocados pelo Audio Source
    public AudioClip clique;

    void Start() {
		
		//Declara Menus na classe Canvas
        quitMenu = quitMenu.GetComponent<Canvas>();
        optionsMenu = optionsMenu.GetComponent<Canvas>();
        creditsMenu = creditsMenu.GetComponent<Canvas>();
        helpMenu = helpMenu.GetComponent<Canvas>();

		//Inicializa todos os Menus como inativos
        quitMenu.enabled = false;
        optionsMenu.enabled = false;
        creditsMenu.enabled = false;
        helpMenu.enabled = false;

    }

	//Feito para definir o que acontece no menu principal e sub menus, funçoes dos botoes
	public void ConfirmarQuit() {
		
		//Confirma açao de encerrar o jogo
		if(quitMenu.enabled){
			Application.Quit();
			//Encerra o jogo
		}
	}
	public void BotaoQuit(){
		
		//Abre janela perguntando se realmente quer encerrar o jogo
		quitMenu.enabled = true;
        optionsMenu.enabled = false;
        creditsMenu.enabled = false;
        helpMenu.enabled = false;
	}
	public void BotaoVoltar() {
        
		//Desativa todos os menus, voltando para o Menu Principal
        quitMenu.enabled = false;
		optionsMenu.enabled = false;
        creditsMenu.enabled = false;
        helpMenu.enabled = false;
    }
	public void BotaoNewGame() {
        
		//Inicia um novo jogo
        SceneManager.LoadScene(1);
	}
	
	public void BotaoCarregar() {
        
		//Ativa o menu de Load (Saved) Game
        quitMenu.enabled = false;
		optionsMenu.enabled = false;
        creditsMenu.enabled = false;
        helpMenu.enabled = false;
    }
	/*
	public void LoadGame() {
        
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
			
            file.Close();
        }
	}*/
	
	public void BotaoOpçoes() {
        
		//Ativa o menu de Options
        quitMenu.enabled = false;
		optionsMenu.enabled = true;
        creditsMenu.enabled = false;
        helpMenu.enabled = false;
    }

	public void BotaoAjuda() {
        
		//Ativa o menu de Help
        quitMenu.enabled = false;
		optionsMenu.enabled = false;
        creditsMenu.enabled = false;
        helpMenu.enabled = true;
    }

	public void BotaoCreditos() {
        
		//Ativa o menu de Credits
        quitMenu.enabled = false;
		optionsMenu.enabled = false;
        creditsMenu.enabled = true;
        helpMenu.enabled = false;
    }
}
/*
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
	
}*/