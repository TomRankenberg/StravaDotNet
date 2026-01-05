using Microsoft.AspNetCore.Components.Authorization;

namespace StravaDotNet.Components.Services
{
    // Thin wrapper so existing components that inject AuthState keep working.
    public class AuthState : IDisposable
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private bool _isLoggedIn;

        public bool IsLoggedIn => _isLoggedIn;

        public event Action? AuthenticationStateChanged;

        public AuthState(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _authenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
            _ = RefreshAsync();
        }

        private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            await RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var auth = state.User?.Identity?.IsAuthenticated ?? false;
            if (_isLoggedIn != auth)
            {
                _isLoggedIn = auth;
                AuthenticationStateChanged?.Invoke();
            }
        }

        public async Task<bool> GetIsLoggedInAsync()
            => (await _authenticationStateProvider.GetAuthenticationStateAsync()).User?.Identity?.IsAuthenticated ?? false;

        public void Dispose()
        {
            _authenticationStateProvider.AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }
    }
}