using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomStructures.AVL_Tree;
using LogicaOdontologia.DTO;
namespace LogicaOdontologia.Entidad
{
   public class BaseDeDatos
    {
        private static BaseDeDatos instance = null;
        /// <summary>
        /// this method use to compare the 2 abstract element for posision un the Tree
        /// </summary>
        /// <param name="NewValue">Value for insert</param>
        /// <param name="OldValue">Curent value in the node</param>
        /// <returns>if "NewValue" is greater than "OldValue" return 1 else if "NewValue" is less than "OldValue" return 1 else if "NewValue" is equal at "Oldvalue" return 0/returns>
        public static  int CompareTo(PacienteDTO NewValue, PacienteDTO OldValue) {
            if (NewValue.DPI < OldValue.DPI)
            {
                return -1;
            }
            else if (NewValue.DPI > OldValue.DPI) {
                return 1;
            }
            return 0;
        }
        public AVLTree<PacienteDTO> ArbolPacientes = new AVLTree<PacienteDTO>(BaseDeDatos.CompareTo);
        
        /// <summary>
        /// Return the instance of the class BaseDeDatos
        /// </summary>
        public static BaseDeDatos Instance
        {
            get {
                if (instance == null) {
                    instance = new BaseDeDatos();
                }
                return instance;
            }
        }

        public byte[] DescargarLog() {
            return ArbolPacientes.GuardarLog();
        }
    }
}
