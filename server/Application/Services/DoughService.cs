namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.AutoMapper;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Repository;

    public class DoughService : IDoughService
    {
        private IDoughRepository _doughRepository;

        public DoughService(IDoughRepository doughRepository)
        {
            _doughRepository = doughRepository;
        }

        public void Delete(string id)
        {
            _doughRepository.Delete(id);
        }

        public DoughDto GetByName(string name)
        {
            var existingDough = _doughRepository.GetByName(name);

            if (existingDough != null)
            {
                return existingDough.ToViewModel();
            }

            return null;
        }

        public DoughDto GetById(string id)
        {
            var existingDough = _doughRepository.GetById(id);

            if (existingDough != null)
            {
                return existingDough.ToViewModel();
            }

            return null;
        }

        public IEnumerable<DoughDto> GetAll()
        {
            return _doughRepository.GetAll().Select(x => x.ToViewModel());
        }

        public DoughDto Insert(DoughCreateRequestDto item)
        {
            return _doughRepository.Insert(item.ToModel()).ToViewModel();
        }

        public DoughDto Update(string id, DoughUpdateRequestDto item)
        {
            return _doughRepository.Update(id, item.ToModel()).ToViewModel();
        }

        public DoughDto Patch(string id, DoughPatchRequestDto dough)
        {
            return _doughRepository.Patch(id, dough.ToModel()).ToViewModel();
        }

        public IEnumerable<string> GetIdentificators()
        {
            return _doughRepository.GetIdentificators();
        }
    }
}