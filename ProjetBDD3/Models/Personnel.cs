using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;

namespace ProjetBDD3.Models
{
    public class Personnel
    {
        public int Matricule { get; set; }
        public string Nom { get; set; }
        public string Categorie { get; set; }
        public string Fonction { get; set; }
        public int NbHeuresVol { get; set; }

        public Personnel(int matricule, string nom, string categorie, string fonction)
        {
            Matricule = matricule;
            Nom = nom;
            Categorie = categorie;
            Fonction = fonction;
        }

        public Personnel(int matricule, string nom, string categorie, int nbHeuresVol)
        {
            Matricule = matricule;
            Nom = nom;
            Categorie = categorie;
            NbHeuresVol = nbHeuresVol;
        }


        public Personnel()
        {

        }

        public List<Personnel> GetAllPersonnel()
        {
            string req = "select * from personnel";
            OracleConnection conn = new OracleConnection(new VariablesGlobales().connexion);
            List<Personnel> reponse = new List<Personnel>();
            try
            {
                conn.Open();
                OracleCommand commande = new OracleCommand(req, conn);
                commande.CommandType = CommandType.Text;
                OracleDataReader lecteur = commande.ExecuteReader();
                while (lecteur.Read())
                {
                    int matricule = lecteur.IsDBNull(0) ? 0 : lecteur.GetInt32(0);
                    string nom = lecteur.IsDBNull(1) ? "" : lecteur.GetString(1);
                    string categorie = lecteur.IsDBNull(2) ? "" : lecteur.GetString(2);
                    string fonction = lecteur.IsDBNull(3) ? "" : lecteur.GetString(3);
                    reponse.Add(new Personnel(matricule, nom, categorie, fonction));
                }
            }
            catch (Exception)
            {
                throw;
            }
            conn.Close();
            return reponse;
        }

        public List<Personnel> ProcedureMembresEquipage(string noVol, string date)
        {
            List<Personnel> reponse = new List<Personnel>();
            string req = "SELECT Personnel.Matricule, Nom, Fonction, TotalHeuresVol(101, '"+noVol+"') FROM Personnel JOIN Equipage ON Personnel.Matricule = Equipage.Matricule WHERE NoVol = '" + noVol+ "' AND DateHeureDepart = to_date('"+date+"', 'DD/MM/YYYY HH24:MI:SS')";
            OracleConnection conn = new OracleConnection(new VariablesGlobales().connexion);
            try
            {
                conn.Open();
                OracleCommand commande = new OracleCommand(req, conn);
                commande.CommandType = CommandType.Text;
                OracleDataReader lecteur = commande.ExecuteReader();
                while (lecteur.Read())
                {
                    int matricule = lecteur.IsDBNull(0) ? 0 : lecteur.GetInt32(0);
                    string nom = lecteur.IsDBNull(1) ? "" : lecteur.GetString(1);
                    string categorie = lecteur.IsDBNull(2) ? "" : lecteur.GetString(2);
                    int nbHeuresVol = lecteur.IsDBNull(3) ? 0 : lecteur.GetInt32(3);
                    reponse.Add(new Personnel(matricule, nom, categorie, nbHeuresVol));
                }

            }
            catch (Exception)
            {
                throw;
            }
            return reponse;
        }
    }
}