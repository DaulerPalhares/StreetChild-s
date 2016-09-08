using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour {

    #region Private Vars
    bool isInGame;
    [SerializeField]
    private Controlador gameControler;
    [SerializeField]
    private GameObject Principal;
    [SerializeField]
    private GameObject Opcoes;
    [SerializeField]
    private GameObject Creditos;
    [SerializeField]
    private GameObject gameOverCanvasObj;
    [SerializeField]
    private GameObject pauseCanvasObj;
    #endregion

    #region Public Vars
    public Text efeitoTxt;
    public Text musicaTxt;
    public bool inventario;
    #endregion

	void Start()
    {
        if(gameControler == null)
        {
            Debug.LogError("Atribua o controlador no inspector");
        }
        if (isInGame)
        {
            pauseCanvasObj.SetActive(false);
            gameOverCanvasObj.SetActive(false);
        }
        else
        {
            Principal.SetActive(true);
            Opcoes.SetActive(false);
            Creditos.SetActive(false);
        }
    }

    public void OpenMenu(GameObject x)
    {
        if (isInGame)
        {
            pauseCanvasObj.SetActive(false);
            gameOverCanvasObj.SetActive(false);
        }
        else
        {
            Principal.SetActive(false);
            Opcoes.SetActive(false);
            Creditos.SetActive(false);
        }
        x.SetActive(true);
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Sair()
    {
        Application.Quit();
    }
    /// <summary>
    /// Funcao para mudar a musica ou efeito
    /// </summary>
    /// <param name="x">o que sera mudado Musica ou Efeito</param>
    public void MudarCfg(string x)
    {
        if (x == "Musica")
        {
            gameControler.musica = !gameControler.musica;
            if (!gameControler.musica)
            {
                musicaTxt.text = "Musicas : ON";
            }
            else
            {
                musicaTxt.text = "Musicas : OFF";
            }
        }
        if (x == "Efeito")
        {

            gameControler.efeito = !gameControler.efeito;
            if (!gameControler.efeito)
            {
                efeitoTxt.text = "Efeitos : ON";
                gameControler.effectsPlayer.mute = false;
            }
            else
            {
                efeitoTxt.text = "Efeitos : OFF";
                gameControler.effectsPlayer.mute = true;
            }
        }
    }
}
