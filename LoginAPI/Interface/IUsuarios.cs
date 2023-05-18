using LoginAPI.Models;

namespace LoginAPI.Interface
{
    public interface IUsuarios
    {
        public List<Usuario> ObtenerUsuarios();
        public Usuario ObtenerUsuario(int id);
        public void AgregarUsuario(Usuario usuario,string confirmacionClave);
        public void ActualizarUsuario(ActualizarClaveInputModel usuario);
        public Usuario EliminarUsuario(int id);
        public bool isUsuario(int id);
        public Usuario ObtenerUsuario(string correo, string clave);
    }
}
