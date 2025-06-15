using CommandMaint.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Utility;

namespace CommandMaint.Authentication
{
    public interface IAuthentication
    {
        bool IsAuthenticatedChange { get; set; }
        event Action? OnChange;  // Evento para notificar cambios
        Task<bool> Authenticate(Usuarios user);
        Task<bool> IsAuthenticated();
        Task<UserSession> GetAuthenticated();
        Task<Usuarios> GetAuthenticatedUser();
        Task Logout();
        Task ExtendSession();
        //Task<List<vFormulariosUsuario>> GetVFormularios(short idUsuario);
        Task<bool> Existe(string login);
        Task<bool> Existe(string login, byte[] password);
        Task<Usuarios> Registro(string login);
        Task<bool> BloquearCuenta(string userName);
        //Task<bool> DesbloquearCuenta(vUsuarios User);
        Task<bool> SumarIntento(string userName);
        Task<Usuarios> BuscarUsuario(string correo);
        Task<bool> UpdatePassword(byte[] password, string correo, string login);
        Task<bool> AutorizedForm(byte IdFormulario, int IdUsuario);
        //Task<List<vPermisosUsuario>> GetPermisosUsuario(short IdUsuario, byte IdFormulario);
    }

    public class ServicesAuthentication : IAuthentication
    {
        private readonly IDbContextFactory<CommandContext> _context;
        private readonly ProtectedSessionStorage _sessionStorage;
        private int _sessionTimeoutMinutes = 0;
        private bool _isAuthenticated;
        private IGeneral _Gral;
        public event Action? OnChange;

        public ServicesAuthentication(IDbContextFactory<CommandContext> context, ProtectedSessionStorage sessionStorage, IGeneral Gral)
        {
            _context = context;
            _sessionStorage = sessionStorage;
            _Gral = Gral;

           //_sessionTimeoutMinutes=_Gral.ValidateInt(DAL_Parametros.Registro((byte)Enums.eParametros.TiempoEspera).Valor);  // Tiempo de expiración en minutos
        }

        private void NotifyStateChanged() => OnChange?.Invoke();  // Notificar a los suscriptores

        public bool IsAuthenticatedChange
        {
            get => _isAuthenticated;
            set
            {
                if (_isAuthenticated != value)
                {
                    _isAuthenticated = value;
                    NotifyStateChanged();  // Notificar solo si cambia
                }
            }
        }

        public async Task<bool> Authenticate(Usuarios user)
        {
            if (user != null)
            {
                using var conexion = _context.CreateDbContext();
                var userBD = await conexion.Usuarios
                    .Where(a => a.Login == user.Login && a.Activo)
                    .SingleOrDefaultAsync();

                if (userBD != null)
                {
                    UserSession uSession = new()
                    {
                        IdUsuario = userBD.IdUsuario,
                        NombreCompleto = userBD.NombreCompleto,
                        ExpirationTime = DateTime.UtcNow.AddMinutes(_sessionTimeoutMinutes)
                    };

                    await _sessionStorage.SetAsync("SESSIONID", uSession);
                    IsAuthenticatedChange = true;  // Actualiza y notifica
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> IsAuthenticated()
        {
            var storage = await _sessionStorage.GetAsync<UserSession>("SESSIONID");
            if (storage.Value != null && storage.Success)
            {
                if (DateTime.UtcNow <= storage.Value.ExpirationTime)
                {
                    IsAuthenticatedChange = true;
                    return true;
                }
                else
                {
                    await Logout();  // Cierra sesión si ha expirado
                }
            }
            IsAuthenticatedChange = false;
            return false;
        }

        public async Task<UserSession> GetAuthenticated()
        {
            var storage = await _sessionStorage.GetAsync<UserSession>("SESSIONID");
            return storage.Success && storage.Value != null ? storage.Value : new UserSession();
        }
        public async Task<Usuarios> GetAuthenticatedUser()
        {
            var storage = await _sessionStorage.GetAsync<UserSession>("SESSIONID");
            var result = storage.Success && storage.Value != null ? storage.Value : new UserSession();
            if (result != null)
            {
                using var conexion = _context.CreateDbContext();
                var Registro = await conexion.Usuarios.Where(a => a.IdUsuario == result.IdUsuario).SingleOrDefaultAsync();
                if (Registro != null)
                {
                    return Registro;
                }
            }
            return new();
        }

        public async Task Logout()
        {
            await _sessionStorage.DeleteAsync("SESSIONID");
            IsAuthenticatedChange = false;  // Actualiza y notifica
        }

        public async Task ExtendSession()
        {
            var storage = await _sessionStorage.GetAsync<UserSession>("SESSIONID");
            if (storage.Success && storage.Value != null)
            {
                storage.Value.ExpirationTime = DateTime.UtcNow.AddMinutes(_sessionTimeoutMinutes);
                await _sessionStorage.SetAsync("SESSIONID", storage.Value);
                IsAuthenticatedChange = true;  // Notificar sobre la extensión
            }
        }
        //public async Task<List<vFormulariosUsuario>> GetVFormularios(short IdUsuario)
        //{
        //    using var conexion = _context.CreateDbContext();
        //    var result = await conexion.vFormulariosUsuario.Where(a => a.IdUsuario == IdUsuario).ToListAsync();
        //    return result;
        //}
        public async Task<bool> Existe(string Login)
        {
            using var conexion = _context.CreateDbContext();
            return await conexion.Usuarios.Where(a => a.Login == Login && a.Activo).AnyAsync();
        }
        public async Task<bool> Existe(string Login, byte[] Password)
        {
            using var conexion = _context.CreateDbContext();
            return await conexion.Usuarios.Where(a => a.Login == Login && a.Password == Password && a.Activo).AnyAsync();
        }
        public async Task<Usuarios> Registro(string Login)
        {
            using var conexion = _context.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.Login == Login).SingleOrDefaultAsync();
            if (Registro != null) return Registro;
            return await Task.FromResult<Usuarios>(new());
        }
        public async Task<bool> BloquearCuenta(string UserName)
        {
            using var conexion = _context.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.Login.Equals(UserName)).SingleOrDefaultAsync();
            if (Registro != null)
            {
                Registro.Bloqueado = true;
                Registro.Intentos++;
                return await conexion.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(false);
        }
        //public async Task<bool> DesbloquearCuenta(vUsuarios User)
        //{
        //    using var conexion = _context.CreateDbContext();
        //    var Registro = await conexion.Usuarios.Where(a => a.IdUsuario == User.IdUsuario).SingleOrDefaultAsync();
        //    if (Registro != null)
        //    {
        //        Registro.Bloqueado = false;
        //        Registro.Intentos = 0;
        //        return await conexion.SaveChangesAsync() > 0;
        //    }
        //    return await Task.FromResult(false);
        //}
        public async Task<bool> SumarIntento(string UserName)
        {
            using var conexion = _context.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.Login.Equals(UserName)).SingleOrDefaultAsync();
            if (Registro != null)
            {
                Registro.Intentos++;
                return await conexion.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(false);
        }
        public async Task<Usuarios> BuscarUsuario(string Correo)
        {
            using var conexion = _context.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.Correo.Equals(Correo)).SingleOrDefaultAsync();
            if (Registro != null)
            {
                return Registro;
            }
            return await Task.FromResult<Usuarios>(new());
        }
        public async Task<bool> UpdatePassword(byte[] Password, string Correo, string Login)
        {
            using var conexion = _context.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.Correo.Equals(Correo) && a.Login.Equals(Login)).SingleOrDefaultAsync();
            if (Registro != null)
            {
                Registro.Password = Password;
                Registro.Intentos = 0;
                Registro.Bloqueado = false;
                return await conexion.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(false);
        }

        public Task<bool> AutorizedForm(byte IdFormulario, int IdUsuario)
        {
            throw new NotImplementedException();
        }
        //public async Task<bool> AutorizedForm(byte IdFormulario, int IdUsuario)
        //{
        //    using var conexion = _context.CreateDbContext();
        //    return await conexion.vFormulariosUsuario.Where(a => a.IdFormulario == IdFormulario && a.IdUsuario == IdUsuario).CountAsync() > 0;
        //}
        //public async Task<List<vPermisosUsuario>> GetPermisosUsuario(short IdUsuario, byte IdFormulario)
        //{
        //    using var conexion = _context.CreateDbContext();
        //    return await conexion.vPermisosUsuario.Where(a => a.IdUsuario == IdUsuario && a.IdFormulario == IdFormulario).ToListAsync();
        //}
    }

    public class UserSession
    {
        public short IdUsuario { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime ExpirationTime { get; set; }
    }
}
