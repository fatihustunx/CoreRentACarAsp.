using Business.Abstracts;
using Business.Constants;
using Core.Entities.Conceretes;
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
        IUserService _userService;
        ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateAccessToken(user, claims);

            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var isExist = UserExist(userForLoginDto.Email);
            if(isExist.Success)
            {
                return new ErrorDataResult<User>(isExist.Message);
            }

            var user = _userService.GetByEmail(userForLoginDto.Email).Data;
            
            var isVerify = HashingHelper.VerifyPassword(userForLoginDto.Password, user.PasswordSalt, user.PasswordHash);
            if(!isVerify) { return new ErrorDataResult<User>(Messages.PasswordError); }

            return new SuccessDataResult<User>(user, Messages.SuccessfulLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            var isExist = UserExist(userForRegisterDto.Email);
            if (!isExist.Success)
            {
                return new ErrorDataResult<User>(isExist.Message);
            }

            byte[] passwordSalt, passwordHash;
            HashingHelper.CreatePassword(userForRegisterDto.Password,
                out passwordSalt, out passwordHash);

            var user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                Status = true
            };

            _userService.Add(user);

            return new SuccessDataResult<User>(user,Messages.SuccessfulRegister);
        }

        public IResult UserExist(string email)
        {
            var user = _userService.GetByEmail(email);
            if(user.Data == null)
            {
                return  new SuccessResult(Messages.UserIsNotFound);
            }

            return new ErrorResult(Messages.UserAlreadyExists);
        }
    }
}
