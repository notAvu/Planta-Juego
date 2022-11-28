using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class RankingSaver : MonoBehaviour
{
    private string fileRoute;
    public static RankingSaver rankingSaver;
    private Dats rankingDats;
    private string playerName;
    private int points;

    /// <summary>
    /// En el awake inicializamos la ruta del archivo y si ya existe un gameObject con el componente rankingSaver eliminamos este si no establecemos que no se borre
    /// </summary>
    private void Awake()
    {
        if (rankingSaver == null)
        {
            rankingSaver = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        fileRoute = Application.persistentDataPath + "/ranking.dat";
    }

    /// <summary>
    /// se cargan los datos del ranking desde el fichero
    /// si no existen los datos se crea un nuevo objeto Dats
    /// </summary>
    void Start()
    {
        rankingDats = new Dats();
        Load();
        if(rankingDats is null)
        {
            rankingDats = new Dats();
        }
        playerName = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Este metodo guarda el ranking que tenemos en el scritp en el fichero
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fileRoute);
       
        bf.Serialize(file, rankingDats);
        file.Close();
    }

    /// <summary>
    /// Este metodo carga el ranking que tenemos en el fichero
    /// </summary>
    public void Load()
    {
        Dats dats = new Dats();
        if (File.Exists(fileRoute))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fileRoute, FileMode.Open);
            dats = (Dats)bf.Deserialize(file);
            
            file.Close();
            rankingDats = dats;
        }
    }

    /// <summary>
    /// Carga lo que hay en el fichero,
    /// a�ade una linea nueva,
    /// y guarda el fichero de nuevo
    /// </summary>
    /// <param name="line"> linea a a�adir en formato LEVEL|NNNN|NAME </param>
    public void addRankingLine(string line)
    {
        Load();
        rankingDats.AddRankingLine(line);
        Save();
    }

    /// <summary>
    /// Obtiene la lista del ranking desde el fichero
    /// </summary>
    /// <returns> lista de ranking</returns>
    public List<string> GetRankingList()
    {
        Load();
        return rankingDats.GetRankingList();
    }
    /// <summary>
    /// Carga el fichero,
    /// establece el nuevo ranking,
    /// guarda de nuevo
    /// </summary>
    /// <param name="rankingLinesToSave"> lista a guardar</param>
    public void SetRankingList(List<string> rankingLinesToSave)
    {
        Load();
        rankingDats.SetRankingList(rankingLinesToSave);
        Save();
    }

    /// <summary>
    /// carga el contendio de el fichero
    /// y muestra el ranking del nivel
    /// </summary>
    /// <param name="level"> nombre del nivel a mostrar</param>
    /// <returns> string con el ranking</returns>
    public string ShowLevelRank(string level)
    {
        Load();
        return rankingDats.ShowLevelRank(level);
    }

    /// <summary>
    /// Carga el contenido de el fichero
    /// y muestra el ranking de un nivel limitado a xlineas
    /// </summary>
    /// <param name="level">nombre del nivel</param>
    /// <param name="limit"> n�mero m�ximo de registros</param>
    /// <returns> string con el ranking</returns>
    public string ShowLevelRank(string level, int limit)
    {
        Load();
        return rankingDats.ShowLevelRank(level, limit);
    }

    /// <summary>
    /// Establece el nombre del jugador
    /// </summary>
    /// <param name="newName"> nombre del jugador</param>
    public void setPlayerName(string newName)
    {
        playerName = newName;
    }

    /// <summary>
    /// obtiene el nombre del jugador
    /// </summary>
    /// <returns>nombre del jugador</returns>
    public string getPlayerName()
    {
        return playerName;
    }

    /// <summary>
    /// Devuelve la puntuaci�n guardada
    /// </summary>
    /// <returns>puntuacion</returns>
    public int getPoints()
    {
        return points;
    }

    /// <summary>
    /// Calucla y establece la puntuaci�n en funcion de un tiempo y un tiempo maximo permitido
    /// si nos hemos pasado del maximo la puntuacion es 0
    /// </summary>
    /// <param name="time">tiempo que hemos tardado</param>
    /// <param name="max">tiempo maximo permitido</param>
    public void setPoints(float time, float max)
    {
        float result = time - max;
        if (time > max)
            points = 0;
        else
            points = (int)result;
    }

    public void saveCurrentRank(string levelName)
    {
        rankingDats.AddRankingLine(levelName + "|" + points + "|" + playerName);
    }


}
/// <summary>
/// Clase que se guarda en el fichero
/// </summary>
[Serializable]
class Dats
{

    /// <summary>
    /// formato de cada linea 
    /// LEVELNAME|XXXX|NICK
    /// Siendo LEVELNAME el nombre del nivel NICK el nomrbe del jugador y X la puntuaci�n
    /// </summary>
    private List<string> rankingLines;

    /// <summary>
    /// Constructor vac�o, la lista es una lista vac�a
    /// </summary>
    public Dats()
    {
        rankingLines = new List<string>();
    }
    /// <summary>
    /// Constructor que establece la lista proporcionada
    /// </summary>
    /// <param name="rankingLinesToSave"></param>
    public Dats(List<string> rankingLinesToSave)
    {
        this.rankingLines = rankingLinesToSave;
    }

    /// <summary>
    /// Devuelve la lista del ranking
    /// Antes de devolverla se ordena
    /// </summary>
    /// <returns> lista del rannking</returns>
    public List<string> GetRankingList()
    {
        ShortList();
        return rankingLines;
    }

    /// <summary>
    /// Establece la lista del ranking a la lista proporcionada
    /// </summary>
    /// <param name="rankingLinesToSave">Lista de ranking a guardar</param>
    public void SetRankingList(List<string> rankingLinesToSave)
    {
        this.rankingLines = rankingLinesToSave;
        
    }

    /// <summary>
    /// Establece una nueva lista de ranking
    /// se comprueba que la puntuacion tenga el formato correcto asi no falla el orden
    /// </summary>
    /// <param name="rankingLine"></param>
    public void AddRankingLine(string rankingLine)
    {
        
        string[] subs = rankingLine.Split('|');
        string puntuacion = subs[1];
        if (puntuacion.Length < 4)
        {
            switch (puntuacion.Length){
                case 1:
                    puntuacion = "000" + puntuacion;
                    break;
                case 2:
                    puntuacion = "00" + puntuacion;
                    break;
                case 3:
                    puntuacion = "0" + puntuacion;
                    break;
            }
        }
        this.rankingLines.Add(subs[0]+"|"+puntuacion+"|"+subs[2]);
    }

    /// <summary>
    /// Ordena el ranking usando el metodo short de la lista e invirtiendolo
    /// Como primero esta el nivel y despues la puntuacion se ordena por nivel, despues por puntuaci�n y despues por nombre alfabeticamente
    /// </summary>
    public void ShortList()
    {
        rankingLines.Sort();

        //como el short ordena de menor a mayor en caso de los numeros se debe invertir


        List<String> reverseList = new List<String>();

        string[] listaArray = rankingLines.ToArray();


        for (int i = listaArray.Length-1; i >= 0; i--)
        {
            reverseList.Add(listaArray[i]);
        }
        rankingLines = reverseList;


    }

    /// <summary>
    /// Devuelve un texto con el ranking del nivel actual
    /// </summary>
    /// <param name="level">nombre del nivel</param>
    /// <returns>texto del rankig</returns>
    public string ShowLevelRank(string level)
    {
        ShortList();
        String rankList = "";
        int rankOrder = 1;

        foreach ( string line in rankingLines)
        {

            string[] subs = line.Split('|');
            if (subs[0] == level)
            {
                rankList += rankOrder +".- "+subs[2] + " - " + subs[1] + "\n";
                rankOrder++;
            }
        }

        return rankList;
    }

    /// <summary>
    /// Devuelve un texto con el ranking del nivel actual limitado a un n�mero de lineas
    /// </summary>
    /// <param name="level">nombre del nivel actual</param>
    /// <param name="limit">lieas maximas a mostrar</param>
    /// <returns></returns>
    public string ShowLevelRank(string level, int limit)
    {
        ShortList();
        String rankList = "";
        int rankOrder = 1;

        foreach (string line in rankingLines)
        {

            string[] subs = line.Split('|');
            if (subs[0] == level && rankOrder <= limit)
            {
                rankList += rankOrder + ".- " + subs[2] + " - " + subs[1] + "\n";
                rankOrder++;
            }
        }

        return rankList;
    }

}
