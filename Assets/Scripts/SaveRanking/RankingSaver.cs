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
        Load();
        if(rankingDats is null)
        {
            rankingDats = new Dats();
        }
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
        if (File.Exists(fileRoute))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fileRoute, FileMode.Open);
            Dats dats = (Dats)bf.Deserialize(file);
            
            file.Close();
            rankingDats = dats;
        }
    }

}
[Serializable]
class Dats
{

    /// <summary>
    /// formato de cada linea 
    /// LEVELNAME|X|NICK
    /// Siendo LEVELNAME el nombre del nivel NICK el nomrbe del jugador y X la puntuación
    /// </summary>
    private List<string> rankingLines;

    /// <summary>
    /// Constructor vacío, la lista es una lista vacía
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
        rankingLines.Sort();
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
    /// </summary>
    /// <param name="rankingLine"></param>
    public void AddRankingLine(string rankingLine)
    {
        this.rankingLines.Add(rankingLine);
    }

    /// <summary>
    /// Ordena el ranking
    /// Como primero esta el nivel y despues la puntuacion se ordena por nivel, despues por puntuación y despues por nombre alfabeticamente
    /// </summary>
    public void ShortList()
    {
        rankingLines.Sort();
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
    /// Devuelve un texto con el ranking del nivel actual limitado a un número de lineas
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
