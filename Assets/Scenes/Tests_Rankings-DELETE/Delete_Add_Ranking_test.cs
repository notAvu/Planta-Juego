using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Add_Ranking_test : MonoBehaviour
{
    public RankingSaver rankingSaver;
    // Start is called before the first frame update
    void Start()
    {
        rankingSaver = GameObject.Find("Ranking_Saver").GetComponent<RankingSaver>();


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addRankingLines()
    {
        //rankingSaver.SetRankingList(new List<string>());


        rankingSaver.addRankingLine("EscenaPlantilla|510|crecencio");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|50|eustaquio");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|10|federico");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|100|aurelia");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|500|placido");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|150|juan");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|50|anastasia");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|25|picatoste");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|68|federico");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|86|pepe");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|15|francisco");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|20|manolo");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|810|ramon");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|600|paca");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|300|mariano");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|250|jesus");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|20|antonia");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|65|pedro");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|21|javier");
        Debug.Log("Linea añadida");
        rankingSaver.addRankingLine("EscenaPlantilla|68|lorenzo");
        

    }
}
