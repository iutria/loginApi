using LoginAPI.Interface;
using LoginAPI.Models;
using LoginAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Services
{
    public class UsuarioService : IUsuarios
    {
        readonly ContextBD _dbContext;

        public UsuarioService(ContextBD dbContext)
        {
            this._dbContext = dbContext;
        }
        public void ActualizarUsuario(ActualizarClaveInputModel nuevos)
        {
            try
            {

                


                Usuario? usuario = ObtenerUsuario(nuevos.Correo, nuevos.ClaveActual);

                if (usuario == null)
                {
                    throw new Exception("usuario y/o contraseña no validos");
                }
                if (nuevos.NuevaClave != nuevos.ConfirmacionClave)
                {
                    throw new Exception("La clave nueva no es igual a la confirmación");
                }

                usuario.Clave = nuevos.ConfirmacionClave;

                _dbContext.Entry(usuario).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void AgregarUsuario(Usuario usuario, string confimacionClave)
        {
            try
            {
                if (usuario.Clave==confimacionClave)
                {
                    _dbContext.Usuarios.Add(usuario);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Las contraseñas no coinciden");
                }
            }
            catch
            {
                throw;
            }
        }

        public Usuario EliminarUsuario(int id)
        {
            try
            {
                Usuario? usuario = _dbContext.Usuarios.Find(id);

                if (usuario != null)
                {
                    _dbContext.Usuarios.Remove(usuario);
                    _dbContext.SaveChanges();
                    return usuario;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool isUsuario(int id)
        {
            return _dbContext.Usuarios.Any(e => e.Id == id);
        }

        public Usuario ObtenerUsuario(string correo, string clave)
        {
            List<Usuario> Usuarios = ObtenerUsuarios();

            foreach (Usuario usuario in Usuarios)
            {
                if(usuario.Correo == correo && usuario.Clave == clave)
                {
                    return usuario;
                }
            }
            return null;
        }

        public Usuario ObtenerUsuario(int id)
        {
            try
            {
                Usuario? usuario = _dbContext.Usuarios.Find(id);
                if (usuario != null)
                {
                    return usuario;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            try
            {
                return _dbContext.Usuarios.ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
