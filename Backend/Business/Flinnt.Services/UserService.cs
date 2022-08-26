﻿using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Services
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            var result = mapper.Map<List<UserModel>>(await unitOfWork.UserRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<UserModel> GetAsync(int id)
        {
            return mapper.Map<UserModel>(await unitOfWork.UserRepository.GetAsync(id));
        }

        public async Task<bool> AddAsync(User model)
        {
            await unitOfWork.UserRepository.AddAsync(model);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(User model)
        {
            var user = await unitOfWork.UserRepository.GetAsync(model.UserId);
            if (user != null)
            {
                user.UserId = model.UserId;
                //MAP other fields
                await unitOfWork.UserRepository.UpdateAsync(user);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var institute = unitOfWork.UserRepository.GetAsync(id).Result;
            if (institute != null)
            {
                await unitOfWork.UserRepository.DeleteAsync(institute);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}