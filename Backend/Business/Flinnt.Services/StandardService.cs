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
    public class StandardService : ServiceBase, IStandardService
    {
        public StandardService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<CityViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<CityViewModel>>(await unitOfWork.CityRepository.GetAllAsync());
            return result.ToList();
        }
    }
}