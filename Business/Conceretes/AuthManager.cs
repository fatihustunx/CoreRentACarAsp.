using Business.Abstracts;
using Business.Constants;
using Core.Entities.Conceretes;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conceretes
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> RunToRegister(UserForRegisterDto userForRegisterDto)
        {
            var user = Register(userForRegisterDto);
            if (!user.Success)
            {
                return new ErrorDataResult<AccessToken>(user.Message);
            }

            var res = CreateAccessToken(user.Data);

            return res;
        }

        public IDataResult<AccessToken> RunToLogin(UserForLoginDto userForLoginDto)
        {
            var user = Login(userForLoginDto);
            if (!user.Success)
            {
                return new ErrorDataResult<AccessToken>(user.Message);
            }
            var res = CreateAccessToken(user.Data);

            return res;
        }

        private IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            var error = IsUserExists(userForRegisterDto.Email);

            if (!error.Success)
            {
                return new ErrorDataResult<User>(error.Message);
            }

            byte[] passwordSalt, passwordHash;
            HashingHelper.CreatePassword(userForRegisterDto.Password, out passwordSalt, out passwordHash);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(user);

            return new SuccessDataResult<User>(user, Messages.SuccessfulRegister);
        }

        private IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var user = _userService.GetByEmail(userForLoginDto.Email);

            if (user.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserIsNotFound);
            }

            if (!HashingHelper.VerifyPassword(userForLoginDto.Password, user.Data.PasswordSalt, user.Data.PasswordHash))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(user.Data, Messages.SuccessfulLogin);
        }

        private IResult IsUserExists(string email)
        {
            if (_userService.GetByEmail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        private IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateAccessToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}