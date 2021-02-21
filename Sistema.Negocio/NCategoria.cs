using System.Data;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Negocio
{
    public class NCategoria
    {
        public static DataTable Listar()
        {
            DCategoria datos = new DCategoria();
            return datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DCategoria datos = new DCategoria();
            return datos.Bucar(valor);
        }

        public static string Insertar(string Nombre, string Descripcion)
        {
            DCategoria datos = new DCategoria();

            string Existe = datos.Existe(Nombre);

            if (Existe.Equals("1"))
            {
                return "La categoria ya existe";
            }
            else
            {
                Categoria obj = new Categoria();
                obj.Nombre = Nombre;
                obj.Descripcion = Descripcion;
                return datos.Insertar(obj);
            }
        }

        public static string Actualizar(int Id, string nombreAnterior, string Nombre, string Descripcion)
        {
            DCategoria datos = new DCategoria();
            Categoria obj = new Categoria();

            string Existe = datos.Existe(Nombre);

            if (nombreAnterior.Equals(Nombre))
            {
                obj.IdCategoria = Id;
                obj.Nombre = Nombre;
                obj.Descripcion = Descripcion;
                return datos.Actualizar(obj);
            }
            else
            {
                if (Existe.Equals("1"))
                {
                    return "La categoria ya existe";
                }
                else
                {
                    obj.IdCategoria = Id;
                    obj.Nombre = Nombre;
                    obj.Descripcion = Descripcion;
                    return datos.Actualizar(obj);
                }
            }
        }

        public static string Activar(int Id)
        {
            DCategoria datos = new DCategoria();
            return datos.Activar(Id);
        }
        public static string Desactivar(int Id)
        {
            DCategoria datos = new DCategoria();
            return datos.Descativar(Id);
        }

        public static string Eliminar(int Id)
        {
            DCategoria datos = new DCategoria();
            return datos.Eliminar(Id);
        }
    }
}
