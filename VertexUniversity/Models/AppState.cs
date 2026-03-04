using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace VertexUniversity.Models
{
    public class AppState
    {
        private readonly IJSRuntime _js;
        public AppState(IJSRuntime js) => _js = js;

        public event Action? OnChange;

        public bool IsLoggedIn { get; private set; } = true;
        public bool IsAdmin { get; private set; } = false;
        public string UserName { get; private set; } = "Vertex Student";
        public string UserEmail { get; private set; } = "student@vertex.edu";
        public List<string> Wishlist { get; private set; } = new();
        public List<string> RegisteredEvents { get; private set; } = new();

        public async Task InitializeAsync()
        {
            try
            {
                var registrations = await _js.InvokeAsync<string>("localStorage.getItem", "vertex_registrations");
                if (!string.IsNullOrEmpty(registrations))
                {
                    RegisteredEvents = registrations.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                
                var wishlist = await _js.InvokeAsync<string>("localStorage.getItem", "vertex_wishlist");
                if (!string.IsNullOrEmpty(wishlist))
                {
                    Wishlist = wishlist.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                // Check for saved admin session
                var adminSession = await _js.InvokeAsync<string>("localStorage.getItem", "vertex_admin_session");
                if (!string.IsNullOrEmpty(adminSession))
                {
                    IsAdmin = true;
                    IsLoggedIn = true;
                    UserName = "System Administrator";
                    UserEmail = adminSession;
                }
                else
                {
                    // Default student session
                    IsLoggedIn = true;
                    IsAdmin = false;
                    UserName = await _js.InvokeAsync<string>("localStorage.getItem", "vertex_user_name") ?? "Vertex Student";
                    UserEmail = await _js.InvokeAsync<string>("localStorage.getItem", "vertex_user_email") ?? "student@vertex.edu";
                }

                NotifyStateChanged();
            }
            catch { /* Fallback for SSR or errors */ }
        }

        public async Task UpdateStudentSession(string name, string email)
        {
            UserName = name;
            UserEmail = email;
            await _js.InvokeVoidAsync("localStorage.setItem", "vertex_user_name", name);
            await _js.InvokeVoidAsync("localStorage.setItem", "vertex_user_email", email);
            NotifyStateChanged();
        }

        public async Task<AuthResult> LoginAdmin(string email, string password)
        {
            try
            {
                var result = await _js.InvokeAsync<AuthResult>("firebaseAuth.signIn", email, password);
                if (result.Success)
                {
                    IsLoggedIn = true;
                    IsAdmin = true;
                    UserName = "System Administrator";
                    UserEmail = result.Email ?? email;
                    await _js.InvokeVoidAsync("localStorage.setItem", "vertex_admin_session", UserEmail);
                    NotifyStateChanged();
                }
                return result;
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, Error = ex.Message };
            }
        }

        public class AuthResult
        {
            public bool Success { get; set; }
            public string? Error { get; set; }
            public string? Email { get; set; }
        }

        public async Task ToggleWishlist(string eventId)
        {
            if (Wishlist.Contains(eventId)) Wishlist.Remove(eventId);
            else Wishlist.Add(eventId);
            
            await _js.InvokeVoidAsync("localStorage.setItem", "vertex_wishlist", string.Join(",", Wishlist));
            NotifyStateChanged();
        }

        public async Task AddRegistration(string eventId)
        {
            if (!RegisteredEvents.Contains(eventId))
            {
                RegisteredEvents.Add(eventId);
                await _js.InvokeVoidAsync("localStorage.setItem", "vertex_registrations", string.Join(",", RegisteredEvents));
                NotifyStateChanged();
            }
        }

        public bool IsRegistered(string eventId) => RegisteredEvents.Contains(eventId);

        public void SetLoginState(bool isLoggedIn, bool isAdmin, string userName, string userEmail)
        {
            IsLoggedIn = isLoggedIn;
            IsAdmin = isAdmin;
            UserName = userName;
            UserEmail = userEmail;
            NotifyStateChanged();
        }

        public async Task Logout()
        {
            IsLoggedIn = false;
            IsAdmin = false;
            UserName = string.Empty;
            UserEmail = string.Empty;
            Wishlist.Clear();
            RegisteredEvents.Clear();
            await _js.InvokeVoidAsync("firebaseAuth.logout");
            await _js.InvokeVoidAsync("localStorage.removeItem", "vertex_registrations");
            await _js.InvokeVoidAsync("localStorage.removeItem", "vertex_wishlist");
            await _js.InvokeVoidAsync("localStorage.removeItem", "vertex_admin_session");
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
