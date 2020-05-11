using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetBDD3.Models
{
    public class Vol
    {
        public string NoVol { get; set; }
        public string Depart { get; set; }
        public string Arrivee { get; set; }

        public Vol()
        {

        }

        public Vol(string noVol, string depart, string arrivee)
        {
            NoVol = noVol;
            Depart = depart;
            Arrivee = arrivee;
        }

        public List<Vol> GetAllVols()
        {
            string req = "select * from vol";
            OracleConnection conn = new OracleConnection(new VariablesGlobales().connexion);
            List<Vol> reponse = new List<Vol>();
            try
            {
                conn.Open(); OracleCommand commande = new OracleCommand(req, conn);
                commande.CommandType = CommandType.Text;
                OracleDataReader lecteur = commande.ExecuteReader();
                while (lecteur.Read())
                {
                    string noVol = lecteur.IsDBNull(0) ? "" : lecteur.GetString(0);
                    string depart = lecteur.IsDBNull(1) ? "" : lecteur.GetString(1);
                    string arrivee = lecteur.IsDBNull(2) ? "" : lecteur.GetString(2);
                    reponse.Add(new Vol(noVol, depart, arrivee));
                }
            }
            catch (Exception)
            {
                throw;
            }
            conn.Close();
            return reponse;
        }

        public void ProcedureDepartVol(string noVol, string date, string heure, string dureeVol)
        {
            string req = "begin programmer('"+noVol+"', to_date('"+date+" "+heure+"', 'yyyy-MM-DD HH24-MI-SS'), "+dureeVol +"); end;";
            OracleConnection conn = new OracleConnection(new VariablesGlobales().connexion);
            try
            {
                conn.Open(); 
                OracleCommand commande = new OracleCommand(req, conn);
                commande.CommandType = CommandType.Text;
                OracleDataReader lecteur = commande.ExecuteReader();
            }
            catch (OracleException)
            {
                throw;
            }
            conn.Close();
        }
    }
}