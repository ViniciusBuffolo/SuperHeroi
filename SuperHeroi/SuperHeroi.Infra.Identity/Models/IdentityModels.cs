﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SuperHeroi.Application.Interfaces;

namespace SuperHeroi.Infra.Identity.Models
{
    public class ApplicationUser : IdentityUser, IDisposable
    {
        private readonly IHeroiAppService _heroiAppService;

        public ApplicationUser(IHeroiAppService heroiAppService)
        {
            _heroiAppService = heroiAppService;
            Clients = new Collection<Client>();
        }
        public ApplicationUser()
        {
            Clients = new Collection<Client>();
        }

        public virtual ICollection<Client> Clients { get; set; }

        [NotMapped]
        public string CurrentClientId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, ClaimsIdentity ext = null)
        {
            // Observe que o authenticationType precisa ser o mesmo que foi definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(CurrentClientId))
            {
                claims.Add(new Claim("AspNet.Identity.ClientId", CurrentClientId));
            }

            //  Adicione novos Claims aqui //

            // Claim Id do usuário logado
            claims.Add(new Claim("ClaimIdUsuario", userIdentity.GetUserId()));

            //var heroi = _heroiAppService.GetAll();

            claims.Add(new Claim("ClaimIdHeroi", Guid.NewGuid().ToString()));

            // Adicionando Claims externos capturados no login
            if (ext != null)
            {
                await SetExternalProperties(userIdentity, ext);
            }

            // Gerenciamento de Claims para informaçoes do usuario
            //claims.Add(new Claim("AdmRoles", "True"));

            userIdentity.AddClaims(claims);

            return userIdentity;
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, List<string> listClaims, ClaimsIdentity ext = null)
        {
            // Observe que o authenticationType precisa ser o mesmo que foi definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(CurrentClientId))
            {
                claims.Add(new Claim("AspNet.Identity.ClientId", CurrentClientId));
            }

            //  Adicione novos Claims aqui //

            // Claim Id do usuário logado
            claims.Add(new Claim("ClaimIdUsuario", userIdentity.GetUserId()));

            //claims.Add(new Claim("ClaimIdHeroi", Guid.NewGuid().ToString()));
            foreach (var item in listClaims)
            {
                claims.Add(new Claim("ClaimIdHeroi", item));
            }

            // Adicionando Claims externos capturados no login
            if (ext != null)
            {
                await SetExternalProperties(userIdentity, ext);
            }

            // Gerenciamento de Claims para informaçoes do usuario
            //claims.Add(new Claim("AdmRoles", "True"));

            userIdentity.AddClaims(claims);

            return userIdentity;
        }

        private async Task SetExternalProperties(ClaimsIdentity identity, ClaimsIdentity ext)
        {
            if (ext != null)
            {
                var ignoreClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
                // Adicionando Claims Externos no Identity
                foreach (var c in ext.Claims)
                {
                    if (!c.Type.StartsWith(ignoreClaim))
                        if (!identity.HasClaim(c.Type, c.Value))
                            identity.AddClaim(c);
                }
            }
        }

        public void Dispose()
        {
            _heroiAppService.Dispose();
        }
    }
}