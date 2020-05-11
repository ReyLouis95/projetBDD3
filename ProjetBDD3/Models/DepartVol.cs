using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetBDD3.Models
{
    public class DepartVol
    {
        public string NoVol { get; set; }
        public DateTime Date { get; set; }
        public int DureeVol { get; set; }

        public DepartVol()
        {

        }

        public DepartVol(string noVol, DateTime date, int dureeVol)
        {
            NoVol = noVol;
            Date = date;
            DureeVol = dureeVol;
        }

        public List<DepartVol> GetAllDepartVols()
        {
            string req = "select * from departvol";
            OracleConnection conn = new OracleConnection(new VariablesGlobales().connexion);
            List<DepartVol> reponse = new List<DepartVol>();
            try
            {
                conn.Open();
                OracleCommand commande = new OracleCommand(req, conn);
                commande.CommandType = CommandType.Text;
                OracleDataReader lecteur = commande.ExecuteReader();
                while (lecteur.Read())
                {
                    string noVol = lecteur.IsDBNull(0) ? "" : lecteur.GetString(0);
                    DateTime date = lecteur.IsDBNull(1) ? Convert.ToDateTime("") : lecteur.GetDateTime(1);
                    int dureeVol = lecteur.IsDBNull(2) ? 0 : lecteur.GetInt32(2);
                    reponse.Add(new DepartVol(noVol, date, dureeVol));
                }
            }
            catch (Exception)
            {
                throw;
            }
            conn.Close();
            return reponse;
        }

        public void ProcedureAffecterPersonnel(string noVol, string date, int matricule)
        {
            string req = "begin AffecterPersonnel('" + noVol + "', to_date('"+date+ "', 'DD/MM/YYYY HH24:MI:SS'), " + matricule + "); end;";
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