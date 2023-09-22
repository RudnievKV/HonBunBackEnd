using AutoMapper;
using HonbunNoAnkiApi.Common;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserDto> GetUser(long id)
        {
            var user = await _unitOfWork.UserRepo.GetUser(id);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.WordCollectionCount = await _unitOfWork.WordCollectionRepo.Find(s => s.User_ID == id).CountAsync();
            userDto.NumberOfWords = await _unitOfWork.WordRepo.Find(s => s.WordCollection.User_ID == id).Include(s => s.WordCollection).CountAsync();
            return userDto;
        }
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _unitOfWork.UserRepo.GetUsers();
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                userDto.WordCollectionCount = await _unitOfWork.WordCollectionRepo.Find(s => s.User_ID == userDto.User_ID).CountAsync();
                userDto.NumberOfWords = await _unitOfWork.WordRepo.Find(s => s.WordCollection.User_ID == userDto.User_ID).Include(s => s.WordCollection).CountAsync();
                userDtos.Add(userDto);
            }
            return userDtos;
        }
        public async Task<UserDto> CreateUser(UserCreateDto userCreateDto)
        {
            var numberOfDuplicates = await _unitOfWork.UserRepo
                .Find(s => s.Email == userCreateDto.Email).CountAsync();
            if (numberOfDuplicates > 0)
            {
                return null;
            }

            PasswordHasher passwordHasher = new PasswordHasher();
            var passwordHash = passwordHasher.HashPassword(userCreateDto.Password);

            var newUser = new User()
            {
                Email = userCreateDto.Email,
                Username = userCreateDto.Username,
                PasswordHash = passwordHash,
                CreatedDate = System.DateTimeOffset.Now
            };

            _unitOfWork.UserRepo.Create(newUser);

            await _unitOfWork.SaveChangesAsync();

            var userDto = await GetUser(newUser.User_ID);
            return userDto;
        }

        public async Task DeleteUser(long id)
        {
            var user = await _unitOfWork.UserRepo.GetUser(id);
            if (user == null)
            {
                throw new Exception();
            }

            _unitOfWork.UserRepo.Delete(user);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<UserDto> UpdateUser(long id, UserUpdateDto userUpdateDto)
        {
            var numberOfDuplicates = await _unitOfWork.UserRepo
                .Find(s => s.Email == userUpdateDto.Email && s.User_ID != id)
                .CountAsync();

            if (numberOfDuplicates > 0)
            {
                return null;
            }

            var user = await _unitOfWork.UserRepo.GetUser(id);

            string passwordHash;
            if (userUpdateDto.Password == null || userUpdateDto.Password == "")
            {
                passwordHash = user.PasswordHash;
            }
            else
            {
                PasswordHasher passwordHasher = new PasswordHasher();

                passwordHash = passwordHasher.HashPassword(userUpdateDto.Password);
            }



            var newUser = new User()
            {
                User_ID = id,
                Email = userUpdateDto.Email,
                PasswordHash = passwordHash,
                Username = userUpdateDto.Username,
                UpdatedDate = System.DateTimeOffset.Now
            };
            _unitOfWork.UserRepo.Update(newUser);
            await _unitOfWork.SaveChangesAsync();


            var userDto = await GetUser(id);
            return userDto;
        }

    }
}
