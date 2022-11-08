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
    public class StudentService : ServiceBase, IStudentService
    {
        public StudentService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<CityViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<CityViewModel>>(await unitOfWork.CityRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<CityViewModel> GetAsync(int id)
        {
            return mapper.Map<CityViewModel>(await unitOfWork.CityRepository.GetAsync(id));
        }

        public async Task<CityViewModel> AddAsync(CityViewModel model)
        {
            return mapper.Map<CityViewModel>(await Task.FromResult(await unitOfWork.CityRepository.AddAsync(mapper.Map<CityViewModel, City>(model))));
        }
    }
}