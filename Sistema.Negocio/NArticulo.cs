using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Negocio
{
    public class NArticulo
    {
        public static DataTable Listar()
        {
            DArticulo datos = new DArticulo();
            return datos.Listar();
        }

        public static DataTable Seleccionar()
        {
            DArticulo datos = new DArticulo();
            return datos.Seleccionar();
        }

        public static DataTable Buscar(string valor)
        {
            DArticulo datos = new DArticulo();
            return datos.Bucar(valor);
        }

        public static string Insertar(int Idcateogira, string Codigo, string Nombre, decimal Precio, int Stock, string Descripcion, string Imagen)
        {
            DArticulo datos = new DArticulo();

            string Existe = datos.Existe(Nombre);

            if (Existe.Equals("1"))
            {
                return "El artículo ya existe";
            }
            else
            {
                Articulo obj = new Articulo();
                obj.IdCategoria = Idcateogira;
                obj.Codigo = Codigo;
                obj.Nombre = Nombre;
                obj.PrecioVenta = Precio;
                obj.Stock = Stock;
                obj.Descripcion = Descripcion;
                obj.Imagen = Imagen;
                return datos.Insertar(obj);
            }
        }

        public static string Actualizar(int Id, string nombreAnterior, int Idcateogira, string Codigo, string Nombre, decimal Precio, int Stock, string Descripcion, string Imagen)
        {
            DArticulo datos = new DArticulo();
            Articulo obj = new Articulo();

            string Existe = datos.Existe(Nombre);

            if (nombreAnterior.Equals(Nombre))
            {
                obj.IdArticulo = Id;
                obj.IdCategoria = Idcateogira;
                obj.Codigo = Codigo;
                obj.Nombre = Nombre;
                obj.PrecioVenta = Precio;
                obj.Stock = Stock;
                obj.Descripcion = Descripcion;
                obj.Imagen = Imagen;
                return datos.Actualizar(obj);
            }
            else
            {
                if (Existe.Equals("1"))
                {
                    return "El artículo ya existe";
                }
                else
                {
                    obj.IdArticulo = Id;
                    obj.IdCategoria = Idcateogira;
                    obj.Codigo = Codigo;
                    obj.Nombre = Nombre;
                    obj.PrecioVenta = Precio;
                    obj.Stock = Stock;
                    obj.Descripcion = Descripcion;
                    obj.Imagen = Imagen;
                    return datos.Actualizar(obj);
                }
            }
        }

        public static string Activar(int Id)
        {
            DArticulo datos = new DArticulo();
            return datos.Activar(Id);
        }

        public static string Desactivar(int Id)
        {
            DArticulo datos = new DArticulo();
            return datos.Descativar(Id);
        }

        public static string Eliminar(int Id)
        {
            DArticulo datos = new DArticulo();
            return datos.Eliminar(Id);
        }
    }
}
