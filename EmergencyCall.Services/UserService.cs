using EmergencyCall.Core;
using EmergencyCall.Entities;
using EmergencyCall.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyCall.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<User> CreateUser(User newUser)
        {
            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
            return newUser;
        }

        public async Task DeleteUser(User user)
        {
            _unitOfWork.Users.Remove(user);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task UpdateUser(User userToBeUpdated, User user)
        {
            userToBeUpdated.FirstName = user.FirstName;

            await _unitOfWork.CommitAsync();
        }

        public async Task<User> UserLogin(string email, string password)
        {
            var user = await _unitOfWork.Users.UserLogin(email, password);
            return user;
        } 
    }
}