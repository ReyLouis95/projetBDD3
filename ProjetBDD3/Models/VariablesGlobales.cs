using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetBDD3.Models
{
    public class VariablesGlobales
    {
        public string connexion = "DATA SOURCE = localhost:1521/orcl;PERSIST SECURITY INFO=True;USER ID = SYSTEM; PASSWORD=oracle";

        public VariablesGlobales()
        {

        }
    }
}