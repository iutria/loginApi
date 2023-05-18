using LoginAPI.Interface;
using LoginAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarios _IUsuarios;

        public UsuarioController(IUsuarios IUsuarios)
        {
            _IUsuarios = IUsuarios;
        }

        [AllowAnonymous]
        [HttpPost, Route("RegistrarUsuario")]
        public async Task<ActionResult> Post(UsuarioInputModel usuarioInputModel)
        {
            try
            {
                Usuario usuario = new Usuario(usuarioInputModel);
                usuario.Rol = 2;
                usuario.Estado = 1;
                _IUsuarios.AgregarUsuario(usuario, usuarioInputModel.ConfirmacionClave);
                return Ok("Registrado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// obtener usuarios, solo los puede obtener el administrador con el rol 1
        [HttpGet]
        [Authorize(Roles = "1")]
        public IEnumerable<UsuarioViewModel> Gets()
        {
            var usuarios = _IUsuarios.ObtenerUsuarios().Select(p => new UsuarioViewModel(p));
            return usuarios;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioViewModel>> Get(int id)
        {
            var usuario = await Task.FromResult(_IUsuarios.ObtenerUsuario(id));
            if (usuario == null)
            {
                return NotFound();
            }
            return new UsuarioViewModel(usuario);
        }

        /// eliminar usuario
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!isUsuario(id)) return NotFound();

            var employee = _IUsuarios.EliminarUsuario(id);
            return Ok("Eliminado correctamente");
        }

        //modificar clave
        [AllowAnonymous]
        [HttpPut, Route("ActualizarClave")]
        public async Task<ActionResult> Put(ActualizarClaveInputModel usuario)
        {
            try
            {
                _IUsuarios.ActualizarUsuario(usuario);
                return Ok("Se guardaron los cambios");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private bool isUsuario(int id)
        {
            return _IUsuarios.isUsuario(id);
        }


    }
}
