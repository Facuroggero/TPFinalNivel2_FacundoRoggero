using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace datos
{
    public class MarcaDatos
    {
        public List<TipoMarca> listar()
        {
            List<TipoMarca> lista = new List<TipoMarca> ();
            AccesoDatos datos = new AccesoDatos ();

            try
            {
                datos.setearConsulta("select Id, Descripcion from MARCAS");
                datos.ejecutarConsulta();

                while (datos.Lector.Read())
                {
                    TipoMarca aux = new TipoMarca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
