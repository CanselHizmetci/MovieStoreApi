using System;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DbOperations;
using MovieStoreApi.TokenOperations.Models;

namespace MovieStoreApi.Application.CustomerOperations.CreateCustomer
{
	public class RefreshTokenCommand
	{
        public string RefreshToken { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IMovieStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Token> Handle()
		{
            var customer = _context.Customers.FirstOrDefault(c => c.RefreshToken == RefreshToken && c.RefreshTokenExpireDate > DateTime.Now);
            if (customer is not null)
            {
                //Token yarat
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(customer);

                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Kullanıcı Adı - Şifre hatalı");
        }
	}
}

